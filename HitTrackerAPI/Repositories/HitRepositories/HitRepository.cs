using HitTrackerAPI.Models;

namespace HitTrackerAPI.Repositories.HitRepositories;

public class HitRepository : IHitRepository
{
    public Task<int?> CreateHit(Split split, string message)
    {
        throw new NotImplementedException();
    }
}