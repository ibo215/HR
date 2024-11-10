using HR.ViewModels.DTOs.DepartmentDTOs;

namespace HR.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentForPreview>> GetAllDepartmentsAsync(int pageNumber, int pageSize);
        Task<DepartmentForPreview> GetDepartmentByIdAsync(int id);
        Task<IEnumerable<DepartmentForPreview>> GetDeletedDepartmentsAsync();
        Task<DepartmentForPreview> AddDepartmentAsync(DepartmentForAdd departmentDto);
        Task UpdateDepartmentAsync(int id, DepartmentForUpdate departmentDto);
        Task DeleteDepartmentAsync(int id);
    }
}
