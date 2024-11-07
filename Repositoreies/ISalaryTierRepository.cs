using Domain;

namespace HR.Repositoreies
{
    public interface ISalaryTierRepository
    {
        Task<IEnumerable<SalaryTier>> GetAllSalaryTiersAsync();
        Task<SalaryTier> GetSalaryTierByIdAsync(int id);
        Task<IEnumerable<SalaryTier>> GetSalaryReportAsync();
        Task<IEnumerable<SalaryTier>> GetDeletedSalaryTiersAsync();
        Task AddSalaryTierAsync(SalaryTier salaryTier);
        Task UpdateSalaryTierAsync(SalaryTier salaryTier);
        Task DeleteSalaryTierAsync(int id);
    }

}
