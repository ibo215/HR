using AutoMapper;
using Domain;

using HR.Repositoreies;
using HR.Services;
using HR.ViewModels;
using HR.ViewModels.DTOs.EmployeeDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace HR.Controllers
{
  
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpGet("Get-All")]
        public async Task<ActionResult<IEnumerable<EmployeeForPreview>>> GetAllEmployees(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var employees = await _employeeService.GetAllEmployeesAsync(pageNumber, pageSize);

                if (!employees.Any())
                {
                    _logger.LogWarning("No employees found.");
                    return NotFound("No employees found.");
                }

                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting employees.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<EmployeeForPreview>> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                if (employee == null) return NotFound("Employee not found.");

                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the employee.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Get-Deleted-Employees")]
        public async Task<ActionResult<IEnumerable<EmployeeForPreview>>> GetDeletedEmployees()
        {
            var deletedEmployees = await _employeeService.GetDeletedEmployeesAsync();
            return Ok(deletedEmployees);
        }

        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<EmployeeForPreview>>> SearchEmployees(string name)
        {
            var Employees = await _employeeService.SearchEmployeesAsync(name);
            return Ok(Employees);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeForAdd employeeDto)
        {
            try
            {
                var employee = await _employeeService.AddEmployeeAsync(employeeDto);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the employee.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeForUpdate employeeDto)
        {
            try
            {
                await _employeeService.UpdateEmployeeAsync(id, employeeDto);
                return NoContent();
            }
            
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the employee.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the employee.");
                return StatusCode(500, "Internal server error.");
            }
        }
    }

}
