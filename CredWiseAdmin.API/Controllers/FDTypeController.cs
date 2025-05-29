using CredWiseAdmin.Core.DTOs.FDProduct;
using CredWiseAdmin.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CredWiseAdmin.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FDTypeController : ControllerBase
    {
        private readonly IFDTypeService _fdTypeService;

        public FDTypeController(IFDTypeService fdTypeService)
        {
            _fdTypeService = fdTypeService;
        }

        [HttpPost]
        public async Task<ActionResult<FDTypeResponseDto>> CreateFDType(CreateFDTypeDto dto)
        {
            var createdBy = User?.Identity?.Name;
            if (string.IsNullOrWhiteSpace(createdBy))
                createdBy = "system";

            var result = await _fdTypeService.CreateFDTypeAsync(dto, createdBy);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FDTypeResponseDto>> UpdateFDType(int id, [FromBody] UpdateFDTypeDto dto)
        {
            if (dto.FdtypeId != id) return BadRequest("ID mismatch");
            var result = await _fdTypeService.UpdateFDTypeAsync(dto, User.Identity?.Name ?? "system");
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFDType(int id)
        {
            var result = await _fdTypeService.DeleteFDTypeAsync(id, User.Identity.Name);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FDTypeResponseDto>> GetFDTypeById(int id)
        {
            var result = await _fdTypeService.GetFDTypeByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FDTypeResponseDto>>> GetAllFDTypes()
        {
            var result = await _fdTypeService.GetAllFDTypesAsync();
            return Ok(result);
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult<FDTypeResponseDto>> ToggleFDTypeStatus(int id)
        {
            var modifiedBy = User?.Identity?.Name ?? "system";
            var result = await _fdTypeService.ToggleFDTypeStatusAsync(id, modifiedBy);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
} 