using Domain;
using HR.Contexts;
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
                .Where(e => e.IsActive)
                .Include(e => e.Department)
                .Include(e => e.SalaryTier)
                .Skip((pageNumber - 1) * pageSize) 
                .Take(pageSize) 
                .ToListAsync();
        }


        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees
                .Where(e => e.IsActive)
                .Include(e => e.Department)
                .Include(e => e.SalaryTier)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);
        }
        public async Task<IEnumerable<Employee>> GetDeletedEmployeesAsync()
        {
            return await _context.Employees
                .Where(e => !e.IsActive)
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
                employee.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }

        }

}
