using Dapper;
using MedEquip.DowntimeHub.Common.SqlHelper;
using MedEquip.DowntimeHub.DAL.Interfaces;
using MedEquip.DowntimeHub.Model.Organizations;

namespace MedEquip.DowntimeHub.DAL.Repository
{
    public class OrganizationRepository(ISqlHelper _sqlHelper) : IOrganizationRepository
    {
        public async Task<int> AddOrUpdateOrganization(OrganizationRequest request)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@OrganizationId", request.OrganizationId);
            parameters.Add("@Name", request.Name);
            parameters.Add("@Address", request.Address);
            parameters.Add("@ContactEmail", request.ContactEmail);
            parameters.Add("@ContactPhone", request.ContactPhone);

            return await _sqlHelper.ExecuteScalarAsync<int>(StoredProcedure.Organization_AddOrUpdate, parameters);
        }

        public async Task<OrganizationResponse?> GetOrganizationById(int organizationId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OrganizationId", organizationId);

            return await _sqlHelper.GetSingleAsync<OrganizationResponse>(StoredProcedure.Organization_GetById, parameters);
        }

        public async Task<IEnumerable<OrganizationResponse>> GetOrganizationList()
        {
            return await _sqlHelper.QueryAsync<OrganizationResponse>(StoredProcedure.Organization_List);
        }
        public async Task<bool> DeleteOrganization(int organizationId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OrganizationId", organizationId);

            int result = await _sqlHelper.ExecuteScalarAsync<int>(StoredProcedure.Organization_Delete, parameters);

            return result > 0;
        }
    }
}
