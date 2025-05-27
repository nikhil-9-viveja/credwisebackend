using CredWiseAdmin.Core.DTOs.FDProduct;
using CredWiseAdmin.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CredWiseAdmin.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FDApplicationController : ControllerBase
    {
        private readonly IFDApplicationService _fdApplicationService;

        public FDApplicationController(IFDApplicationService fdApplicationService)
        {
            _fdApplicationService = fdApplicationService;
        }

        [HttpPost]
        public async Task<ActionResult<FDApplicationResponseDto>> CreateFDApplication(CreateFDApplicationDto dto)
        {
            var createdBy = User?.Identity?.Name ?? "system";
            var result = await _fdApplicationService.CreateFDApplicationAsync(dto, createdBy);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<FDApplicationResponseDto>> UpdateFDApplication(UpdateFDApplicationDto dto)
        {
            var result = await _fdApplicationService.UpdateFDApplicationAsync(dto, User.Identity?.Name ?? "system");
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFDApplication(int id)
        {
            var result = await _fdApplicationService.DeleteFDApplicationAsync(id, User.Identity?.Name ?? "system");
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FDApplicationResponseDto>> GetFDApplicationById(int id)
        {
            var result = await _fdApplicationService.GetFDApplicationByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FDApplicationResponseDto>>> GetAllFDApplications()
        {
            var result = await _fdApplicationService.GetAllFDApplicationsAsync();
            return Ok(result);
        }
    }
} 