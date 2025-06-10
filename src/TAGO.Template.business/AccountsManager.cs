using Microsoft.Extensions.Logging;
using TAGO.Template.Abstractions;
using TAGO.Template.Abstractions.Exceptions;
using TAGO.Template.Abstractions.Interfaces;

namespace TAGO.Template.Business
{
    public class AccountsManager : IAccountsManager
    {
        private readonly ILogger<AccountsManager> _logger;
        private readonly IAccountDataAccess accountDataAccess;

        public AccountsManager(
            ILogger<AccountsManager> logger,
            IAccountDataAccess accountDataAccess)
        {
            _logger = logger;
            this.accountDataAccess = accountDataAccess;
        }

        public async Task<Account> CreateAccountAsync(Account account, CancellationToken cancellationToken = default)
        {

            //do some validation 
            if (account == null)
            {
                throw new ValidationException("account info could not be null");
            }


            var existing = await accountDataAccess.GetAccountAsync(account.BranchId, account.AccountId);

            if (existing == null)
            {
                var result = await accountDataAccess.CreateAccountAsync(account, cancellationToken);
                //do some other staff

                return result;
            }
            else
            {
                throw new AccountAlreadyExistException();
            }
        }

        public async Task<bool> DeleteAccountAsync(AccountIdentifier? requestedAccountId, CancellationToken cancellationToken = default)
        {
            //do some validation 
            if (requestedAccountId == null)
            {
                throw new ValidationException("account id could not be null");
            }

            var account = await accountDataAccess.GetAccountAsync(requestedAccountId.BranchId, requestedAccountId.AccountId);
            if (account != null)
            {
                await accountDataAccess.DeleteAccountAsync(requestedAccountId, cancellationToken);

                return true;
                //do some other staff
            }
            else
            {
                throw new AccountNotFoundException();
            }
        }

        public async Task<Account> GetAccountAsync(int branchId, int accountId, CancellationToken cancellationToken = default)
        {
            return await accountDataAccess.GetAccountAsync(branchId, accountId, cancellationToken);
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync(CancellationToken cancellationToken = default)
        {
            //do some validation 

            var accounts = await accountDataAccess.GetAllAccountsAsync(cancellationToken);

            //do some other staff


            return accounts;
        }

        public async Task<bool> UpdateAccountAsync(Account account, CancellationToken cancellationToken = default)
        {

            if (account == null)
            {
                throw new ValidationException("account could not be null");
            }

            var existing = await accountDataAccess.GetAccountAsync(account.BranchId, account.AccountId);
            if (existing != null)
            {
                await accountDataAccess.UpdateAccountAsync(account, cancellationToken);

                return true;
                //do some other staff
            }
            else
            {
                throw new AccountNotFoundException();
            }


        }
    }

}
