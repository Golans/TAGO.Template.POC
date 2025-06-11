using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Tago.Extensions.AspNetCore;
using TAGO.Template.Abstractions;
using TAGO.Template.Abstractions.Interfaces;
using TAGO.Template.Api.model;

namespace TAGO.Template.Controllers
{
    //[Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]

    //TODO : Rename
    public class AccountsReportDemoController : ControllerBase
    {
        private static AccountsRepo _accountsRepo = new AccountsRepo();
        private readonly IAccountDataAccess _dataAccess;
        public AccountsReportDemoController(
                ILogger<AccountsReportDemoController> logger,
                IConfiguration config,
                IAccountDataAccess dataAccess
            )
        {
            this._dataAccess = dataAccess;
        }

        // GET: api/Accounts
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<Account>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BaseHttpException), StatusCodes.Status400BadRequest)]
        //TODO : Rename
        public async Task<ActionResult> GetAccounts(CancellationToken cancellationToken = default)
        {
            try
            {
                return Ok(_accountsRepo.Accounts);
            }
            finally
            {
            }
        }


        //TODO : Rename

        // GET: api/Account
        [HttpGet("{bankId}/{branchId}/{accountId}")]
        [ProducesResponseType(typeof(Account), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseHttpException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseHttpException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAccount([FromQuery] GetAccountRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var acc = _accountsRepo.Accounts.FirstOrDefault(o => o.Id == request.AccountId);
                if (acc != null)
                {
                    return Ok(acc);
                }
                else
                {
                    return NotFound();
                }
            }
            finally
            {
            }

        }
    }

    internal class AccountsRepo
    {

        private Random rnd = new Random(Environment.TickCount);

        private List<Account> accounts = new List<Account>();

        public List<Account> Accounts { get => accounts; }

        public AccountsRepo()
        {
            Load();
        }



        private void Load()
        {
            this.accounts.Clear();
            for (int i = 0; i < 10; i++)
            {
                accounts.Add(new Account
                {
                    Id = Guid.NewGuid().ToString(),
                    AccountId = (i + 1),
                    BankId = 31,
                    BranchId = rnd.Next(1, 99),
                    DateCreated = DateTime.Now
                });
            }
        }
    }
}
