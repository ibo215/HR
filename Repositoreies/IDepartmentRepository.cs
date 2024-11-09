using Domain;

namespace HR.Repositoreies
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync(int pageNumber = 1, int pageSize = 10);
        Task<Department> GetDepartmentByIdAsync(int id);
        Task<IEnumerable<Department>> GetDeletedDepartmentsAsync();
        Task AddDepartmentAsync(Department department);
        Task UpdateDepartmentAsync(Department department);
        Task DeleteDepartmentAsync(int id);
    }

}
