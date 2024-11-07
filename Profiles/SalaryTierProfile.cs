using AutoMapper;
using HR.DTOs.SalaryTierDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.dto.Profiles
{
    public class SalaryTierProfile : Profile
    {
        public SalaryTierProfile()
        {
            //CreateMap<SalaryTierForPreview, SalaryTier>().ReverseMap();

            // For preview mapping
            CreateMap<SalaryTierForPreview, SalaryTier>().ReverseMap();

            // For adding new salary tier
            CreateMap<SalaryTierForAdd, SalaryTier>().ReverseMap();

            // For updating an existing salary tier
            CreateMap<SalaryTierForUpdate, SalaryTier>().ReverseMap();
        }
    }
}
