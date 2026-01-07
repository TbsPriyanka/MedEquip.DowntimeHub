using Dapper;
using MedEquip.DowntimeHub.Common.SqlHelper;
using MedEquip.DowntimeHub.DAL.Interfaces;
using MedEquip.DowntimeHub.Model.User;

namespace MedEquip.DowntimeHub.DAL.Repository
{
    public class UserRepository(ISqlHelper _sqlHelper) : IUserRepository
    {
        public async Task<int> AddOrUpdateUser(UserRequest request)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@UserId", request.UserId);
            parameters.Add("@UserCode", request.UserCode);
            parameters.Add("@FullName", request.FullName);
            parameters.Add("@Email", request.Email);
            parameters.Add("@MobileNo", request.MobileNo);
            parameters.Add("@RoleId", request.RoleId);
            parameters.Add("@Department", request.Department);
            parameters.Add("@IsActive", request.IsActive);
            parameters.Add("@Password", request.Password);
            parameters.Add("@ActionBy", request.ActionBy);

            return await _sqlHelper.ExecuteScalarAsync<int>(StoredProcedure.User_AddOrUpdate,parameters);
        }

        public async Task<UserResponse?> GetUserById(int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);

            return await _sqlHelper.GetSingleAsync<UserResponse>(StoredProcedure.User_GetById,parameters);
        }

        public async Task<IEnumerable<UserResponse>> GetUserList()
        {
            return await _sqlHelper.QueryAsync<UserResponse>(StoredProcedure.User_List);
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);

            int result = await _sqlHelper.ExecuteScalarAsync<int>(StoredProcedure.User_Delete,parameters);

            return result > 0;
        }
    }
}
