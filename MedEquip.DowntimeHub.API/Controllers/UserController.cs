using MedEquip.DowntimeHub.BAL.Interfaces;
using MedEquip.DowntimeHub.Model.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedEquip.DowntimeHub.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("AddOrUpdate")]
        [Authorize]
        public async Task<IActionResult> AddOrUpdate([FromBody] UserRequest request)
        {
            var result = await _userService.AddOrUpdate(request);
            return StatusCode(result.Code, result);
        }

        [HttpGet("GetById/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetById(int userId)
        {
            var result = await _userService.GetById(userId);
            return StatusCode(result.Code, result);
        }

        [HttpGet("List")]
        [Authorize]
        public async Task<IActionResult> GetList()
        {
            var result = await _userService.GetList();
            return StatusCode(result.Code, result);
        }

        [HttpDelete("Delete/{userId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int userId)
        {
            var result = await _userService.Delete(userId);
            return StatusCode(result.Code, result);
        }
    }
}
