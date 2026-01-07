using MedEquip.DowntimeHub.Model.Organizations;

namespace MedEquip.DowntimeHub.DAL.Interfaces
{
    public interface IOrganizationRepository
    {
        Task<int> AddOrUpdateOrganization(OrganizationRequest request);
        Task<OrganizationResponse?> GetOrganizationById(int organizationId);
        Task<IEnumerable<OrganizationResponse>> GetOrganizationList();
        Task<bool> DeleteOrganization(int organizationId);
    }
}
