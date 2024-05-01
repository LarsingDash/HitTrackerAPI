using HitTrackerAPI.Models;

namespace HitTrackerAPI.Repositories;

public interface IAccountRepository
{
    Task<Account?> GetAccount(int id);
    Task<bool> CreateAccount(Account account);
}