using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.DTOs.SalaryTierDTOs
{
    public class SalaryTierForPreview
    {
        public int SalaryTierId { get; set; }
        public string TierName { get; set; }
        public decimal SalaryAmount { get; set; }
    }

}
