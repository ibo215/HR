﻿using AutoMapper;
using Domain;
using HR.ViewModels.DTOs.DepartmentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HR.ViewModels.Profiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            //CreateMap<DepartmentForPreview, Department>().ReverseMap();

            // For preview mapping
            CreateMap<DepartmentForPreview, Department>().ReverseMap();

            // For adding new department
            CreateMap<DepartmentForAdd, Department>().ReverseMap();

            // For updating an existing department
            CreateMap<DepartmentForUpdate, Department>().ReverseMap();
        }
    }
}
