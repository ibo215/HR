using AutoMapper;
using Domain;
using Domain.dto.Profiles;
using HR.Repositoreies;
using HR.Services;
using HR.ViewModels;
using HR.ViewModels.DTOs.SalaryTierDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace HR.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaryTierController : ControllerBase
    {
        private readonly ISalaryTierRepository _salaryTierRepository;
        private readonly IValidationService _validationService;
        private readonly ILogger<SalaryTierController> _logger;
        private readonly IMapper _mapper;

        public SalaryTierController(
            ISalaryTierRepository salaryTierRepository,
            IValidationService validationService,
            ILogger<SalaryTierController> logger,
            IMapper mapper
        )
        {
            _salaryTierRepository = salaryTierRepository;
            _validationService = validationService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("Get-All")]
        public async Task<ActionResult<IEnumerable<SalaryTierForPreview>>> GetAllSalaryTiers(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var salaryTiers = await _salaryTierRepository.GetAllSalaryTiersAsync(pageNumber, pageSize);

                if (salaryTiers == null || !salaryTiers.Any())
                {
                    _logger.LogWarning("No salary tiers found.");
                    return NotFound("No salary tiers found.");
                }

                var salaryTierDtos = _mapper.Map<IEnumerable<SalaryTierForPreview>>(salaryTiers);

   
                return Ok(salaryTierDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting salary tiers.");
                return StatusCode(500, "Internal server error.");
            }
        }


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

        [HttpGet("Get-Deleted-SalaryTiers")]
        public async Task<ActionResult<IEnumerable<SalaryTierForPreview>>> GetDeletedSalaryTiers()
        {
            var deletedSalaryTiers = await _salaryTierRepository.GetDeletedSalaryTiersAsync();
            var deletedSalaryTierDtos = _mapper.Map<IEnumerable<SalaryTierForPreview>>(deletedSalaryTiers);
            return Ok(deletedSalaryTierDtos);
        }

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
            return Ok(salaryTierForPreview);
        }

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

            _mapper.Map(salaryTierDto, existingSalaryTier);
            await _salaryTierRepository.UpdateSalaryTierAsync(existingSalaryTier);

            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteSalaryTier(int id)
        {
            await _salaryTierRepository.DeleteSalaryTierAsync(id);
            return NoContent();
        }
    }

}
