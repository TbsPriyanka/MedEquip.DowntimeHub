using MedEquip.DowntimeHub.Common.ResponseHelper;
using MedEquip.DowntimeHub.Model.User;

namespace MedEquip.DowntimeHub.BAL.Interfaces
{
    public interface IUserService
    {
        Task<ResponseResult<UserResponse>> AddOrUpdate(UserRequest request);
        Task<ResponseResult<UserResponse>> GetById(int userId);
        Task<ResponseResult<List<UserResponse>>> GetList();
        Task<ResponseResult<bool>> Delete(int userId);
    }
}
