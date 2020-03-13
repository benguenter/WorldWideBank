using WorldWideBank.Domain;

namespace WorldWideBank.Factories
{
    /// <summary>
    /// Factory for hydrating Accounts from persistence.
    /// </summary>
    public interface IAccountFactory
    {
        /// <summary>
        /// Finds an account identified by <param name="accountNumber" />
        /// </summary>
        /// <remarks>
        /// For the purposes of this test, we will assume that requests are always valid.
        /// Normally this method would also return a success/fail value plus error messages.
        /// </remarks>
        Account GetAccount(string accountNumber);
    }
}