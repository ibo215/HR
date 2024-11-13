namespace HR.ViewModels.Profiles
{
    using AutoMapper;
    using Domain;
    using HR.Models;
    using HR.ViewModels.DTOs.AccountDTOs;

    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            // For preview mapping
            CreateMap<AccountForPreview, Account>().ReverseMap();

            // For adding new account
            CreateMap<AccountForAdd, Account>().ReverseMap();

            // For updating an existing account
            CreateMap<AccountForUpdate, Account>().ReverseMap();
        }
    }

}
