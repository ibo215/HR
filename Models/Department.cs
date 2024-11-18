using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public bool InActive { get; set; } = true;

        [JsonIgnore]
        public ICollection<Employee> Employees { get; set; }
    }

}
