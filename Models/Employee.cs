using System.Text.Json.Serialization;

namespace Domain
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public bool InActive { get; set; } = true;


        public int DepartmentId { get; set; }
        [JsonIgnore]
        public Department Department { get; set; }

        public int SalaryTierId { get; set; }
        [JsonIgnore]
        public SalaryTier SalaryTier { get; set; }
    }

}
