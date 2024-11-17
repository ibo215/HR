using AutoMapper;
using Domain;
using HR.Repositoreies;
using HR.ViewModels.DTOs.EmployeeDTOs;
using Microsoft.Extensions.Logging;

namespace HR.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<EmployeeForPreview>> GetAllEmployeesAsync(int pageNumber, int pageSize)
        {
            try
            {
                _logger.LogInformation("Fetching all employees (Page: {PageNumber}, Size: {PageSize})", pageNumber, pageSize);
                var employees = await _employeeRepository.GetAllEmployeesAsync(pageNumber, pageSize);
                return _mapper.Map<IEnumerable<EmployeeForPreview>>(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all employees.");
                throw; // Rethrow the exception to handle it further up the stack.
            }
        }

        public async Task<EmployeeForPreview> GetEmployeeByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Fetching employee with ID: {Id}", id);
                var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

                if (employee == null)
                {
                    _logger.LogWarning("Employee with ID: {Id} not found.", id);
                    throw new KeyNotFoundException("Employee not found.");
                }

                return _mapper.Map<EmployeeForPreview>(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching employee with ID: {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeForPreview>> GetDeletedEmployeesAsync()
        {
            try
            {
                _logger.LogInformation("Fetching deleted employees.");
                var employees = await _employeeRepository.GetDeletedEmployeesAsync();
                return _mapper.Map<IEnumerable<EmployeeForPreview>>(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching deleted employees.");
                throw;
            }
        }

        public async Task<EmployeeForPreview> AddEmployeeAsync(EmployeeForAdd employeeDto)
        {
            try
            {
                _logger.LogInformation("Adding new employee.");
                var employee = _mapper.Map<Employee>(employeeDto);
                await _employeeRepository.AddEmployeeAsync(employee);
                _logger.LogInformation("Employee added successfully");

                return _mapper.Map<EmployeeForPreview>(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new employee.");
                throw;
            }
        }

        public async Task UpdateEmployeeAsync(int id, EmployeeForUpdate employeeDto)
        {
            try
            {
                _logger.LogInformation("Updating employee with ID: {Id}", id);
                var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(id);
                if (existingEmployee == null)
                {
                    _logger.LogWarning("Employee with ID: {Id} not found.", id);
                    throw new KeyNotFoundException("Employee not found.");
                }

                await _employeeRepository.UpdateEmployeeAsync(_mapper.Map(employeeDto, existingEmployee));
                _logger.LogInformation("Employee with ID: {Id} updated successfully.", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating employee with ID: {Id}", id);
                throw;
            }
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting employee with ID: {Id}", id);
                await _employeeRepository.DeleteEmployeeAsync(id);
                _logger.LogInformation("Employee with ID: {Id} deleted successfully.", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting employee with ID: {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeForPreview>> SearchEmployeesAsync(string name)
        {
            try
            {
                _logger.LogInformation("Searching employees with name containing: {Name}", name);
                var employees = await _employeeRepository.SearchEmployeesAsync(name);
                return _mapper.Map<IEnumerable<EmployeeForPreview>>(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching employees with name containing: {Name}", name);
                throw;
            }
        }
    }
}
