using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HitTrackerAPI.Models;

/// <summary>
/// Model class for database table "Hit"
/// <list type="bullet">
///     <item><see cref="SplitId"/></item>
///     <item><see cref="Timestamp"/></item>
///     <item><see cref="Message"/></item>
/// </list>
/// </summary>
[PrimaryKey(nameof(SplitId), nameof(Timestamp))]
public class Hit
{
    /// <summary>
    /// Id of the parenting Split
    /// </summary>
    [ForeignKey("Split")] public int SplitId { get; init; }
    
    /// <summary>
    /// Timestamp of the registration of this hit
    /// </summary>
    public DateTime Timestamp { get; init; }

    /// <summary>
    /// Optional message for the hit
    /// </summary>
    [MaxLength(150)] public string Message { get; init; } = null!;
}