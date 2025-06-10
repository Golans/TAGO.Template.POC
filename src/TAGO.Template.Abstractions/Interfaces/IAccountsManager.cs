namespace TAGO.Template.Abstractions.Interfaces
{
    public interface IAccountsManager
    {
        Task<IEnumerable<Account>> GetAccountsAsync(CancellationToken cancellationToken = default);
        Task<Account> GetAccountAsync(int branchId, int accountId, CancellationToken cancellationToken = default);
        Task<Account> CreateAccountAsync(Account account, CancellationToken cancellationToken = default);
        Task<bool> UpdateAccountAsync(Account account, CancellationToken cancellationToken = default);
        Task<bool> DeleteAccountAsync(AccountIdentifier? requestedAccountId, CancellationToken cancellationToken = default);
    }
}