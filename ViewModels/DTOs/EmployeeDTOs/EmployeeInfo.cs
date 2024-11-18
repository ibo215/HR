namespace HR.ViewModels.DTOs.EmployeeDTOs
{
    public class EmployeeInfo
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string DepartmentName { get; set; }
        public string SalaryTierName { get; set; }
        public decimal SalaryTierAmount { get; set; }
    }

}
