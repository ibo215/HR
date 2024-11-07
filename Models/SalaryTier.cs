using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SalaryTier
    {
        public int SalaryTierId { get; set; }
        public string TierName { get; set; }
        public decimal SalaryAmount { get; set; }
        public bool IsActive { get; set; } = true;


        public ICollection<Employee> Employees { get; set; }
    }

}
