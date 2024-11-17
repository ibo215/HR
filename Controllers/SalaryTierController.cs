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
    [Authorize]
    [Route("api/[controller]")]
    public class SalaryTierController : ControllerBase
    {
        private readonly ISalaryTierService _salaryTierService;

        public SalaryTierController(ISalaryTierService salaryTierService, ILogger<SalaryTierController> logger)
        {
            _salaryTierService = salaryTierService;
        }

        [HttpGet("Get-All")]
        public async Task<ActionResult<IEnumerable<SalaryTierForPreview>>> GetAllSalaryTiers(int pageNumber = 1, int pageSize = 10)
        {
            var salaryTiers = await _salaryTierService.GetAllSalaryTiersAsync(pageNumber, pageSize);
            if (!salaryTiers.Any()) return NotFound("No salary tiers found.");

            return Ok(salaryTiers);
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<SalaryTierForPreview>> GetSalaryTierById(int id)
        {
            var salaryTier = await _salaryTierService.GetSalaryTierByIdAsync(id);
            if (salaryTier == null) return NotFound("Salary tier not found.");
            return Ok(salaryTier);      
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Get-Deleted-Salary-Tiers")]
        public async Task<ActionResult<IEnumerable<SalaryTierForPreview>>> GetDeletedSalaryTiers()
        {
            var deletedSalaryTiers = await _salaryTierService.GetDeletedSalaryTiersAsync();
            return Ok(deletedSalaryTiers);
        }

        [HttpGet("Get-Salary-Tier-Report")]
        public async Task<IActionResult> GetSalaryTierReport()
        {
            var report = await _salaryTierService.GetReportSalaryTierAsync();
            return Ok(report);
        }


        [HttpPost("Add")]
        public async Task<IActionResult> AddSalaryTier([FromBody] SalaryTierForAdd salaryTierDto)
        {
            var salaryTier = await _salaryTierService.AddSalaryTierAsync(salaryTierDto);
            return Ok(salaryTier);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateSalaryTier(int id, [FromBody] SalaryTierForUpdate salaryTierDto)
        {
            await _salaryTierService.UpdateSalaryTierAsync(id, salaryTierDto);
            return NoContent();    
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteSalaryTier(int id)
        {
            await _salaryTierService.DeleteSalaryTierAsync(id);
            return NoContent();
        }
    }
}
