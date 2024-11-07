using AutoMapper;
using Domain;
using Domain.dto.Profiles;
using HR.DTOs.EmployeeDTOs;
using HR.Repositoreies;
using HR.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IValidationService _validationService;
        private readonly IMapper _mapper;

        public EmployeeController(
            IEmployeeRepository employeeRepository,
            IValidationService validationService,
            IMapper mapper
            )
        {
            _employeeRepository = employeeRepository;
            _validationService = validationService;
            _mapper = mapper;
        }


        [HttpGet("Get-All")]
        public async Task<ActionResult<IEnumerable<EmployeeForPreview>>> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeForPreview>>(employees);
            return Ok(employeeDtos);
        }

 
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<EmployeeForPreview>> GetEmployeeById(int id)
        {
            if (!_validationService.ValidateId(id, out string errorMessage))
            {
                return BadRequest(errorMessage);
            }

            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound("Employee not found.");

            var employeeDto = _mapper.Map<EmployeeForPreview>(employee);
            return Ok(employeeDto);
        }

        // Get deleted employees
        [HttpGet("Get-Deleted-Employees")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetDeletedEmployees()
        {
            var deletedEmployees = await _employeeRepository.GetDeletedEmployeesAsync();

            var employeeDto = _mapper.Map<IEnumerable<EmployeeForPreview>>(deletedEmployees);


            return Ok(employeeDto);
        }


        [HttpPost("Add")]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeForAdd employeeDto)
        {
           
            if (!_validationService.ValidateCreate(employeeDto, out string errorMessage))
            {
                return BadRequest(errorMessage);
            }

            var employee = _mapper.Map<Employee>(employeeDto);
            await _employeeRepository.AddEmployeeAsync(employee);

            var employeeForPreview = _mapper.Map<EmployeeForPreview>(employee);
            return Ok(employeeForPreview);
        }


        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeForUpdate employeeDto)
        {
            if (!_validationService.ValidateUpdate(id, employeeDto, out string errorMessage))
            {
                return BadRequest(errorMessage);
            }

            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (existingEmployee == null)
            {
                return NotFound("Employee not found.");
            }

            // Map updated properties to the existing entity
            _mapper.Map(employeeDto, existingEmployee);
            await _employeeRepository.UpdateEmployeeAsync(existingEmployee);

            return NoContent();
        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeRepository.DeleteEmployeeAsync(id);
            return NoContent();
        }
    }

}
