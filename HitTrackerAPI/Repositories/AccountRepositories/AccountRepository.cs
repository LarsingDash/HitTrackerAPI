using HitTrackerAPI.Models;
using HitTrackerAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace HitTrackerAPI.Repositories.AccountRepositories
{
    public class AccountRepository(HitTrackerContext context) : IAccountRepository
    {
        public async Task<Account?> GetAccount(int id) =>
            await context.Accounts.Include(account => account.Runs).FirstOrDefaultAsync(account => account.AccountId == id);

        public async Task<bool> CreateAccount(Account account)
        {
            await context.Accounts.AddAsync(account);
            return context.SaveChangesAsync().GetAwaiter().GetResult() > 0;
        }
    }
}