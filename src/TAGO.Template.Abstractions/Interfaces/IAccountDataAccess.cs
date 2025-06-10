namespace TAGO.Template.Abstractions.Interfaces
{
    public interface IAccountDataAccess
    {
        Task<Account> CreateAccountAsync(Account account, CancellationToken cancellationToken = default);
        Task<Account> GetAccountAsync(int branchId, int accountId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Bank>> GetAccountsGroupedByBranchLinqAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Account>> GetAllAccountsAsync(CancellationToken cancellationToken = default);
        Task DeleteAccountAsync(AccountIdentifier? requestedAccountId, CancellationToken cancellationToken = default);
        Task UpdateAccountAsync(Account account, CancellationToken cancellationToken = default);

        Task<bool> ServiceIsReady(CancellationToken cancellationToken = default);
    }
}