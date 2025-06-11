namespace TAGO.Template.Abstractions.Interfaces
{
    public interface IAccountsManager
    {
        Task<IEnumerable<Account>> GetAsync(CancellationToken cancellationToken = default);
        Task<Account> GetAsync(string accountId, CancellationToken cancellationToken = default);
        Task<Account> CreateAsync(Account account, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Account account, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(string accountId, CancellationToken cancellationToken = default);
    }
}