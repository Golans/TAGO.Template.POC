namespace TAGO.Template.Abstractions.Interfaces
{
    public interface IBankDataAccess
    {
        Task<IEnumerable<Bank>> GetBankAccountsByBranchLinqAsync(CancellationToken cancellationToken);
    }
}