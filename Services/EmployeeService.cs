using AutoMapper;
using Domain;
using HR.Repositoreies;
using HR.ViewModels.DTOs.EmployeeDTOs;

namespace HR.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, IValidationService validationService)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _validationService = validationService;
        }

        public async Task<IEnumerable<EmployeeForPreview>> GetAllEmployeesAsync(int pageNumber, int pageSize)
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<EmployeeForPreview>>(employees);
        }

        public async Task<EmployeeForPreview> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            return _mapper.Map<EmployeeForPreview>(employee);
        }

        public async Task<IEnumerable<EmployeeForPreview>> GetDeletedEmployeesAsync()
        {
            var employees = await _employeeRepository.GetDeletedEmployeesAsync();
            return _mapper.Map<IEnumerable<EmployeeForPreview>>(employees);
        }

        public async Task<EmployeeForPreview> AddEmployeeAsync(EmployeeForAdd employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            await _employeeRepository.AddEmployeeAsync(employee);
            return _mapper.Map<EmployeeForPreview>(employee);
        }

        public async Task UpdateEmployeeAsync(int id, EmployeeForUpdate employeeDto)
        {
            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (existingEmployee == null) throw new KeyNotFoundException("Employee not found.");

            _mapper.Map(employeeDto, existingEmployee);
            await _employeeRepository.UpdateEmployeeAsync(existingEmployee);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _employeeRepository.DeleteEmployeeAsync(id);
        }
    }
}
