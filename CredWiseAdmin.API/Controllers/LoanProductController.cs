using AutoMapper;
using CredWiseAdmin.Core.DTOs.LoanProduct;
using CredWiseAdmin.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CredWiseAdmin.Repository;
using Microsoft.EntityFrameworkCore;

namespace CredWiseAdmin.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanProductController : ControllerBase
    {
        private readonly ILoanProductService _loanProductService;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public LoanProductController(ILoanProductService loanProductService, IMapper mapper, AppDbContext context)
        {
            _loanProductService = loanProductService;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<LoanProductListResponse>> GetLoanProducts([FromQuery] bool includeInactive = false)
        {
            var products = await _loanProductService.GetAllLoanProductsAsync(includeInactive);
            var response = new LoanProductListResponse
            {
                Success = true,
                Data = _mapper.Map<List<LoanProductResponseDto>>(products),
                Message = "Loan products fetched successfully"
            };
            return Ok(response);
        }

        [HttpGet("active")]
        public async Task<ActionResult<LoanProductListResponse>> GetActive()
        {
            var products = await _loanProductService.GetActiveLoanProductsAsync();
            var response = new LoanProductListResponse
            {
                Success = true,
                Data = _mapper.Map<List<LoanProductResponseDto>>(products),
                Message = "Active loan products fetched successfully"
            };
            return Ok(response);
        }

        [HttpGet("type/{loanType}")]
        public async Task<ActionResult<LoanProductListResponse>> GetByType(string loanType)
        {
            var products = await _loanProductService.GetLoanProductsByTypeAsync(loanType);
            var response = new LoanProductListResponse
            {
                Success = true,
                Data = _mapper.Map<List<LoanProductResponseDto>>(products),
                Message = $"Loan products of type {loanType} fetched successfully"
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LoanProductResponseDto>> GetById(int id)
        {
            var product = await _loanProductService.GetLoanProductWithDetailsAsync(id);
            if (product == null) return NotFound();
            return Ok(_mapper.Map<LoanProductResponseDto>(product));
        }

        [HttpPost]
        public async Task<ActionResult<LoanProductResponseDto>> Create([FromBody] CreateLoanProductDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdBy = "system"; // Replace with actual user context
            var product = await _loanProductService.CreateLoanProductAsync(dto, createdBy);
            return CreatedAtAction(nameof(GetById), new { id = product.LoanProductId }, 
                _mapper.Map<LoanProductResponseDto>(product));
        }

        [HttpPost("home")]
        public async Task<ActionResult<LoanProductResponseDto>> CreateHomeLoan([FromBody] CreateHomeLoanProductDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var createdBy = "system"; // Replace with actual user context
            var product = await _loanProductService.CreateHomeLoanProductAsync(dto, createdBy);
            return CreatedAtAction(nameof(GetById), new { id = product.LoanProductId }, _mapper.Map<LoanProductResponseDto>(product));
        }

        [HttpPost("personal")]
        public async Task<ActionResult<LoanProductResponseDto>> CreatePersonalLoan([FromBody] CreatePersonalLoanProductDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var createdBy = "system"; // Replace with actual user context
            var product = await _loanProductService.CreatePersonalLoanProductAsync(dto, createdBy);
            var personalDetail = await _context.PersonalLoanDetails
                .FirstOrDefaultAsync(p => p.LoanProductId == product.LoanProductId);
            return CreatedAtAction(nameof(GetById), new { id = product.LoanProductId }, _mapper.Map<LoanProductResponseDto>(product));
        }

        [HttpPost("gold")]
        public async Task<ActionResult<LoanProductResponseDto>> CreateGoldLoan([FromBody] CreateGoldLoanProductDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var createdBy = "system"; // Replace with actual user context
            var product = await _loanProductService.CreateGoldLoanProductAsync(dto, createdBy);
            return CreatedAtAction(nameof(GetById), new { id = product.LoanProductId }, _mapper.Map<LoanProductResponseDto>(product));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LoanProductResponseDto>> Update(int id, [FromBody] UpdateLoanProductDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != dto.LoanProductId) return BadRequest("ID mismatch");

            var modifiedBy = "system"; // Replace with actual user context
            var product = await _loanProductService.UpdateLoanProductAsync(_mapper.Map<Core.Entities.LoanProduct>(dto), modifiedBy);
            return Ok(_mapper.Map<LoanProductResponseDto>(product));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var modifiedBy = "system"; // Replace with actual user context
            await _loanProductService.DeleteLoanProductAsync(id, modifiedBy);
            return NoContent();
        }
    }
} 