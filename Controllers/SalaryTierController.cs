using AutoMapper;
using Domain;
//using Domain.dto.Profiles;
using HR.Repositoreies;
using HR.Services;
using HR.ViewModels;
using HR.ViewModels.DTOs.SalaryTierDTOs;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ISalaryTierService _salaryTierService;
        private readonly ILogger<SalaryTierController> _logger;

        public SalaryTierController(ISalaryTierService salaryTierService, ILogger<SalaryTierController> logger)
        {
            _salaryTierService = salaryTierService;
            _logger = logger;
        }

        [HttpGet("Get-All")]
        public async Task<ActionResult<IEnumerable<SalaryTierForPreview>>> GetAllSalaryTiers(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var salaryTiers = await _salaryTierService.GetAllSalaryTiersAsync(pageNumber, pageSize);
                if (!salaryTiers.Any())
                {
                    _logger.LogWarning("No salary tiers found.");
                    return NotFound("No salary tiers found.");
                }
                return Ok(salaryTiers);
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
            try
            {
                var salaryTier = await _salaryTierService.GetSalaryTierByIdAsync(id);
                if (salaryTier == null) return NotFound("Salary tier not found.");
                return Ok(salaryTier);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the salary tier.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("Get-Deleted-Salary-Tiers")]
        public async Task<ActionResult<IEnumerable<SalaryTierForPreview>>> GetDeletedSalaryTiers()
        {
            try
            {
                var deletedSalaryTiers = await _salaryTierService.GetDeletedSalaryTiersAsync();
                return Ok(deletedSalaryTiers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting deleted salary tiers.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("Get-Salary-Tier-Report")]
        public async Task<IActionResult> GetSalaryTierReport()
        {
            try
            {
                var report = await _salaryTierService.GetReportSalaryTierAsync();
                return Ok(report);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating the salary tier report.");
                return StatusCode(500, "Internal server error occurred while generating the salary tier report.");
            }
        }


        [HttpPost("Add")]
        public async Task<IActionResult> AddSalaryTier([FromBody] SalaryTierForAdd salaryTierDto)
        {
            try
            {
                var salaryTier = await _salaryTierService.AddSalaryTierAsync(salaryTierDto);
                return Ok(salaryTier);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the salary tier.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateSalaryTier(int id, [FromBody] SalaryTierForUpdate salaryTierDto)
        {
            try
            {
                await _salaryTierService.UpdateSalaryTierAsync(id, salaryTierDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the salary tier.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteSalaryTier(int id)
        {
            try
            {
                await _salaryTierService.DeleteSalaryTierAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the salary tier.");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
