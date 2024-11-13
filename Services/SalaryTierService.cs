namespace HR.Services
{
    using AutoMapper;
    using Domain;
    using HR.Repositoreies;
    using HR.ViewModels.DTOs.SalaryTierDTOs;
    using HR.ViewModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HR.ViewModels.DTOs.DepartmentDTOs;

    public class SalaryTierService : ISalaryTierService
    {
        private readonly ISalaryTierRepository _salaryTierRepository;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public SalaryTierService(ISalaryTierRepository salaryTierRepository, IMapper mapper, IValidationService validationService)
        {
            _salaryTierRepository = salaryTierRepository;
            _mapper = mapper;
            _validationService = validationService;
        }

        public async Task<IEnumerable<SalaryTierForPreview>> GetAllSalaryTiersAsync(int pageNumber, int pageSize)
        {
            var salaryTiers = await _salaryTierRepository.GetAllSalaryTiersAsync(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<SalaryTierForPreview>>(salaryTiers);
        }

        public async Task<SalaryTierForPreview> GetSalaryTierByIdAsync(int id)
        {
            var salaryTier = await _salaryTierRepository.GetSalaryTierByIdAsync(id);
            return _mapper.Map<SalaryTierForPreview>(salaryTier);
        }

        public async Task<IEnumerable<SalaryTierForPreview>> GetDeletedSalaryTiersAsync()
        {
            var deletedSalaryTiers = await _salaryTierRepository.GetDeletedSalaryTiersAsync();
            return _mapper.Map<IEnumerable<SalaryTierForPreview>>(deletedSalaryTiers);
        }

        public async Task<SalaryTierForPreview> AddSalaryTierAsync(SalaryTierForAdd salaryTierDto)
        {
            var salaryTier = _mapper.Map<SalaryTier>(salaryTierDto);
            await _salaryTierRepository.AddSalaryTierAsync(salaryTier);
            return _mapper.Map<SalaryTierForPreview>(salaryTier);
        }

        public async Task UpdateSalaryTierAsync(int id, SalaryTierForUpdate salaryTierDto)
        {
            var existingSalaryTier = await _salaryTierRepository.GetSalaryTierByIdAsync(id);
            if (existingSalaryTier == null) throw new KeyNotFoundException("Salary tier not found.");

            _mapper.Map(salaryTierDto, existingSalaryTier);
            await _salaryTierRepository.UpdateSalaryTierAsync(existingSalaryTier);
        }

        public async Task DeleteSalaryTierAsync(int id)
        {
            await _salaryTierRepository.DeleteSalaryTierAsync(id);
        }

        public async Task<IEnumerable<SalaryTiersReport>> GetReportSalaryTierAsync()
        {
            var salaryTiers = await _salaryTierRepository.GetAllActiveSalaryTiersAsync(); 
            var salaryTiersReports = salaryTiers.Select(st => new SalaryTiersReport
            {
                TierName = st.TierName,
                SalaryAmount = st.SalaryAmount,
                EmployeeCount = st.Employees.Count(e => e.IsActive),
                TotalSalary = st.Employees
                    .Where(e => e.IsActive)
                    .Sum(e => st.SalaryAmount),
                DepartmentInfo = st.Employees
                    .Where(e => e.IsActive && e.Department.IsActive)
                    .Select(e => e.Department)
                    .Distinct()
                    .Select(d => new DepartmentForPreview
                    {
                        DepartmentId = d.DepartmentId,
                        DepartmentName = d.DepartmentName
                    })
                    .ToList()
            }).ToList();

            return salaryTiersReports;
            
        }
    }
}

