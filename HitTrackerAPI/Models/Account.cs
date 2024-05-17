using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HitTrackerAPI.Models
{
    /// <summary>
    /// Model class for database table "Account"
    /// <list type="bullet">
    ///     <item><see cref="AccountId"/></item>
    /// </list>
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Unique identifier for each account
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccountId { get; init; }
    }
}