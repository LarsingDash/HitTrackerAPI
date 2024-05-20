using HitTrackerAPI.Models;

namespace HitTrackerAPI.Database;

public class MockDb
{
    public readonly List<Split> Splits = [];
    public readonly List<Run> Runs = [];
    public readonly List<Account> Accounts = [];

    public MockDb()
    {
        // ReSharper disable StringLiteralTypo
        //SPLITS
        Splits.AddRange(
        [
            new Split
            {
                Name = "Genichiro",
            },
            new Split
            {
                Name = "Ogre",
            },
            new Split
            {
                Name = "Gyoubu",
            },
        ]);

        //RUNS
        Runs.AddRange(
        [
            new Run
            {
                Name = "Dark Souls",
            },
            new Run
            {
                Name = "Sekiro",
                Splits =
                [
                    Splits[0],
                    Splits[1],
                    Splits[2],
                ],
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
                    Runs[1],
                ],
            },
            new Account
            {
                AccountId = 1,
            },
        ]);
    }
}