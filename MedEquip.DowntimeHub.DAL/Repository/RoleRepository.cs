using Dapper;
using MedEquip.DowntimeHub.Common.SqlHelper;
using MedEquip.DowntimeHub.DAL.Interfaces;
using MedEquip.DowntimeHub.Model.Role;

namespace MedEquip.DowntimeHub.DAL.Repository
{
    public class RoleRepository(ISqlHelper _sqlHelper) : IRoleRepository
    {
        public async Task<int> AddOrUpdateRole(RoleRequest request)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@RoleId", request.RoleId);
            parameters.Add("@RoleName", request.RoleName);
            parameters.Add("@IsActive", request.IsActive);
            parameters.Add("@CreatedDate", request.CreatedDate);

            return await _sqlHelper.ExecuteScalarAsync<int>(StoredProcedure.Role_AddOrUpdate, parameters);
        }

        public async Task<RoleResponse?> GetRoleById(int roleId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@RoleId", roleId);

            return await _sqlHelper.GetSingleAsync<RoleResponse>(StoredProcedure.Role_GetById, parameters);
        }

        public async Task<IEnumerable<RoleResponse>> GetRoleList()
        {
            return await _sqlHelper.QueryAsync<RoleResponse>(StoredProcedure.Role_List);
        }

        public async Task<bool> DeleteRole(int roleId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@RoleId", roleId);

            int result = await _sqlHelper.ExecuteScalarAsync<int>(StoredProcedure.Role_Delete, parameters);

            return result > 0;
        }
    }
}
