using HitTrackerAPI.Models;

namespace HitTrackerAPI.Repositories.RunRepositories;

public interface IRunRepository
{
    /// <summary>
    /// Tries to find a run with the given ID
    /// </summary>
    /// <param name="id">RunID for the target run</param>
    /// <returns>
    /// The <see cref="Run"/> that was found with the given ID. If no run was found with the given ID, returns null
    /// </returns>
    Task<Run?> GetRun(int id);
    
    /// <summary>
    /// Creates a run with the given name on the given account
    /// </summary>
    /// <param name="account">The account which will receive the new run</param>
    /// <param name="name">The name of the run that will be entered into the database</param>
    /// <returns>Success indicator</returns>
    Task<int?> CreateRun(Account account, string name);
}