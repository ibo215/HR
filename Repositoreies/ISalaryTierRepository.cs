using Domain;

namespace HR.Repositoreies
{
    public interface ISalaryTierRepository
    {
        Task<IEnumerable<SalaryTier>> GetAllSalaryTiersAsync(int pageNumber = 1, int pageSize = 10);
        Task<SalaryTier> GetSalaryTierByIdAsync(int id);
        Task<IEnumerable<SalaryTier>> GetSalaryReportAsync();
        Task<IEnumerable<SalaryTier>> GetDeletedSalaryTiersAsync();
        Task AddSalaryTierAsync(SalaryTier salaryTier);
        Task UpdateSalaryTierAsync(SalaryTier salaryTier);
        Task DeleteSalaryTierAsync(int id);
        Task<IEnumerable<SalaryTier>> GetAllActiveSalaryTiersAsync();
       
    }

}
