using MedEquip.DowntimeHub.BAL.Interfaces;
using MedEquip.DowntimeHub.Common.ResponseHelper;
using MedEquip.DowntimeHub.DAL.Interfaces;
using MedEquip.DowntimeHub.Model.Organizations;

namespace MedEquip.DowntimeHub.BAL.Services
{
    public class OrganizationService(IOrganizationRepository _repository) : IOrganizationService
    {
        public async Task<ResponseResult<OrganizationResponse>> AddOrUpdate(OrganizationRequest request)
        {
            try
            {
                int organizationId = await _repository.AddOrUpdateOrganization(request);

                if (organizationId <= 0)
                    return ResponseHelper<OrganizationResponse>.Error("Unable to save Organization.");

                return ResponseHelper<OrganizationResponse>.Success(
                    request.OrganizationId == 0
                        ? "Organization created successfully."
                        : "Organization updated successfully.",
                    new OrganizationResponse
                    {
                        OrganizationId = organizationId,
                        Name = request.Name,
                        Address = request.Address,
                        ContactEmail = request.ContactEmail,
                        ContactPhone = request.ContactPhone,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                );
            }
            catch (Exception)
            {
                return ResponseHelper<OrganizationResponse>.Error("Unexpected error occurred.");
            }
        }

        public async Task<ResponseResult<OrganizationResponse>> GetById(int organizationId)
        {
            try
            {
                if (organizationId <= 0)
                    return ResponseHelper<OrganizationResponse>.Error("Invalid OrganizationId.");

                var organization = await _repository.GetOrganizationById(organizationId);

                if (organization == null)
                    return ResponseHelper<OrganizationResponse>.Error("Organization not found.");

                return ResponseHelper<OrganizationResponse>.Success(organization, "Organization fetched successfully.");
            }
            catch (Exception)
            {
                return ResponseHelper<OrganizationResponse>.Error("Unexpected error occurred. Please contact support.");
            }
        }

        public async Task<ResponseResult<List<OrganizationResponse>>> GetList()
        {
            try
            {
                var organizations = await _repository.GetOrganizationList();

                return ResponseHelper<List<OrganizationResponse>>.Success(organizations.ToList(), "Organization fetched successfully.");
            }
            catch (Exception)
            {
                return ResponseHelper<List<OrganizationResponse>>.Error("Unexpected error occurred. Please contact support.");
            }
        }

        public async Task<ResponseResult<bool>> Delete(int organizationId)
        {
            try
            {
                if (organizationId <= 0)
                    return ResponseHelper<bool>.Error("Invalid OrganizationId.");

                bool isDeleted = await _repository.DeleteOrganization(organizationId);

                if (!isDeleted)
                    return ResponseHelper<bool>.Error("Unable to delete organization.");

                return ResponseHelper<bool>.Success(true, "Organization deleted successfully.");
            }
            catch (Exception)
            {
                return ResponseHelper<bool>.Error("Unexpected error occurred. Please contact support.");
            }
        }
    }
}
