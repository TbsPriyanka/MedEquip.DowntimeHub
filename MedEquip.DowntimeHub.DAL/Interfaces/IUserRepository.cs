using MedEquip.DowntimeHub.Model.User;

namespace MedEquip.DowntimeHub.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<int> AddOrUpdateUser(UserRequest request);
        Task<UserResponse?> GetUserById(int userId);
        Task<IEnumerable<UserResponse>> GetUserList();
        Task<bool> DeleteUser(int userId, int actionBy);
    }
}
