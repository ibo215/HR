using Domain;
using HR.ViewModels.DTOs.EmployeeDTOs;

namespace HR.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeForPreview>> GetAllEmployeesAsync(int pageNumber, int pageSize);
        Task<EmployeeForPreview> GetEmployeeByIdAsync(int id);
        Task<IEnumerable<EmployeeForPreview>> GetDeletedEmployeesAsync();
        Task<IEnumerable<EmployeeInfo>> SearchEmployeesAsync(string name);
        Task<EmployeeForPreview> AddEmployeeAsync(EmployeeForAdd employeeDto);
        Task UpdateEmployeeAsync(int id, EmployeeForUpdate employeeDto);
        Task DeleteEmployeeAsync(int id);
    }
}
