namespace HR.ViewModels.DTOs.EmployeeDTOs
{
    public class EmployeeForUpdate
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }

        public int DepartmentId { get; set; }
        public int SalaryTierId { get; set; }
    }
}
