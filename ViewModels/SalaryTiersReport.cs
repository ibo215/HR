using HR.ViewModels.DTOs.DepartmentDTOs;

namespace HR.ViewModels
{
    public class SalaryTiersReport
    {
        public string TierName { get; set; }
        public decimal SalaryAmount { get; set; }
        public int EmployeeCount { get; set; }
        public decimal TotalSalary { get; set; }
        public List<DepartmentForPreview> DepartmentInfo { get; set; }
    }

}
