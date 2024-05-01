using HitTrackerAPI.Database;
using HitTrackerAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace HitTrackerAPI;

public static class SeedDb
{
    public static void SeedDatabase(IApplicationBuilder applicationBuilder)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<HitTrackerContext>();

        if (context is null)
            return;

        if (context.Accounts.Any())
            return;

        var account0 = new Account { AccountId = 0 };
        var account1 = new Account { AccountId = 1 };

        context.Accounts.AddRange(new List<Account> { account0, account1 });
        context.SaveChanges();
    }
}