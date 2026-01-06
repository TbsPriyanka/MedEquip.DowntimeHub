using MedEquip.DowntimeHub.Model.AuthLogin;

namespace MedEquip.DowntimeHub.DAL.Interfaces
{
    public interface ILoginRepository
    {
        Task<LoginResponse?> LoginAsync(string email, string passwordHash);
        Task ForgotPasswordAsync(string email, string token, int expiryMinutes);
        Task<bool> ResetPasswordAsync(string token, string newHash);
        Task<bool> ChangePasswordAsync(int userId, string oldHash, string newHash);
        Task<bool> LogoutAsync(int userId, string token);
    }
}
