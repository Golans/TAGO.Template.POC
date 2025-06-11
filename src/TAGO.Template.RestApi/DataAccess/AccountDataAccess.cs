using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tago.Extensions.Http;
using TAGO.Template.Abstractions;
using TAGO.Template.Abstractions.Exceptions;
using TAGO.Template.Abstractions.Interfaces;

namespace TAGO.Template.RestApi.DataAccess
{
    //TODO : Rename
    internal class AccountDataAccess : IAccountDataAccess
    {
        private readonly IRestClientFactory _restClientFactory;
        private readonly IRestClient _restClient;
        private readonly ILogger<AccountDataAccess> _logger;


        private readonly string _apiResourcePath = "/Account/GetAll";
        private string _serverBaseUrl;
        private const string BANK_NUMBER = "31";


        public AccountDataAccess(IRestClientFactory restClientFactory,
            IOptions<RestApiAccountDataAccessSetting> options,
            ILogger<AccountDataAccess> logger,
            IConfiguration config)
        {
            _restClientFactory = restClientFactory;
            _restClient = _restClientFactory.GetClient("RestApiAccountDataAccessClient");
            _logger = logger;

        }

        //TODO : Add real entities
        public async Task<Account> CreateAsync(Account account, CancellationToken cancellationToken = default)
        {
            var path = CombinePaths(_serverBaseUrl, _apiResourcePath);
            var res = await _restClient.PostAsync<CreateAccountResponseModel>(path, account, cancellationToken: cancellationToken);
            account.Id = res.Data.CreatedAccountId;
            return account;
        }


        public async Task<IEnumerable<Account>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var path = CombinePaths(_serverBaseUrl, _apiResourcePath);
                var res = await _restClient.GetAsync<Account[]>(path, cancellationToken: cancellationToken);
                if (res.IsSuccessStatusCode)
                {
                    if (res != null)
                    {
                        return res.Data;
                    }
                    else
                    {
                        return Enumerable.Empty<Account>();
                    }
                }
                else
                {
                    throw new BaseException(res.ResponseString);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }

        }

        public async Task<Account?> GetAsync(string accountId, CancellationToken cancellationToken = default)
        {
            try
            {
                var path = CombinePaths(_serverBaseUrl, _apiResourcePath, BANK_NUMBER, accountId.ToString());

                var res = await _restClient.GetAsync<Account>(path, cancellationToken: cancellationToken);
                if (res != null)
                {
                    return res.Data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }

        }

        public Task UpdateAsync(Account account, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string accountId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        private string CombinePaths(params string[] segements)
        {
            if (segements?.Length > 0)
            {
                List<string> parts = new List<string>();
                foreach (var segement in segements)
                {
                    var value = segement;

                    if (value.StartsWith("/") || value.StartsWith("\\"))
                    {
                        value = value.Substring(1);
                    }

                    if (value.EndsWith("/") || value.EndsWith("\\"))
                    {
                        value = value.Substring(0, value.Length - 1);
                    }

                    parts.Add(value.Trim());
                }

                var res = string.Join("/", parts);

                return res;
            }
            else
            {
                return "";
            }
        }
    }

}
