using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HitTrackerAPI.Models;

/// <summary>
/// Model class for database table "Account"
/// <list type="bullet">
///     <item><see cref="AccountId"/></item>
///     <item><see cref="Runs"/></item>
/// </list>
/// </summary>
public sealed class Account
{
    /// <summary>
    /// Unique identifier for each account
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int AccountId { get; init; }

    /// <summary>
    /// Runs on an account
    /// </summary>
    public ICollection<Run>? Runs { get; init; }

    public override string ToString()
    {
        return $"AccountId: {AccountId}, Runs: [{string.Join(", ", Runs ?? [])}]";
    }
}