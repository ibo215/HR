using AutoMapper;
using Domain;
using HR.DTOs.DepartmentDTOs;
using HR.Repositoreies;
using HR.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IValidationService _validationService;
        private readonly IMapper _mapper;

        public DepartmentController(
            IDepartmentRepository departmentRepository,
            IValidationService validationService,
            IMapper mapper
        )
        {
            _departmentRepository = departmentRepository;
            _validationService = validationService;
            _mapper = mapper;
        }

        // Get all departments
        [HttpGet("Get-All")]
        public async Task<ActionResult<IEnumerable<DepartmentForPreview>>> GetAllDepartments()
        {
            var departments = await _departmentRepository.GetAllDepartmentsAsync();
            var departmentDtos = _mapper.Map<IEnumerable<DepartmentForPreview>>(departments);
            return Ok(departmentDtos);
        }

        // Get department by ID
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<DepartmentForPreview>> GetDepartmentById(int id)
        {
            if (!_validationService.ValidateId(id, out string errorMessage))
            {
                return BadRequest(errorMessage);
            }

            var department = await _departmentRepository.GetDepartmentByIdAsync(id);
            if (department == null) return NotFound("Department not found.");

            var departmentDto = _mapper.Map<DepartmentForPreview>(department);
            return Ok(departmentDto);
        }

        // Get deleted departments
        [HttpGet("Get-Deleted-Departments")]
        public async Task<ActionResult<Department>> GetDeletedDepartments()
        {
            var deletedDepartments = await _departmentRepository.GetDeletedDepartmentsAsync();
            var departmentDto = _mapper.Map<IEnumerable<DepartmentForPreview>>(deletedDepartments);


            return Ok(departmentDto);
        }

        // Add a new department
        [HttpPost("Add")]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentForAdd departmentDto)
        {
            if (!_validationService.ValidateCreate(departmentDto, out string errorMessage))
            {
                return BadRequest(errorMessage);
            }

            var department = _mapper.Map<Department>(departmentDto);
            await _departmentRepository.AddDepartmentAsync(department);

            var departmentForPreview = _mapper.Map<DepartmentForPreview>(department);
            return CreatedAtAction(nameof(GetDepartmentById), new { id = department.DepartmentId }, departmentForPreview);
        }

        // Update an existing department
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentForUpdate departmentDto)
        {
            if (!_validationService.ValidateUpdate(id, departmentDto, out string errorMessage))
            {
                return BadRequest(errorMessage);
            }

            var existingDepartment = await _departmentRepository.GetDepartmentByIdAsync(id);
            if (existingDepartment == null)
            {
                return NotFound("Department not found.");
            }

            // Map updated properties to the existing entity
            _mapper.Map(departmentDto, existingDepartment);
            await _departmentRepository.UpdateDepartmentAsync(existingDepartment);

            return NoContent();
        }

        // Delete a department
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            if (!_validationService.ValidateId(id, out string errorMessage))
            {
                return BadRequest(errorMessage);
            }

            await _departmentRepository.DeleteDepartmentAsync(id);
            return NoContent();
        }
    }

}
