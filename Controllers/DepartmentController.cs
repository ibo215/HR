using AutoMapper;
using Domain;
using HR.Repositoreies;
using HR.Services;
using HR.ViewModels;
using HR.ViewModels.DTOs.DepartmentDTOs;
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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger)
        {
            _departmentService = departmentService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("Get-All")]
        public async Task<ActionResult<IEnumerable<DepartmentForPreview>>> GetAllDepartments(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var departments = await _departmentService.GetAllDepartmentsAsync(pageNumber, pageSize);
                if (!departments.Any())
                {
                    _logger.LogWarning("No departments found.");
                    return NotFound("No departments found.");
                }
                return Ok(departments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting departments.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<DepartmentForPreview>> GetDepartmentById(int id)
        {
            try
            {
                var department = await _departmentService.GetDepartmentByIdAsync(id);
                if (department == null) return NotFound("Department not found.");
                return Ok(department);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the department.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Get-Deleted-Departments")]
        public async Task<ActionResult<IEnumerable<DepartmentForPreview>>> GetDeletedDepartments()
        {
            try
            {
                var departments = await _departmentService.GetDeletedDepartmentsAsync();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting deleted departments.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentForAdd departmentDto)
        {
            try
            {
                var department = await _departmentService.AddDepartmentAsync(departmentDto);
                return Ok(department);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the department.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentForUpdate departmentDto)
        {
            try
            {
                await _departmentService.UpdateDepartmentAsync(id, departmentDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Department not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the department.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                await _departmentService.DeleteDepartmentAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the department.");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
