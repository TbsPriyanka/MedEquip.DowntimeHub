using Dapper;
using MedEquip.DowntimeHub.Common.SqlHelper;
using MedEquip.DowntimeHub.DAL.Interfaces;
using MedEquip.DowntimeHub.Model.AuthLogin;

namespace MedEquip.DowntimeHub.DAL.Repository
{
    public class LoginRepository(ISqlHelper _sqlHelper) : ILoginRepository
    {
        public async Task<LoginResponse?> LoginAsync(string email, string passwordHash)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Email", email);
            parameters.Add("@Password", passwordHash);

            return await _sqlHelper.QueryFirstOrDefaultAsync<LoginResponse>(StoredProcedure.User_Login, parameters);
        }
        public async Task ForgotPasswordAsync(string email, string token, int expiryMinutes)
        {
            var param = new DynamicParameters();
            param.Add("@Email", email);
            param.Add("@ResetToken", token);
            param.Add("@ExpiryMinutes", expiryMinutes);

            await _sqlHelper.ExecuteAsync(StoredProcedure.User_ForgotPassword, param);
        }

        public async Task<bool> ResetPasswordAsync(string token, string newHash)
        {
            var param = new DynamicParameters();
            param.Add("@ResetToken", token);
            param.Add("@NewPassword", newHash);

            int rows = await _sqlHelper.ExecuteRawSqlAsync("EXEC dbo.User_ResetPassword @ResetToken, @NewPassword", param);

            return rows > 0;
        }

        public async Task<bool> ChangePasswordAsync(int userId, string oldHash, string newHash)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", userId);
            param.Add("@OldPassword", oldHash);
            param.Add("@NewPassword", newHash);

            int rows = await _sqlHelper.ExecuteRawSqlAsync("EXEC dbo.User_ChangePassword @UserId, @OldPassword, @NewPassword", param);

            return rows > 0;
        }
        public async Task<bool> LogoutAsync(int userId, string token)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);
            parameters.Add("@Token", token);

            var affectedRows = await _sqlHelper.ExecuteScalarAsync<int>(StoredProcedure.User_Logout, parameters);

            return affectedRows > 0;
        }
    }
}
