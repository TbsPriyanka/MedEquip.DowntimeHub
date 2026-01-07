using MedEquip.DowntimeHub.Common.ResponseHelper;
using MedEquip.DowntimeHub.Model.Organizations;

namespace MedEquip.DowntimeHub.BAL.Interfaces
{
    public interface IOrganizationService
    {
        Task<ResponseResult<OrganizationResponse>> AddOrUpdate(OrganizationRequest request);
        Task<ResponseResult<OrganizationResponse>> GetById(int organizationId);
        Task<ResponseResult<List<OrganizationResponse>>> GetList();
        Task<ResponseResult<bool>> Delete(int organizationId);
    }
}
