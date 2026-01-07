using MedEquip.DowntimeHub.Model.Role;

namespace MedEquip.DowntimeHub.DAL.Interfaces
{
    public interface IRoleRepository
    {
        Task<int> AddOrUpdateRole(RoleRequest request);
        Task<RoleResponse?> GetRoleById(int roleId);
        Task<IEnumerable<RoleResponse>> GetRoleList();
        Task<bool> DeleteRole(int roleId);
    }
}
