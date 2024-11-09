using Domain;
using HR.Contexts;
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


        public async Task<IEnumerable<SalaryTier>> GetSalaryReportAsync()
        {
            return await _context.SalaryTiers.ToListAsync();
        }
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
    }

}
