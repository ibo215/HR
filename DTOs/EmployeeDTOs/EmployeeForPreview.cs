using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.DTOs.EmployeeDTOs
{
    public class EmployeeForPreview
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        //public bool IsActive { get; set; }
        public int DepartmentId { get; set; }
        public int SalaryTierId { get; set; }
    }

}
