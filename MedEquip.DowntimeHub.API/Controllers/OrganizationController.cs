using MedEquip.DowntimeHub.BAL.Interfaces;
using MedEquip.DowntimeHub.Model.Organizations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedEquip.DowntimeHub.API.Controllers
{
    [ApiController]
    [Route("api/organization")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }
        [HttpPost("AddOrUpdate")]
        [Authorize]
        public async Task<IActionResult> AddOrUpdate([FromBody] OrganizationRequest request)
        {
            var result = await _organizationService.AddOrUpdate(request);
            return StatusCode(result.Code, result);
        }

        [HttpGet("GetById/{organizationId}")]
        [Authorize]
        public async Task<IActionResult> GetById(int organizationId)
        {
            var result = await _organizationService.GetById(organizationId);
            return StatusCode(result.Code, result);
        }

        [HttpGet("List")]
        [Authorize]
        public async Task<IActionResult> GetList()
        {
            var result = await _organizationService.GetList();
            return StatusCode(result.Code, result);
        }

        [HttpDelete("Delete/{organizationId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int organizationId)
        {
            var result = await _organizationService.Delete(organizationId);
            return StatusCode(result.Code, result);
        }
    }
}
