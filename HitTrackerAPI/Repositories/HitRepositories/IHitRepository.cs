using HitTrackerAPI.Models;

namespace HitTrackerAPI.Repositories.HitRepositories;

public interface IHitRepository
{
    /// <summary>
    /// Creates a hit on the split with the given message 
    /// </summary>
    /// <param name="split">The which which will receive the new hit</param>
    /// <param name="message">The message with which the hit will be entered into the database</param>
    /// <returns>Id of the newly created hit, null if no hit could be created</returns>
    Task<int?> CreateHit(Split split, string message);
}