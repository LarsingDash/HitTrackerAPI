using HitTrackerAPI.Models;

namespace HitTrackerAPI.Repositories.AccountRepositories
{
    public class AccountMock : IAccountRepository
    {
        private readonly List<Account> _accounts =
        [
            new Account { AccountId = 0 },
            new Account { AccountId = 1 },
        ];

        public Task<Account?> GetAccount(int id) => Task.Run(() =>
        {
            return _accounts.FirstOrDefault(account => account.AccountId == id);
        });

        public Task<bool> CreateAccount(Account account)=> Task.Run(() =>
        {
            _accounts.Add(account);
            return _accounts.Contains(account);
        });
    }
}