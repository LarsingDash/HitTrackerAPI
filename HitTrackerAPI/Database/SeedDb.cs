using HitTrackerAPI.Repositories;

namespace HitTrackerAPI.Database;

public static class SeedDb
{
    public static void SeedDatabase(IApplicationBuilder applicationBuilder)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<HitTrackerContext>();

        if (context != null && !context.Accounts.Any()) SeedingRun(context);
    }

    public static void SeedingRun(HitTrackerContext context)
    {
        var mock = new MockDb();

        context.Accounts.AddRange(mock.Accounts);
        context.Runs.AddRange(mock.Runs);
        context.SaveChanges();
    }
}