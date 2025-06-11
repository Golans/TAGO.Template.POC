using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Data;
using TAGO.Template.Abstractions;
using TAGO.Template.Abstractions.Interfaces;
using TAGO.Template.MsSql.DataAccess.DbModel;

namespace TAGO.Template.MsSql.DataAccess
{
    //TODO : Rename
    internal class AccountDataAccess : IAccountDataAccess
    {
        private readonly ILogger<AccountDataAccess> _logger;
        public readonly DbOptionsBuilder dbOptions;


        public AccountDataAccess(IOptions<DbOptionsBuilder> options, ILogger<AccountDataAccess> logger)
        {
            _logger = logger;
            this.dbOptions = options.Value;


        }

        //TODO : Add real entities
        public async Task<Account> CreateAsync(Account account, CancellationToken cancellationToken = default)
        {
            var con = CreateClient<Account>();

            try
            {
                await con.InsertOneAsync(account, cancellationToken);
                return account;
            }
            catch (Exception ex)
            {
                throw;
            }

            return null;
        }

        public async Task<IEnumerable<Account>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var con = CreateClient<Account>();
            try
            {
                var res = con.AsQueryable();
                return await (from n in res
                              where n.AccountId == 1
                              select n).ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<Account?> GetAsync(string accountId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();

        }

        public Task UpdateAsync(Account account, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string accountId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> ServiceIsReady(CancellationToken cancellationToken = default)
        {
            var client = CreateClient<Account>();
            return true;
        }


        private IMongoCollection<T> CreateClient<T>(bool open = true)
        {
            MongoClientSettings settings = MongoClientSettings.FromConnectionString(this.dbOptions.ConnectionString);
            //settings.LinqProvider = LinqProvider.V3;
            MongoClient client = new MongoClient(settings);
            IMongoCollection<T> collection = client.GetDatabase(this.dbOptions.Database).GetCollection<T>(this.dbOptions.Collection);
            return collection;

        }
    }
}
