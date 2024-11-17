using AutoMapper;
using Domain;
using HR.Repositoreies;
using HR.ViewModels.DTOs.DepartmentDTOs;
using Microsoft.Extensions.Logging;

namespace HR.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentService> _logger;

        public DepartmentService(
            IDepartmentRepository departmentRepository,
            IMapper mapper,
            ILogger<DepartmentService> logger)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<DepartmentForPreview>> GetAllDepartmentsAsync(int pageNumber, int pageSize)
        {
            try
            {
                _logger.LogInformation("Fetching all departments (Page: {PageNumber}, Size: {PageSize})", pageNumber, pageSize);
                var departments = await _departmentRepository.GetAllDepartmentsAsync(pageNumber, pageSize);
                return _mapper.Map<IEnumerable<DepartmentForPreview>>(departments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all departments.");
                throw;
            }
        }

        public async Task<DepartmentForPreview> GetDepartmentByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Fetching department with ID: {Id}", id);
                var department = await _departmentRepository.GetDepartmentByIdAsync(id);

                if (department == null)
                {
                    _logger.LogWarning("Department with ID: {Id} not found.", id);
                    throw new KeyNotFoundException("Department not found.");
                }

                return _mapper.Map<DepartmentForPreview>(department);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching department with ID: {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<DepartmentForPreview>> GetDeletedDepartmentsAsync()
        {
            try
            {
                _logger.LogInformation("Fetching deleted departments.");
                var deletedDepartments = await _departmentRepository.GetDeletedDepartmentsAsync();
                return _mapper.Map<IEnumerable<DepartmentForPreview>>(deletedDepartments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching deleted departments.");
                throw;
            }
        }

        public async Task<DepartmentForPreview> AddDepartmentAsync(DepartmentForAdd departmentDto)
        {
            try
            {
                _logger.LogInformation("Adding new department.");
                var department = _mapper.Map<Department>(departmentDto);
                await _departmentRepository.AddDepartmentAsync(department);
                _logger.LogInformation("Department added successfully ");

                return _mapper.Map<DepartmentForPreview>(department);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new department.");
                throw;
            }
        }

        public async Task UpdateDepartmentAsync(int id, DepartmentForUpdate departmentDto)
        {
            try
            {
                _logger.LogInformation("Updating department with ID: {Id}", id);
                var existingDepartment = await _departmentRepository.GetDepartmentByIdAsync(id);

                if (existingDepartment == null)
                {
                    _logger.LogWarning("Department with ID: {Id} not found.", id);
                    throw new KeyNotFoundException("Department not found.");
                }

                _mapper.Map(departmentDto, existingDepartment);
                await _departmentRepository.UpdateDepartmentAsync(existingDepartment);
                _logger.LogInformation("Department with ID: {Id} updated successfully.", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating department with ID: {Id}", id);
                throw;
            }
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting department with ID: {Id}", id);
                await _departmentRepository.DeleteDepartmentAsync(id);
                _logger.LogInformation("Department with ID: {Id} deleted successfully.", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting department with ID: {Id}", id);
                throw;
            }
        }
    }
}
