using MedEquip.DowntimeHub.Common.ResponseHelper;
using MedEquip.DowntimeHub.Model.Role;

namespace MedEquip.DowntimeHub.BAL.Interfaces
{
    public interface IRoleService
    {
        Task<ResponseResult<RoleResponse>> AddOrUpdate(RoleRequest request);
        Task<ResponseResult<RoleResponse>> GetById(int roleId);
        Task<ResponseResult<List<RoleResponse>>> GetList();
        Task<ResponseResult<bool>> Delete(int roleId);
    }
}
