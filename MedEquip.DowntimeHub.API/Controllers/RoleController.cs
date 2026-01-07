using MedEquip.DowntimeHub.BAL.Interfaces;
using MedEquip.DowntimeHub.Model.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedEquip.DowntimeHub.API.Controllers
{
    [ApiController]
    [Route("api/role")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("AddOrUpdate")]
        [Authorize]
        public async Task<IActionResult> AddOrUpdate([FromBody] RoleRequest request)
        {
            var result = await _roleService.AddOrUpdate(request);
            return StatusCode(result.Code, result);
        }

        [HttpGet("GetById/{roleId}")]
        [Authorize]
        public async Task<IActionResult> GetById(int roleId)
        {
            var result = await _roleService.GetById(roleId);
            return StatusCode(result.Code, result);
        }

        [HttpGet("List")]
        [Authorize]
        public async Task<IActionResult> GetList()
        {
            var result = await _roleService.GetList();
            return StatusCode(result.Code, result);
        }

        [HttpDelete("Delete/{roleId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int roleId)
        {
            var result = await _roleService.Delete(roleId);
            return StatusCode(result.Code, result);
        }
    }
}
