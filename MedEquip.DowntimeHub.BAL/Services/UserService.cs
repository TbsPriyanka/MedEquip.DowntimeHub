using MedEquip.DowntimeHub.BAL.Interfaces;
using MedEquip.DowntimeHub.Common.JwtHelper;
using MedEquip.DowntimeHub.Common.ResponseHelper;
using MedEquip.DowntimeHub.DAL.Interfaces;
using MedEquip.DowntimeHub.Model.User;

namespace MedEquip.DowntimeHub.BAL.Services
{
    public class UserService(IUserRepository _repository) : IUserService
    {
        public async Task<ResponseResult<UserResponse>> AddOrUpdate(UserRequest request)
        {
            try
            {
                if (request.RoleId <= 0)
                    return ResponseHelper<UserResponse>.Error("Invalid role selected.");

                if (string.IsNullOrWhiteSpace(request.Email))
                    return ResponseHelper<UserResponse>.Error("Email is required.");

                if (request.UserId == 0 && string.IsNullOrWhiteSpace(request.Password))
                    return ResponseHelper<UserResponse>.Error("Password is required.");

                if (!string.IsNullOrWhiteSpace(request.Password))
                    request.Password = PasswordHelper.HashPassword(request.Password);

                int userId = await _repository.AddOrUpdateUser(request);

                if (userId <= 0)
                    return ResponseHelper<UserResponse>.Error("Unable to save user.");

                return ResponseHelper<UserResponse>.Success(
                    request.UserId == 0 ? "User created successfully." : "User updated successfully.",
                    new UserResponse { UserId = userId }
                );
            }
            catch (Exception)
            {
                return ResponseHelper<UserResponse>.Error("Unexpected error occurred.");
            }
        }

        public async Task<ResponseResult<UserResponse>> GetById(int userId)
        {
            try
            {
                if (userId <= 0)
                    return ResponseHelper<UserResponse>.Error("Invalid UserId.");

                var user = await _repository.GetUserById(userId);

                if (user == null)
                    return ResponseHelper<UserResponse>.Error("User not found.");

                return ResponseHelper<UserResponse>.Success(user, "User fetched successfully.");
            }
            catch (Exception)
            {
                return ResponseHelper<UserResponse>.Error("Unexpected error occurred. Please contact support.");
            }
        }

        public async Task<ResponseResult<List<UserResponse>>> GetList()
        {
            try
            {
                var users = await _repository.GetUserList();

                return ResponseHelper<List<UserResponse>>.Success(
                    users.ToList(),
                    "Users fetched successfully."
                );
            }
            catch (Exception)
            {
                return ResponseHelper<List<UserResponse>>.Error("Unexpected error occurred. Please contact support.");
            }
        }

        public async Task<ResponseResult<bool>> Delete(int userId, int actionBy)
        {
            try
            {
                if (userId <= 0)
                    return ResponseHelper<bool>.Error("Invalid UserId.");

                bool isDeleted = await _repository.DeleteUser(userId, actionBy);

                if (!isDeleted)
                    return ResponseHelper<bool>.Error("Unable to delete user.");

                return ResponseHelper<bool>.Success(true, "User deleted successfully.");
            }
            catch (Exception)
            {
                return ResponseHelper<bool>.Error("Unexpected error occurred. Please contact support.");
            }
        }
    }
}
