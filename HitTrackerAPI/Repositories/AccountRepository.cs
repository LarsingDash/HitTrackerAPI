using HitTrackerAPI.Database;
using HitTrackerAPI.Models;

namespace HitTrackerAPI.Repositories;

public class AccountRepository(HitTrackerContext context) : IAccountRepository
{
    public async Task<bool> CreateAccount(Account account) 
    {
        await context.Accounts.AddAsync(account);
        return context.SaveChangesAsync().GetAwaiter().GetResult() > 0;
    }
}