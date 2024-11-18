using Domain;
using HR.Contexts;
using HR.ViewModels.DTOs.EmployeeDTOs;
using Microsoft.EntityFrameworkCore;

namespace HR.Repositoreies
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HRContext _context;

        public EmployeeRepository(HRContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(int pageNumber, int pageSize)
        {
            return await _context.Employees
                .Where(e => e.InActive)
                .Include(e => e.Department)
                .Include(e => e.SalaryTier)
                .Skip((pageNumber - 1) * pageSize) 
                .Take(pageSize) 
                .ToListAsync();
        }


        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var emp =  await _context.Employees
                .Where(e => e.InActive)
                .Include(e => e.Department)
                .Include(e => e.SalaryTier)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            return emp;
        }
        public async Task<IEnumerable<Employee>> GetDeletedEmployeesAsync()
        {
            return await _context.Employees
                .Where(e => !e.InActive)
                .ToListAsync();
        }
        public async Task AddEmployeeAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                employee.InActive = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<EmployeeInfo>> SearchEmployeesAsync(string name)
        {
            var employees = await _context.Set<Employee>()
                .Where(e => e.Name.Contains(name))
                .Include(e => e.SalaryTier)
                .Include(e => e.Department)
                .Select(e => new EmployeeInfo
                {
                    EmployeeId = e.EmployeeId,
                    Name = e.Name,
                    Position = e.Position,
                    DepartmentName = e.Department.DepartmentName,
                    SalaryTierName = e.SalaryTier.TierName,
                    SalaryTierAmount = e.SalaryTier.SalaryAmount
                })
                .ToListAsync();

            return employees;
        }



    }

}
