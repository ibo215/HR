using AutoMapper;
using Domain;
using HR.Repositoreies;
using HR.ViewModels.DTOs.DepartmentDTOs;

namespace HR.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper, IValidationService validationService)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _validationService = validationService;
        }

        public async Task<IEnumerable<DepartmentForPreview>> GetAllDepartmentsAsync(int pageNumber, int pageSize)
        {
            var departments = await _departmentRepository.GetAllDepartmentsAsync(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<DepartmentForPreview>>(departments);
        }

        public async Task<DepartmentForPreview> GetDepartmentByIdAsync(int id)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(id);
            return _mapper.Map<DepartmentForPreview>(department);
        }

        public async Task<IEnumerable<DepartmentForPreview>> GetDeletedDepartmentsAsync()
        {
            var deletedDepartments = await _departmentRepository.GetDeletedDepartmentsAsync();
            return _mapper.Map<IEnumerable<DepartmentForPreview>>(deletedDepartments);
        }

        public async Task<DepartmentForPreview> AddDepartmentAsync(DepartmentForAdd departmentDto)
        {
            var department = _mapper.Map<Department>(departmentDto);
            await _departmentRepository.AddDepartmentAsync(department);
            return _mapper.Map<DepartmentForPreview>(department);
        }

        public async Task UpdateDepartmentAsync(int id, DepartmentForUpdate departmentDto)
        {
            var existingDepartment = await _departmentRepository.GetDepartmentByIdAsync(id);
            if (existingDepartment == null) throw new KeyNotFoundException("Department not found.");

            _mapper.Map(departmentDto, existingDepartment);
            await _departmentRepository.UpdateDepartmentAsync(existingDepartment);
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            await _departmentRepository.DeleteDepartmentAsync(id);
        }
    }
}
