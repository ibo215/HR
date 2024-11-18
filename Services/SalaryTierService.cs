using AutoMapper;
using Domain;
using HR.Repositoreies;
using HR.ViewModels.DTOs.SalaryTierDTOs;
using HR.ViewModels;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR.ViewModels.DTOs.DepartmentDTOs;

namespace HR.Services
{
    public class SalaryTierService : ISalaryTierService
    {
        private readonly ISalaryTierRepository _salaryTierRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SalaryTierService> _logger;

        public SalaryTierService(
            ISalaryTierRepository salaryTierRepository,
            IMapper mapper,
            ILogger<SalaryTierService> logger)
        {
            _salaryTierRepository = salaryTierRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<SalaryTierForPreview>> GetAllSalaryTiersAsync(int pageNumber, int pageSize)
        {
            try
            {
                _logger.LogInformation("Fetching all salary tiers (Page: {PageNumber}, Size: {PageSize})", pageNumber, pageSize);
                var salaryTiers = await _salaryTierRepository.GetAllSalaryTiersAsync(pageNumber, pageSize);
                return _mapper.Map<IEnumerable<SalaryTierForPreview>>(salaryTiers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching salary tiers.");
                throw;
            }
        }

        public async Task<SalaryTierForPreview> GetSalaryTierByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Fetching salary tier with ID: {Id}", id);
                var salaryTier = await _salaryTierRepository.GetSalaryTierByIdAsync(id);
                if (salaryTier == null)
                {
                    _logger.LogWarning("Salary tier with ID: {Id} not found.", id);
                    throw new KeyNotFoundException("Salary tier not found.");
                }

                return _mapper.Map<SalaryTierForPreview>(salaryTier);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching salary tier with ID: {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<SalaryTierForPreview>> GetDeletedSalaryTiersAsync()
        {
            try
            {
                _logger.LogInformation("Fetching deleted salary tiers.");
                var deletedSalaryTiers = await _salaryTierRepository.GetDeletedSalaryTiersAsync();
                return _mapper.Map<IEnumerable<SalaryTierForPreview>>(deletedSalaryTiers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching deleted salary tiers.");
                throw;
            }
        }

        public async Task<SalaryTierForPreview> AddSalaryTierAsync(SalaryTierForAdd salaryTierDto)
        {
            try
            {
                _logger.LogInformation("Adding new salary tier.");
                var salaryTier = _mapper.Map<SalaryTier>(salaryTierDto);
                await _salaryTierRepository.AddSalaryTierAsync(salaryTier);
                _logger.LogInformation("Added salary tier ");
                return _mapper.Map<SalaryTierForPreview>(salaryTier);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding salary tier.");
                throw;
            }
        }

        public async Task UpdateSalaryTierAsync(int id, SalaryTierForUpdate salaryTierDto)
        {
            try
            {
                _logger.LogInformation("Updating salary tier with ID: {Id}", id);
                var existingSalaryTier = await _salaryTierRepository.GetSalaryTierByIdAsync(id);
                if (existingSalaryTier == null)
                {
                    _logger.LogWarning("Salary tier with ID: {Id} not found.", id);
                    throw new KeyNotFoundException("Salary tier not found.");
                }

                _mapper.Map(salaryTierDto, existingSalaryTier);
                await _salaryTierRepository.UpdateSalaryTierAsync(existingSalaryTier);
                _logger.LogInformation("Updated salary tier with ID: {Id}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating salary tier with ID: {Id}", id);
                throw;
            }
        }

        public async Task DeleteSalaryTierAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting salary tier with ID: {Id}", id);
                await _salaryTierRepository.DeleteSalaryTierAsync(id);
                _logger.LogInformation("Deleted salary tier with ID: {Id}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting salary tier with ID: {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<SalaryTiersReport>> GetReportSalaryTierAsync()
        {
            try
            {
                _logger.LogInformation("Generating salary tier report.");
                var salaryTiers = await _salaryTierRepository.GetAllActiveSalaryTiersAsync();

                var salaryTiersReports = salaryTiers.Select(st => new SalaryTiersReport
                {
                    TierName = st.TierName,
                    SalaryAmount = st.SalaryAmount,
                    EmployeeCount = st.Employees.Count(e => e.InActive),
                    TotalSalary = st.Employees
                        .Where(e => e.InActive)
                        .Sum(e => st.SalaryAmount),
                    DepartmentInfo = st.Employees
                        .Where(e => e.InActive && e.Department?.InActive == true)
                        .Select(e => e.Department)
                        .Distinct()
                        .Select(d => new DepartmentForPreview
                        {
                            DepartmentId = d.DepartmentId,
                            DepartmentName = d.DepartmentName
                        })
                        .ToList()
                }).ToList();

                _logger.LogInformation("Salary tier report generated successfully.");
                return salaryTiersReports;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating salary tier report.");
                throw;
            }
        }
    }
}
