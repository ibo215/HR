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

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
        }

        [HttpGet("Get-All")]
        public async Task<ActionResult<IEnumerable<EmployeeForPreview>>> GetAllEmployees(int pageNumber = 1, int pageSize = 10)
        {
         
            var employees = await _employeeService.GetAllEmployeesAsync(pageNumber, pageSize);

            if (!employees.Any()) return NotFound("No employees found.");
               
            return Ok(employees);

        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<EmployeeForPreview>> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound("Employee not found.");

            return Ok(employee);
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
            var employee = await _employeeService.AddEmployeeAsync(employeeDto);
            return Ok(employee);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeForUpdate employeeDto)
        {
            await _employeeService.UpdateEmployeeAsync(id, employeeDto);
            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return NoContent();  
        }
    }

}
