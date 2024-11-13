using Domain;
using HR.Contexts;
using HR.ViewModels;

//using HR.DTOs.SalaryTierDTOs;
using Microsoft.EntityFrameworkCore;

namespace HR.Repositoreies
{
    public class SalaryTierRepository : ISalaryTierRepository
    {
        private readonly HRContext _context;

        public SalaryTierRepository(HRContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SalaryTier>> GetAllSalaryTiersAsync(int pageNumber, int pageSize)
        {
            return await _context.SalaryTiers
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<SalaryTier> GetSalaryTierByIdAsync(int id)
        {
            return await _context.SalaryTiers.Where(t => t.IsActive).FirstOrDefaultAsync(t =>t.SalaryTierId == id);
        }


        //public async Task<IEnumerable<SalaryTier>> GetSalaryReportAsync()
        //{
        //    return await _context.SalaryTiers.ToListAsync();
        //}
        //public async Task<IEnumerable<SalaryTiersReport>> GetReportSalaryTierAsync()
        //{
        //    try
        //    {
        //        var salaryTiers = await _context.SalaryTiers
        //            .Include(st => st.Employees)
        //                .ThenInclude(e => e.Department)
        //            .ToListAsync();

        //        var salaryTiersReports = salaryTiers.Select(st => new SalaryTiersReport
        //        {
        //            TierName = st.TierName,
        //            BaseSalary = st.BaseSalary,
        //            Bonus = st.Bonus,
        //            EmployeeCount = st.Employees.Count,
        //            TotalSalary = st.Employees.Sum(e => st.BaseSalary + st.Bonus),

        //            DepartmentTotalSalaries = st.Employees
        //                .GroupBy(e => e.Department.DepartmentId)
        //                .Select(g => new DepartmentSalaryInfo
        //                {
        //                    DepartmentName = g.First().Department.DepartmetnName,
        //                    TotalDepartmentSalary = g.Sum(e => st.BaseSalary + st.Bonus)
        //                })
        //                .ToList()
        //        }).ToList();

        //        return salaryTiersReports;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogCritical(ex, "Error occurred in SalaryTiersRepository in GetReportSalaryTierAsync method");
        //        return Enumerable.Empty<SalaryTiersReportResponse>();
        //    }
        //}

        public async Task<IEnumerable<SalaryTier>> GetDeletedSalaryTiersAsync()
        {
            return await _context.SalaryTiers
                .Where(st => !st.IsActive)
                .ToListAsync();
        }

        public async Task AddSalaryTierAsync(SalaryTier salaryTier)
        {
            await _context.SalaryTiers.AddAsync(salaryTier);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSalaryTierAsync(SalaryTier salaryTier)
        {
            _context.SalaryTiers.Update(salaryTier);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSalaryTierAsync(int id)
        {
            var salaryTier = await _context.SalaryTiers.FindAsync(id);
            if (salaryTier != null)
            {
                salaryTier.IsActive = false;
                 await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<SalaryTier>> GetAllActiveSalaryTiersAsync()
        {
            return await _context.SalaryTiers
                .Where(st => st.IsActive) 
                .Include(st => st.Employees)
                .ThenInclude(e => e.Department)   
                .ToListAsync();
        }

        public Task<IEnumerable<SalaryTier>> GetSalaryReportAsync()
        {
            throw new NotImplementedException();
        }
    }

}
