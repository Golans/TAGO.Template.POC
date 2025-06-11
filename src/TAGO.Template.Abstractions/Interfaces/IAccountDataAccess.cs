namespace TAGO.Template.Abstractions.Interfaces
{
    public interface IAccountDataAccess
    {
        Task<Account> CreateAsync(Account account, CancellationToken cancellationToken = default);
        Task<Account> GetAsync(string accountId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Account>> GetAllAsync(CancellationToken cancellationToken = default);
        Task DeleteAsync(string accountId, CancellationToken cancellationToken = default);
        Task UpdateAsync(Account account, CancellationToken cancellationToken = default);
    }
}