using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

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
    /// Foreign key of the split's parent run
    /// </summary>
    [ForeignKey("Run")]
    public int ParentId { get; init; }

    /// <summary>
    /// Name for the split, also has to be unique
    /// </summary>
    [MaxLength(35)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Order of the split in the run
    /// </summary>
    public int Order { get; set; }
    
    public override string ToString() => JsonSerializer.Serialize(this, Program.Options);
}