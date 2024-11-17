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


        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger)
        {
            _departmentService = departmentService;

        }

        [Authorize]
        [HttpGet("Get-All")]
        public async Task<ActionResult<IEnumerable<DepartmentForPreview>>> GetAllDepartments(int pageNumber = 1, int pageSize = 10)
        {
            var departments = await _departmentService.GetAllDepartmentsAsync(pageNumber, pageSize);
            if (!departments.Any()) return NotFound("No departments found."); 
            return Ok(departments);
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<DepartmentForPreview>> GetDepartmentById(int id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            if (department == null) return NotFound("Department not found.");
            return Ok(department);   
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Get-Deleted-Departments")]
        public async Task<ActionResult<IEnumerable<DepartmentForPreview>>> GetDeletedDepartments()
        {
            var departments = await _departmentService.GetDeletedDepartmentsAsync();
            return Ok(departments);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentForAdd departmentDto)
        {
            var department = await _departmentService.AddDepartmentAsync(departmentDto);
            return Ok(department);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentForUpdate departmentDto)
        {
            await _departmentService.UpdateDepartmentAsync(id, departmentDto);
            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await _departmentService.DeleteDepartmentAsync(id);
            return NoContent();

        }
    }
}
