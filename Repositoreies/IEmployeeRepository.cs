using Domain;
using HR.ViewModels.DTOs.EmployeeDTOs;
using Microsoft.AspNetCore.Mvc;

namespace HR.Repositoreies
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync(int pageNumber, int pageSize);
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<IEnumerable<Employee>> GetDeletedEmployeesAsync();
        Task<IEnumerable<Employee>> SearchEmployeesAsync(string name);
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int id);
    }

}
