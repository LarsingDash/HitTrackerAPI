﻿using HitTrackerAPI.Models;

namespace HitTrackerAPI.Database;

public class MockDb
{
    public readonly List<Hit> Hits = [];
    public readonly List<Split> Splits = [];
    public readonly List<Run> Runs = [];
    public readonly List<Account> Accounts = [];

    // ReSharper disable StringLiteralTypo
    public MockDb()
    {
        //HITS
        Hits.AddRange([
            new Hit
            {
                SplitId = 1,
                Timestamp = DateTime.Now.AddHours(-3),
                Message = "Test hit 1: Split 1"
            },
            new Hit
            {
                SplitId = 3,
                Timestamp = DateTime.Now.AddHours(-2),
                Message = "Test hit 2: Split 3"
            },
            new Hit
            {
                SplitId = 1,
                Timestamp = DateTime.Now.AddHours(-1),
                Message = "Test hit 3: Split 1 again"
            },
        ]);

        //SPLITS
        Splits.AddRange(
        [
            new Split
            {
                SplitId = 1,
                ParentId = 2,
                Name = "Split 1",
                Order = 0,
                Hits =
                [
                    Hits[0],
                    Hits[2],
                ]
            },
            new Split
            {
                SplitId = 2,
                ParentId = 2,
                Name = "Split 2",
                Order = 1,
            },
            new Split
            {
                SplitId = 3,
                ParentId = 2,
                Name = "Split 3",
                Order = 2,
                Hits =
                [
                    Hits[1],
                ],
            },
        ]);

        //RUNS
        Runs.AddRange(
        [
            new Run
            {
                RunId = 1,
                Name = "Game 1",
            },
            new Run
            {
                RunId = 2,
                Name = "Game 2",
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