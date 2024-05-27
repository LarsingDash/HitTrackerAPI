using HitTrackerAPI.Database;
using HitTrackerAPI.Models;

namespace HitTrackerAPI.Repositories.HitRepositories;

public class HitRepository(HitTrackerContext context) : IHitRepository
{
    public async Task<int?> CreateHit(Split split, string message)
    {
        //SafetyCheck
        if (split.Hits == null) return null;

        //Create hit and add to split 
        await context.Hits.AddAsync(new Hit
        {
            SplitId = split.SplitId,
            Timestamp = DateTime.Now,
            Message = message,
        });

        //Save
        await context.SaveChangesAsync();
        return split.Hits!.Count;
    }

    public async Task<bool> UndoHit(Split split)
    {
        //Safety Check
        if (split.Hits?.Count == 0) return false;

        //Find latest hit on split
        var last = split.Hits?.MaxBy(hit => hit.Timestamp);
        if (last == null) return false;

        //Remove
        split.Hits?.Remove(last);

        //Save
        await context.SaveChangesAsync();
        return true;
    }
}