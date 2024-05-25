using HitTrackerAPI.Database;
using HitTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HitTrackerAPI.Repositories.HitRepositories;

public class HitRepository(HitTrackerContext context) : IHitRepository
{
    public async Task<int?> CreateHit(Split split, string message)
    {
        if (split.Hits == null) return null;

        await context.Hits.AddAsync(new Hit
        {
            SplitId = split.SplitId,
            Timestamp = DateTime.Now,
            Message = message,
        });

        await context.SaveChangesAsync();
        return split.Hits!.Count;
    }
}