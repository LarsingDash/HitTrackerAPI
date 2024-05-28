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
    /// <param name="run">Id of the run which the split will belong to</param>
    /// <param name="name">Name of the split that will be created</param>
    /// <returns>Id of the newly created split, null if no run could be created</returns>
    Task<int?> CreateSplit(Run run, string name);

    /// <summary>
    /// Renames the split with the given id to the given name
    /// </summary>
    /// <param name="split">The split to rename</param>
    /// <param name="parent">Parent run to the split, necessary for checking if the name is taken</param>
    /// <param name="name">New name for the split, must not be already taken</param>
    /// <returns>Success Indication</returns>
    Task<bool> RenameSplit(Split split, Run parent, string name);

    /// <summary>
    /// Moves the split to the given position
    /// </summary>
    /// <param name="split">The split to move</param>
    /// <param name="parent">Parent run to the split, necessary for moving other splits</param>
    /// <param name="runPosition">The new position in the run for the split</param>
    /// <returns>Success Indication</returns>
    Task<bool> MoveSplit(Split split, Run parent, int runPosition);

    /// <summary>
    /// Tries to delete the split with the given ID
    /// </summary>
    /// <param name="split">The split to delete</param>
    /// <param name="parent">Parent run to the split, necessary for moving other splits</param>
    /// <returns>Success Indication</returns>
    Task<bool> DeleteSplit(Split split, Run parent);
}