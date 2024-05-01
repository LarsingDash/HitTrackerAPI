using HitTrackerAPI.Database;
using HitTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HitTrackerAPI.Repositories;

public class AccountRepository(HitTrackerContext context) : IAccountRepository
{
    public Task<Account?> GetAccount(int id) =>
        context.Accounts.FirstOrDefaultAsync(account => account.AccountId == id);
    
    public async Task<bool> CreateAccount(Account account)
    {
        await context.Accounts.AddAsync(account);
        return context.SaveChangesAsync().GetAwaiter().GetResult() > 0;
    }
}