using HitTrackerAPI.Database;
using HitTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HitTrackerAPI.Repositories.RunRepositories;

public class RunRepository(HitTrackerContext context) : IRunRepository
{
    public async Task<Run?> GetRun(int id) =>
        await context.Runs.FirstOrDefaultAsync(run => run.RunId == id);

    public async Task<int?> CreateRun(Account account, string name)
    {
        //Test if name already exists
        if (account.Runs?.FirstOrDefault(other => other.Name == name) != null)
        {
            Console.WriteLine("Run name already existed");
            return null;
        }

        //Create new run and add to db
        var result = await context.Runs.AddAsync(new Run { Name = name }); //Add to Runs
        account.Runs?.Add(result.Entity); //Add run to account in Accounts
        await context.SaveChangesAsync();

        Console.WriteLine($"Created run: {result.Entity}");

        // await context.Runs.ForEachAsync(Console.WriteLine); //Debug print

        //Return id of newly created run
        return result.Entity.RunId;
    }
}