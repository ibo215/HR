using Domain;

namespace HR.Services
{
    public interface ISalaryTierRepository
    {
        Task<IEnumerable<SalaryTier>> GetAllSalaryTiersAsync();
        Task<SalaryTier> GetSalaryTierByIdAsync(int id);
        Task AddSalaryTierAsync(SalaryTier salaryTier);
        Task UpdateSalaryTierAsync(SalaryTier salaryTier);
        Task DeleteSalaryTierAsync(int id);
    }

}
