using System.ComponentModel.DataAnnotations;

namespace HitTrackerAPI.Models;

public class Account
{
    [Key] public int AccountId { get; set; }
}