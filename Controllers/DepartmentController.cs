using AutoMapper;
using Domain;
using HR.Repositoreies;
using HR.Services;
using HR.ViewModels;
using HR.ViewModels.DTOs.DepartmentDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace HR.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IValidationService _validationService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IMapper _mapper;

        public DepartmentController(
            IDepartmentRepository departmentRepository,
            IValidationService validationService,
            ILogger<DepartmentController> logger,
            IMapper mapper
        )
        {
            _departmentRepository = departmentRepository;
            _validationService = validationService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("Get-All")]
        public async Task<ActionResult<IEnumerable<DepartmentForPreview>>> GetAllDepartments(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var departments = await _departmentRepository.GetAllDepartmentsAsync(pageNumber, pageSize);

                if (departments == null || !departments.Any())
                {
                    _logger.LogWarning("No departments found.");
                    return NotFound("No departments found.");
                }

                var departmentDtos = _mapper.Map<IEnumerable<DepartmentForPreview>>(departments);


                return Ok(departmentDtos);
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
            if (!_validationService.ValidateId(id, out string errorMessage))
            {
                return BadRequest(errorMessage);
            }

            var department = await _departmentRepository.GetDepartmentByIdAsync(id);
            if (department == null) return NotFound("Department not found.");

            var departmentDto = _mapper.Map<DepartmentForPreview>(department);
            return Ok(departmentDto);
        }

        [HttpGet("Get-Deleted-Departments")]
        public async Task<ActionResult<IEnumerable<DepartmentForPreview>>> GetDeletedDepartments()
        {
            var deletedDepartments = await _departmentRepository.GetDeletedDepartmentsAsync();
            var departmentDtos = _mapper.Map<IEnumerable<DepartmentForPreview>>(deletedDepartments);
            return Ok(departmentDtos);
        }

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
            return Ok(departmentForPreview);
        }

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

            _mapper.Map(departmentDto, existingDepartment);
            await _departmentRepository.UpdateDepartmentAsync(existingDepartment);

            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await _departmentRepository.DeleteDepartmentAsync(id);
            return NoContent();
        }
    }

}
