using HR.ViewModels;
using HR.ViewModels.DTOs.SalaryTierDTOs;

namespace HR.Services
{
    public interface ISalaryTierService
    {
        Task<IEnumerable<SalaryTierForPreview>> GetAllSalaryTiersAsync(int pageNumber, int pageSize);
        Task<SalaryTierForPreview> GetSalaryTierByIdAsync(int id);
        Task<IEnumerable<SalaryTierForPreview>> GetDeletedSalaryTiersAsync();
        Task<SalaryTierForPreview> AddSalaryTierAsync(SalaryTierForAdd salaryTierDto);
        Task<IEnumerable<SalaryTiersReport>> GetReportSalaryTierAsync();

        Task UpdateSalaryTierAsync(int id, SalaryTierForUpdate salaryTierDto);
        Task DeleteSalaryTierAsync(int id);
    }
}
