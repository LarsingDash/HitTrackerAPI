using HitTrackerAPI.Models;

namespace HitTrackerAPI.Repositories;

public interface IAccountRepository
{
    Task<bool> CreateAccount(Account account);
}