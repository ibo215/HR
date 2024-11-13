using AutoMapper;
using Domain;

using HR.Repositoreies;
using HR.Services;
using HR.ViewModels;
using HR.ViewModels.DTOs.EmployeeDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace HR.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    //public class EmployeeController : ControllerBase
    //{
    //    private readonly IEmployeeRepository _employeeRepository;
    //    private readonly IValidationService _validationService;
    //    private readonly ILogger _logger;
    //    private readonly IMapper _mapper;

    //    public EmployeeController(
    //        IEmployeeRepository employeeRepository,
    //        IValidationService validationService,
    //        ILogger<EmployeeController> logger,
    //        IMapper mapper
    //        )
    //    {
    //        _employeeRepository = employeeRepository;
    //        _validationService = validationService;
    //        _logger = logger;
    //        _mapper = mapper;
    //    }


    //    [HttpGet("Get-All")]
    //    public async Task<ActionResult<IEnumerable<EmployeeForPreview>>> GetAllEmployees(int pageNumber = 1, int pageSize = 10)
    //    {
    //        try
    //        {

    //            var employees = await _employeeRepository.GetAllEmployeesAsync(pageNumber, pageSize);

    //            if (employees == null || !employees.Any())
    //            {
    //                _logger.LogWarning("No employees found.");
    //                return NotFound("No employees found.");
    //            }

    //            var employeeDtos = _mapper.Map<IEnumerable<EmployeeForPreview>>(employees);

    //            return Ok(employeeDtos);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, "An error occurred while getting employees.");
    //            return StatusCode(500, "Internal server error.");
    //        }
    //    }


    //    [HttpGet("Get/{id}")]
    //    public async Task<ActionResult<EmployeeForPreview>> GetEmployeeById(int id)
    //    {
    //        if (!_validationService.ValidateId(id, out string errorMessage))
    //        {
    //            return BadRequest(errorMessage);
    //        }

    //        var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
    //        if (employee == null) return NotFound("Employee not found.");

    //        var employeeDto = _mapper.Map<EmployeeForPreview>(employee);
    //        return Ok(employeeDto);
    //    }

    //    // Get deleted employees
    //    [HttpGet("Get-Deleted-Employees")]
    //    public async Task<ActionResult<IEnumerable<Employee>>> GetDeletedEmployees()
    //    {
    //        var deletedEmployees = await _employeeRepository.GetDeletedEmployeesAsync();

    //        var employeeDto = _mapper.Map<IEnumerable<EmployeeForPreview>>(deletedEmployees);


    //        return Ok(employeeDto);
    //    }


    //    [HttpPost("Add")]
    //    public async Task<IActionResult> AddEmployee([FromBody] EmployeeForAdd employeeDto)
    //    {

    //        if (!_validationService.ValidateCreate(employeeDto, out string errorMessage))
    //        {
    //            return BadRequest(errorMessage);
    //        }

    //        var employee = _mapper.Map<Employee>(employeeDto);
    //        await _employeeRepository.AddEmployeeAsync(employee);

    //        var employeeForPreview = _mapper.Map<EmployeeForPreview>(employee);
    //        return Ok(employeeForPreview);
    //    }


    //    [HttpPut("Update/{id}")]
    //    public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeForUpdate employeeDto)
    //    {
    //        if (!_validationService.ValidateUpdate(id, employeeDto, out string errorMessage))
    //        {
    //            return BadRequest(errorMessage);
    //        }

    //        var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(id);
    //        if (existingEmployee == null)
    //        {
    //            return NotFound("Employee not found.");
    //        }

    //        // Map updated properties to the existing entity
    //        _mapper.Map(employeeDto, existingEmployee);
    //        await _employeeRepository.UpdateEmployeeAsync(existingEmployee);

    //        return NoContent();
    //    }


    //    [HttpDelete("Delete/{id}")]
    //    public async Task<IActionResult> DeleteEmployee(int id)
    //    {
    //        await _employeeRepository.DeleteEmployeeAsync(id);
    //        return NoContent();
    //    }
    //}

    [ApiController]
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

        [HttpGet("Get-Deleted-Employees")]
        public async Task<ActionResult<IEnumerable<EmployeeForPreview>>> GetDeletedEmployees()
        {
            var deletedEmployees = await _employeeService.GetDeletedEmployeesAsync();
            return Ok(deletedEmployees);
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
