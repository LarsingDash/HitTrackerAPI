using HitTrackerAPI.Models;

namespace HitTrackerAPI.Repositories.SplitRepositories;

public interface ISplitRepository
{
    /// <summary>
    /// Tries to find a split with the given ID
    /// </summary>
    /// <param name="id">SplitID for the target split</param>
    /// <returns>
    /// The <see cref="Split"/> that was found with the given ID. If no split was found with the given ID, returns null
    /// </returns>
    Task<Split?> GetSplit(int id);
    
    /// <summary>
    /// Creates a split with the given name on the given account
    /// </summary>
    /// <param name="account">The account which will receive the new split</param>
    /// <param name="runId">Id of the run which the split will belong to</param>
    /// <param name="name">Name of the split that will be created</param>
    /// <returns>Success indicator</returns>
    Task<int?> CreateSplit(Account account, int runId, string name);
}