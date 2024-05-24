using HitTrackerAPI.Models;

namespace HitTrackerAPI.Repositories.AccountRepositories
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Tries to find an account with the given ID
        /// </summary>
        /// <param name="id">AccountId for the target account</param>
        /// <returns>
        /// The <see cref="Account"/> that was found with the given ID. If no account was found with the given ID, returns null
        /// </returns>
        Task<Account?> GetAccount(int id);
    
        /// <summary>
        /// Creates the given account
        /// </summary>
        /// <param name="account">The account that will be entered into the database</param>
        /// <returns>Success indication</returns>
        Task<bool> CreateAccount(Account account);
    }
}