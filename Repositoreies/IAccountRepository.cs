using HR.Models;

namespace HR.Repositoreies
{
    public interface IAccountRepository
    {
        Task<Account> GetByUserNameAsync(string username);
        Task<Account> GetByIdAsync(int id);
        Task AddAccountAsync(Account account);
        Task UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(int id);
        //Task<Account> ValidateCredentialsAsync(string username, string password);
    }

}
