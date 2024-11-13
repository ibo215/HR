using Domain;
using HR.Contexts;
using HR.Models;
using Microsoft.EntityFrameworkCore;

namespace HR.Repositoreies
{
    public class AccountRepository : IAccountRepository
    {
        private readonly HRContext _context;

        public AccountRepository(HRContext context)
        {
            _context = context;
        }

        public async Task<Account> GetByUserNameAsync(string username)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.UserName == username);
        }

        public async Task AddAccountAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAccountAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                account.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Account> GetByIdAsync(int id)
        {
            return await _context.Accounts
                .Where(a => a.IsActive)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
      

        //public async Task<Account> ValidateCredentialsAsync(string username, string password)
        //{
        //    var account = await GetByUserNameAsync(username);
        //    if (account != null && VerifyPasswordHash(password, account.PasswordHash))
        //    {
        //        return account;
        //    }
        //    return null;
        //}

        //private bool VerifyPasswordHash(string password, string storedHash)
        //{
        //    // منطق التحقق من كلمة المرور باستخدام التشفير
        //    return true;
        //}
    }

}
