using AutoMapper;
using Domain;
using Domain.dto.Profiles;
using HR.DTOs.SalaryTierDTOs;
using HR.Repositoreies;
using HR.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR.Controllers
{
    [ApiController]
    [Route("api/Salary-Tier")]
    public class SalaryTierController : ControllerBase
    {
        private readonly Repositoreies.ISalaryTierRepository _salaryTierRepository;
        private readonly IValidationService _validationService;
        private readonly IMapper _mapper;

        public SalaryTierController(
            Repositoreies.ISalaryTierRepository salaryTierRepository,
            IValidationService validationService,
            IMapper mapper
        )
        {
            _salaryTierRepository = salaryTierRepository;
            _validationService = validationService;
            _mapper = mapper;
        }

        // Get all salary tiers
        [HttpGet("Get-All")]
        public async Task<ActionResult<IEnumerable<SalaryTierForPreview>>> GetAllSalaryTiers()
        {
            var salaryTiers = await _salaryTierRepository.GetAllSalaryTiersAsync();
            var salaryTierDtos = _mapper.Map<IEnumerable<SalaryTierForPreview>>(salaryTiers);
            return Ok(salaryTierDtos);
        }

        // Get salary tier by ID
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<SalaryTierForPreview>> GetSalaryTierById(int id)
        {
            if (!_validationService.ValidateId(id, out string errorMessage))
            {
                return BadRequest(errorMessage);
            }

            var salaryTier = await _salaryTierRepository.GetSalaryTierByIdAsync(id);
            if (salaryTier == null) return NotFound("Salary tier not found.");

            var salaryTierDto = _mapper.Map<SalaryTierForPreview>(salaryTier);
            return Ok(salaryTierDto);
        }

        [HttpGet("Get-Salary-Report")]
        public async Task<ActionResult<IEnumerable<SalaryTierForPreview>>> GetSalaryReport()
        {
            var salaryTiers = await _salaryTierRepository.GetSalaryReportAsync();
            if (salaryTiers == null || !salaryTiers.Any())
            {
                return NotFound("No salary data found.");
            }

            var salaryTierDtos = _mapper.Map<IEnumerable<SalaryTierForPreview>>(salaryTiers);

            return Ok(salaryTierDtos);
        }

        // Get deleted salary tiers
        [HttpGet("Get-Deleted-SalaryTiers")]
        public async Task<ActionResult<IEnumerable<SalaryTierForPreview>>> GetDeletedSalaryTiers()
        {
            var deletedSalaryTiers = await _salaryTierRepository.GetDeletedSalaryTiersAsync();

            var deletedSalaryTierDtos = _mapper.Map<IEnumerable<SalaryTierForPreview>>(deletedSalaryTiers);
            return Ok(deletedSalaryTierDtos);
        }

        // Add a new salary tier
        [HttpPost("Add")]
        public async Task<IActionResult> AddSalaryTier([FromBody] SalaryTierForAdd salaryTierDto)
        {
            if (!_validationService.ValidateCreate(salaryTierDto, out string errorMessage))
            {
                return BadRequest(errorMessage);
            }

            var salaryTier = _mapper.Map<SalaryTier>(salaryTierDto);
            await _salaryTierRepository.AddSalaryTierAsync(salaryTier);

            var salaryTierForPreview = _mapper.Map<SalaryTierForPreview>(salaryTier);
            return CreatedAtAction(nameof(GetSalaryTierById), new { id = salaryTier.SalaryTierId }, salaryTierForPreview);
        }

        // Update an existing salary tier
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateSalaryTier(int id, [FromBody] SalaryTierForUpdate salaryTierDto)
        {
            if (!_validationService.ValidateUpdate(id, salaryTierDto, out string errorMessage))
            {
                return BadRequest(errorMessage);
            }

            var existingSalaryTier = await _salaryTierRepository.GetSalaryTierByIdAsync(id);
            if (existingSalaryTier == null)
            {
                return NotFound("Salary tier not found.");
            }

            // Map updated properties to the existing entity
            _mapper.Map(salaryTierDto, existingSalaryTier);
            await _salaryTierRepository.UpdateSalaryTierAsync(existingSalaryTier);

            return NoContent();
        }

        // Delete a salary tier
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteSalaryTier(int id)
        {
            if (!_validationService.ValidateId(id, out string errorMessage))
            {
                return BadRequest(errorMessage);
            }

            await _salaryTierRepository.DeleteSalaryTierAsync(id);
            return NoContent();
        }
    }

}
