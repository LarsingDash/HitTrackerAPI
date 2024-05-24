﻿using HitTrackerAPI.Models;

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
                SplitId = 1,
                ParentId = 2,
                Name = "Genichiro",
            },
            new Split
            {
                SplitId = 2,
                ParentId = 2,
                Name = "Ogre",
            },
            new Split
            {
                SplitId = 3,
                ParentId = 2,
                Name = "Gyoubu",
            },
        ]);

        //RUNS
        Runs.AddRange(
        [
            new Run
            {
                RunId = 1,
                Name = "Dark Souls",
            },
            new Run
            {
                RunId = 2,
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