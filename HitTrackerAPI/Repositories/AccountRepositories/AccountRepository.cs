using HitTrackerAPI.Models;
using HitTrackerAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace HitTrackerAPI.Repositories.AccountRepositories
{
    public class AccountRepository(HitTrackerContext context) : IAccountRepository
    {
        public async Task<Account?> GetAccount(int id) =>
            await context.Accounts.Include(account => account.Runs)!.ThenInclude(run => run.Splits)
                .FirstOrDefaultAsync(account => account.AccountId == id);

        public async Task<bool> CreateAccount(Account account)
        {
            var result = await context.Accounts.AddAsync(account);
            Console.WriteLine($"Created account: {result.Entity}");
            return context.SaveChangesAsync().GetAwaiter().GetResult() > 0;
        }
    }
}