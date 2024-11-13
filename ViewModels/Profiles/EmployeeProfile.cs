using AutoMapper;
using Domain;
using HR.ViewModels.DTOs.EmployeeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.ViewModels.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            //CreateMap<EmployeeForPreview, Employee>().ReverseMap();

            // For preview mapping
            CreateMap<EmployeeForPreview, Employee>().ReverseMap();

            // For adding new employee
            CreateMap<EmployeeForAdd, Employee>().ReverseMap();

            // For updating an existing employee
            CreateMap<EmployeeForUpdate, Employee>().ReverseMap();
        }
    }
}
