using System.ComponentModel.DataAnnotations;

namespace HitTrackerAPI.Models;

/// <summary>
/// Model class for database table "Split"
/// <list type="bullet">
///     <item><see cref="SplitId"/></item>
///     <item><see cref="Name"/></item>
/// </list>
/// </summary>
public sealed class Split
{
    /// <summary>
    /// Unique identifier for each split
    /// </summary>
    [Key]
    public int SplitId { get; init; }

    /// <summary>
    /// Name for the split, also has to be unique
    /// </summary>
    [MaxLength(35)]
    public string Name { get; init; } = null!;
}