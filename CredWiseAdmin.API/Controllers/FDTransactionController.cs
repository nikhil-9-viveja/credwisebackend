using CredWiseAdmin.Core.DTOs.FDProduct;
using CredWiseAdmin.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CredWiseAdmin.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FDTransactionController : ControllerBase
    {
        private readonly IFDTransactionService _fdTransactionService;

        public FDTransactionController(IFDTransactionService fdTransactionService)
        {
            _fdTransactionService = fdTransactionService;
        }

        [HttpPost]
        public async Task<ActionResult<FDTransactionResponseDto>> CreateFDTransaction(CreateFDTransactionDto dto)
        {
            var result = await _fdTransactionService.CreateFDTransactionAsync(dto, User.Identity.Name);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<FDTransactionResponseDto>> UpdateFDTransaction(UpdateFDTransactionDto dto)
        {
            var result = await _fdTransactionService.UpdateFDTransactionAsync(dto, User.Identity.Name);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFDTransaction(int id)
        {
            var result = await _fdTransactionService.DeleteFDTransactionAsync(id, User.Identity.Name);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FDTransactionResponseDto>> GetFDTransactionById(int id)
        {
            var result = await _fdTransactionService.GetFDTransactionByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FDTransactionResponseDto>>> GetAllFDTransactions()
        {
            var result = await _fdTransactionService.GetAllFDTransactionsAsync();
            return Ok(result);
        }
    }
} 