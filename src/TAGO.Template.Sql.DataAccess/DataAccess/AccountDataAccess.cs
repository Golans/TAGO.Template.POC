using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        public async Task<Account> CreateAccountAsync(Account account, CancellationToken cancellationToken = default)
        {
            using (var con = await CreateConnection())
            {
                using (var cmd = CreateCommand(con))
                {
                    try
                    {
                        //Use the command to display employee names from 
                        // the EMPLOYEES table
                        cmd.CommandText = "dbo.createAccountProcedure";// where id = :id";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter id = new SqlParameter("userName", account.AccountId.ToString());
                        cmd.Parameters.Add(id);


                        var res = await cmd.ExecuteNonQueryAsync();

                        return account;
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }


            return null;
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync(CancellationToken cancellationToken = default)
        {

            using (var con = await CreateConnection())
            {
                using (var cmd = CreateCommand(con))
                {
                    try
                    {
                        List<Account> accounts = new List<Account>();

                        //Use the command to display employee names from 
                        // the EMPLOYEES table
                        cmd.CommandText = "getAccountsProcedure";// where id = :id";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        foreach (DataRow row in dt.Rows)
                        {
                            var account = GetAccountModel(row);
                            accounts.Add(account);
                        }

                        //var reader = await cmd.ExecuteReaderAsync();
                        //while (reader.Read())
                        //{
                        //    var account = GetAccountModel(reader);
                        //    accounts.Add(account);
                        //}

                        //reader.Dispose();

                        return accounts;
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
        }

        public async Task<Account?> GetAccountAsync(int branchId, int accountId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();

        }

        public Task UpdateAccountAsync(Account account, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAccountAsync(AccountIdentifier? requestedAccountId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> ServiceIsReady(CancellationToken cancellationToken = default)
        {
            using (var conn = await CreateConnection())
            {
                await conn.CloseAsync();
            }
            return true;
        }

        public Task<IEnumerable<Bank>> GetAccountsGroupedByBranchLinqAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }



        private async Task<SqlConnection> CreateConnection(bool open = true)
        {
            var con = new SqlConnection(this.dbOptions.ConnectionString);
            if (open)
            {
                await con.OpenAsync();
            }
            return con;
        }
        private SqlCommand CreateCommand(SqlConnection con)
        {
            var res = con.CreateCommand();

            if (this.dbOptions?.RequestTimeout != null)
            {
                res.CommandTimeout = Convert.ToInt32(this.dbOptions?.RequestTimeout.Value.TotalSeconds);
            }

            return res;
        }
        private Account GetAccountModel(DataRow row)
        {
            Account account = new Account();

            account.Id = (Guid)row["Guid"];
            account.AccountId = (int)row["Id"];
            return account;
        }
    }
}
