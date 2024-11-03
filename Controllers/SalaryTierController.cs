using AutoMapper;
using Domain;
using Domain.dto;
using HR.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SalaryTiersController : ControllerBase
    {
        private readonly ISalaryTierRepository _salaryTierRepository; // حقن الريبوزيتوري
        private readonly IMapper _mapper; // حقن AutoMapper

        public SalaryTiersController(
            ISalaryTierRepository salaryTierRepository, 
            IMapper mapper
            )
        {
            _salaryTierRepository = salaryTierRepository;
            _mapper = mapper;
        }

        // GET: api/SalaryTiers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalaryTier>>> GetSalaryTiers()
        {
            var salaryTiers = await _salaryTierRepository.GetAllSalaryTiersAsync();
            return Ok(salaryTiers);
        }

        // GET: api/SalaryTiers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalaryTier>> GetSalaryTier(int id)
        {
            var salaryTier = await _salaryTierRepository.GetSalaryTierByIdAsync(id);
            if (salaryTier == null)
            {
                return NotFound("Salary tier not found.");
            }
            return Ok(salaryTier);
        }

        // POST: api/SalaryTiers
        [HttpPost]
        public async Task<ActionResult<SalaryTier>> CreateSalaryTier([FromBody] SalaryTierDto salaryTierDto)
        {
            var salaryTier = _mapper.Map<SalaryTier>(salaryTierDto);
            await _salaryTierRepository.AddSalaryTierAsync(salaryTier);
            return CreatedAtAction(nameof(GetSalaryTier), new { id = salaryTier.SalaryTierId }, salaryTier);
        }

        // PUT: api/SalaryTiers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSalaryTier(int id, [FromBody] SalaryTierDto salaryTierDto)
        {
            if (id != salaryTierDto.SalaryTierId)
            {
                return BadRequest("The ID in the URL does not match the SalaryTierId in the body.");
            }

            var existingSalaryTier = await _salaryTierRepository.GetSalaryTierByIdAsync(id);
            if (existingSalaryTier == null)
            {
                return NotFound("Salary tier not found.");
            }

            var salaryTier = _mapper.Map<SalaryTier>(salaryTierDto);
            await _salaryTierRepository.UpdateSalaryTierAsync(salaryTier);
            return NoContent();
        }

        // DELETE: api/SalaryTiers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalaryTier(int id)
        {
            var existingSalaryTier = await _salaryTierRepository.GetSalaryTierByIdAsync(id);
            if (existingSalaryTier == null)
            {
                return NotFound("Salary tier not found.");
            }

            await _salaryTierRepository.DeleteSalaryTierAsync(id);
            return NoContent();
        }
    }
}
