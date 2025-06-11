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

        public async Task<Account> CreateAsync(Account account, CancellationToken cancellationToken = default)
        {

            //do some validation 
            if (account == null)
            {
                throw new ValidationException("account info could not be null");
            }


            var existing = await accountDataAccess.GetAsync(account.Id);

            if (existing == null)
            {
                var result = await accountDataAccess.CreateAsync(account, cancellationToken);
                //do some other staff

                return result;
            }
            else
            {
                throw new AccountAlreadyExistException();
            }
        }

        public async Task<bool> DeleteAsync(string accountId, CancellationToken cancellationToken = default)
        {
            //do some validation 
            if (accountId == null)
            {
                throw new ValidationException("account id could not be null");
            }

            var account = await accountDataAccess.GetAsync(accountId);
            if (account != null)
            {
                await accountDataAccess.DeleteAsync(accountId, cancellationToken);

                return true;
                //do some other staff
            }
            else
            {
                throw new AccountNotFoundException();
            }
        }

        public async Task<Account> GetAsync(string accountId, CancellationToken cancellationToken = default)
        {
            return await accountDataAccess.GetAsync(accountId, cancellationToken);
        }

        public async Task<IEnumerable<Account>> GetAsync(CancellationToken cancellationToken = default)
        {
            //do some validation 

            var accounts = await accountDataAccess.GetAllAsync(cancellationToken);

            //do some other staff


            return accounts;
        }

        public async Task<bool> UpdateAsync(Account account, CancellationToken cancellationToken = default)
        {

            if (account == null)
            {
                throw new ValidationException("account could not be null");
            }

            var existing = await accountDataAccess.GetAsync(account.Id);
            if (existing != null)
            {
                await accountDataAccess.UpdateAsync(account, cancellationToken);

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
