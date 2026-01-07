using MedEquip.DowntimeHub.BAL.Interfaces;
using MedEquip.DowntimeHub.Common.ResponseHelper;
using MedEquip.DowntimeHub.DAL.Interfaces;
using MedEquip.DowntimeHub.Model.Role;

namespace MedEquip.DowntimeHub.BAL.Services
{
    public class RoleService(IRoleRepository _repository) : IRoleService
    {
        public async Task<ResponseResult<RoleResponse>> AddOrUpdate(RoleRequest request)
        {
            try
            {
                int roleId = await _repository.AddOrUpdateRole(request);

                if (roleId <= 0)
                    return ResponseHelper<RoleResponse>.Error("Unable to save Role.");

                return ResponseHelper<RoleResponse>.Success(
                    request.RoleId == 0 ? "Role created successfully." : "Role updated successfully.",
                    new RoleResponse
                    {
                        RoleId = roleId,
                        RoleName = request.RoleName,
                        IsActive = request.IsActive,
                        CreatedDate = DateTime.UtcNow
                    }
                );
            }
            catch (Exception)
            {
                return ResponseHelper<RoleResponse>.Error("Unexpected error occurred.");
            }
        }

        public async Task<ResponseResult<RoleResponse>> GetById(int roleId)
        {
            try
            {
                if (roleId <= 0)
                    return ResponseHelper<RoleResponse>.Error("Invalid RoleId.");

                var role = await _repository.GetRoleById(roleId);

                if (role == null)
                    return ResponseHelper<RoleResponse>.Error("Role not found.");

                return ResponseHelper<RoleResponse>.Success(role, "Role fetched successfully.");
            }
            catch (Exception)
            {
                return ResponseHelper<RoleResponse>.Error("Unexpected error occurred. Please contact support.");
            }
        }

        public async Task<ResponseResult<List<RoleResponse>>> GetList()
        {
            try
            {
                var roles = await _repository.GetRoleList();

                return ResponseHelper<List<RoleResponse>>.Success(roles.ToList(), "Roles fetched successfully.");
            }
            catch (Exception)
            {
                return ResponseHelper<List<RoleResponse>>.Error("Unexpected error occurred. Please contact support.");
            }
        }
        public async Task<ResponseResult<bool>> Delete(int roleId)
        {
            try
            {
                if (roleId <= 0)
                    return ResponseHelper<bool>.Error("Invalid RoleId.");

                bool isDeleted = await _repository.DeleteRole(roleId);

                if (!isDeleted)
                    return ResponseHelper<bool>.Error("Unable to delete role.");

                return ResponseHelper<bool>.Success(true, "Role deleted successfully.");
            }
            catch (Exception)
            {
                return ResponseHelper<bool>.Error("Unexpected error occurred. Please contact support.");
            }
        }
    }
}
