using MedEquip.DowntimeHub.BAL.Interfaces;
using MedEquip.DowntimeHub.Common.JwtHelper;
using MedEquip.DowntimeHub.Common.ResponseHelper;
using MedEquip.DowntimeHub.DAL.Interfaces;
using MedEquip.DowntimeHub.Model.AuthLogin;
using Microsoft.AspNetCore.Http;

namespace MedEquip.DowntimeHub.BAL.Services
{
    public class LoginService (ILoginRepository _repository, IHttpContextAccessor _httpContext, JwtTokenHelper _jwtTokenHelper) : ILoginService
    {
        public async Task<ResponseResult<LoginResponse>> Login(LoginRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Email))
                    return ResponseHelper<LoginResponse>.Error("Email is required.");

                if (string.IsNullOrWhiteSpace(request.Password))
                    return ResponseHelper<LoginResponse>.Error("Password is required.");

                string passwordHash = PasswordHelper.HashPassword(request.Password);

                var user = await _repository.LoginAsync(request.Email, passwordHash);

                if (user == null)
                    return ResponseHelper<LoginResponse>.Error("Invalid email or password.");

                user.Token = _jwtTokenHelper.GenerateJwtToken(user.UserId);

                return ResponseHelper<LoginResponse>.Success("Login successful.",user);
            }
            catch (Exception)
            {
                return ResponseHelper<LoginResponse>.Error( "Unexpected error occurred. Please contact support.");
            }
        }
        public async Task<ResponseResult<string>> ForgotPassword(ForgotPasswordRequest request)
        {
            try
            {
                string token = Guid.NewGuid().ToString("N");

                await _repository.ForgotPasswordAsync(
                    request.Email, token, 30);

                return ResponseHelper<string>.Success(
                    "If the email exists, reset instructions have been sent.");
            }
            catch
            {
                return ResponseHelper<string>.Error("Unexpected error occurred. Please contact support.");
            }
        }
        public async Task<ResponseResult<string>> ResetPassword(ResetPasswordRequest request)
        {
            try
            {
                string hash = PasswordHelper.HashPassword(request.NewPassword);

                bool success = await _repository.ResetPasswordAsync(
                    request.ResetToken, hash);

                if (!success)
                    return ResponseHelper<string>.Error("Invalid or expired token.");

                return ResponseHelper<string>.Success("Password reset successfully.");
            }
            catch
            {
                return ResponseHelper<string>.Error("Unexpected error occurred. Please contact support.");
            }
        }
        public async Task<ResponseResult<string>> ChangePassword(ChangePasswordRequest request)
        {
            try
            {
                string oldHash = PasswordHelper.HashPassword(request.OldPassword);
                string newHash = PasswordHelper.HashPassword(request.NewPassword);

                bool success = await _repository.ChangePasswordAsync(
                    request.UserId, oldHash, newHash);

                if (!success)
                    return ResponseHelper<string>.Error("Old password is incorrect.");

                return ResponseHelper<string>.Success("Password changed successfully.");
            }
            catch
            {
                return ResponseHelper<string>.Error("Unexpected error occurred. Please contact support.");
            }
        }
        public async Task<ResponseResult<string>> Logout(string token)
        {
            try
            {
                var userIdClaim = _httpContext.HttpContext?
                    .User?
                    .FindFirst("id")?.Value;

                if (string.IsNullOrEmpty(userIdClaim))
                    return ResponseHelper<string>.Error("Invalid user.");

                var success = await _repository.LogoutAsync(
                    int.Parse(userIdClaim),
                    token
                );

                if (!success)
                    return ResponseHelper<string>.Error("Logout failed or already logged out.");

                return ResponseHelper<string>.Success("Logged out successfully.");
            }
            catch (Exception)
            {
                return ResponseHelper<string>.Error("Unexpected error occurred.");
            }
        }
    }
}
