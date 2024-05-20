using System.ComponentModel.DataAnnotations;

namespace HitTrackerAPI.Models;

/// <summary>
/// Model class for database table "Run"
/// <list type="bullet">
///     <item><see cref="RunId"/></item>
///     <item><see cref="Name"/></item>
/// </list>
/// </summary>
public sealed class Run
{
    /// <summary>
    /// Unique identifier for each run
    /// </summary>
    [Key]
    public int RunId { get; init; }
    
    /// <summary>
    /// Name for the run, also has to be unique
    /// </summary>
    [MaxLength(35)]
    public string Name { get; init; } = null!;
    
    /// <summary>
    /// Splits on a run
    /// </summary>
    public ICollection<Split>? Splits { get; init; }
}