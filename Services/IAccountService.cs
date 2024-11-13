using HR.ViewModels.DTOs.AccountDTOs;

namespace HR.Services
{
    public interface IAccountService
    {
        Task<string> RegisterAccountAsync(AccountForAdd accountDto);
        Task<string> LoginAsync(string username, string password);
        Task DeleteMyAccountAsync(string username, string password);
    }

}
