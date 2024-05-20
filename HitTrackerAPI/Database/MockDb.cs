using HitTrackerAPI.Models;

namespace HitTrackerAPI.Repositories;

public class MockDb
{
    public readonly List<Account> Accounts = [];
    public readonly List<Run> Runs = [];
    
    public MockDb()
    {
        //RUNS
        Runs.AddRange(
        [
            new Run
            {
                Name = "Dark Souls",
            },
        ]);
        
        //ACCOUNTS
        Accounts.AddRange(
        [
            new Account
            {
                AccountId = 0,
                Runs =
                [
                    Runs[0],
                ],
            },
            new Account
            {
                AccountId = 1,
            },
        ]);
    }
}