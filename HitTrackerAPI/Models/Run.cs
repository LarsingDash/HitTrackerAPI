using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HitTrackerAPI.Models;

/// <summary>
/// Model class for database table "Run"
/// <list type="bullet">
///     <item><see cref="RunId"/></item>
///     <item><see cref="Name"/></item>
/// </list>
/// </summary>
public class Run
{
    /// <summary>
    /// Unique identifier for each run
    /// </summary>
    [Key]
    public int RunId { get; init; }
    
    /// <summary>
    /// Name for the run, also has to be unique
    /// </summary>
    public string Name { get; init; }

    public override string ToString()
    {
        return $"Run {RunId}: {Name}";
    }
}