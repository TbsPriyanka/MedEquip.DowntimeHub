using MedEquip.DowntimeHub.Common.ResponseHelper;
using MedEquip.DowntimeHub.Model.AuthLogin;

namespace MedEquip.DowntimeHub.BAL.Interfaces
{
    public interface ILoginService
    {
        Task<ResponseResult<LoginResponse>> Login(LoginRequest request);
        Task<ResponseResult<string>> ForgotPassword(ForgotPasswordRequest request);
        Task<ResponseResult<string>> ResetPassword(ResetPasswordRequest request);
        Task<ResponseResult<string>> ChangePassword(ChangePasswordRequest request);
        Task<ResponseResult<string>> Logout(string token);
    }
}
