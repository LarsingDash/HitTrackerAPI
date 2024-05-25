﻿using HitTrackerAPI.Database;
using HitTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HitTrackerAPI.Repositories.SplitRepositories;

public class SplitRepository(HitTrackerContext context) : ISplitRepository
{
    public async Task<Split?> GetSplit(int id) =>
        await context.Splits.FirstOrDefaultAsync(split => split.SplitId == id);

    public async Task<int?> CreateSplit(Account account, int runId, string name)
    {
        //Test if name already exists
        var run = account.Runs?.FirstOrDefault(other => other.RunId == runId);
        if (run == null)
        {
            Console.WriteLine("Run with given runId not found");
            return null;
        }

        if (run.Splits?.FirstOrDefault(other => other.Name == name) != null)
        {
            Console.WriteLine("Split on this run with name already existed");
            return null;
        }

        //Create new split and add to db
        var result = await context.Splits.AddAsync(new Split { ParentId = runId, Name = name }); //Add to Splits
        run.Splits?.Add(result.Entity); //Add split to run in Runs
        await context.SaveChangesAsync();

        Console.WriteLine($"Created split: {result.Entity}");

        //Return id of newly created run
        return result.Entity.SplitId;
    }

    public async Task<bool> RenameSplit(Split split, Run parent, string name)
    {
        //Check if name is already taken
        if (parent.Splits != null && parent.Splits.Any(check => check.Name == name)) return false;

        //Change and save
        split.Name = name;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> MoveSplit(Split split, Run parent, int runPosition)
    {
        if (parent.Splits == null) return false;

        //Clamp within list bounds
        runPosition = int.Clamp(runPosition, 0, parent.Splits.Count - 1);

        //Convert Splits to a list, remove the item and place it at the correct index
        var splitsList = parent.Splits.ToList();
        if (!splitsList.Remove(split)) return false;
        splitsList.Insert(runPosition, split);

        //Replace indices the original collection
        for (var i = 0; i < splitsList.Count; i++)
            parent.Splits.Single(single => splitsList[i].SplitId == single.SplitId).Order = i;

        //Save changes
        await context.SaveChangesAsync();
        return true;
    }
}