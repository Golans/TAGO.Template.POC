using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Tago.Extensions.AspNetCore;
using TAGO.Template.Abstractions;
using TAGO.Template.Abstractions.Interfaces;
using TAGO.Template.Api.Extensions;
using TAGO.Template.Api.model;

namespace TAGO.Template.Controllers
{
    //[Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]

    //TODO : Rename
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IAccountsManager _accountManager;

        public AccountsController(
                ILogger<AccountsController> logger,
                IConfiguration config,
                IAccountsManager dataAccess
            )
        {
            this._logger = logger;
            this._accountManager = dataAccess;
        }


        /// <summary>
        /// get accounts list
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<Account>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BaseHttpException), StatusCodes.Status400BadRequest)]
        //TODO : Rename
        public async Task<ActionResult> GetAccounts(CancellationToken cancellationToken)
        {
            List<Account> accounts = new List<Account>();
            try
            {
                var result = await _accountManager.GetAccountsAsync(cancellationToken);
                if (result != null)
                {
                    accounts.AddRange(result);
                }
                if (null == accounts || 0 == accounts.Count)
                    return NoContent();

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }
            return Ok(accounts);
        }



        //TODO : Rename

        // GET: api/Account
        /// <summary>
        /// get account by branch and account id
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{bankId}/{branchId}/{accountId}")]
        [ProducesResponseType(typeof(Account), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseHttpException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseHttpException), StatusCodes.Status500InternalServerError)]
        //TODO : Rename
        public async Task<ActionResult> GetAccount([FromQuery] GetAccountRequest request, CancellationToken cancellationToken)
        {
            Account account;

            //TODO : maybe use this
            //var requestedAccountId = await HttpContext.GetAccountIdentifierFromJWT();

            try
            {
                account = await _accountManager.GetAccountAsync(request.BranchId, request.AccountId, cancellationToken);
                if (null == account)
                {
                    _logger.LogDebug($"Account Bank={request.BankId} Branch={request.BranchId} AccountId={request.AccountId} doesn't exists");
                    return NotFound();
                }
                else
                {
                    return Ok(account);
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



        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        /// <summary>
        /// update account by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Account), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BaseHttpException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseHttpException), StatusCodes.Status500InternalServerError)]
        //TODO : Rename
        public async Task<IActionResult> UpdateAccount([FromRoute] string id, [Required][FromBody] Account account, CancellationToken cancellationToken)
        {
            var requestedAccountId = await HttpContext.GetAccountIdentifierFromJWT();
            if (requestedAccountId.BranchId != account.BranchId || requestedAccountId.AccountId != account.AccountId)
            {
                return BadRequest(new BaseHttpException { Type = "Application error", Title = "Data missmatch.", Status = 400 });
            }
            try
            {
                await _accountManager.UpdateAccountAsync(account, cancellationToken);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }
        }


        /// <summary>
        /// create new account
        /// </summary>
        /// <param name="account"></param>
        /// <param name="cancellationTok"></param>
        /// <returns></returns>
        // POST: api/Accounts
        [HttpPost]
        [ProducesResponseType(typeof(Account), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseHttpException), StatusCodes.Status400BadRequest)]
        //TODO : Rename
        public async Task<ActionResult> CreateAccount([Required][FromBody] Account account, CancellationToken cancellationToken)
        {
            try
            {
                var createdAccount = await _accountManager.CreateAccountAsync(account, cancellationToken);
                return CreatedAtAction("Account", new { id = account.Id }, createdAccount);
            }
            catch (Exception ex)
            {
                throw new BaseAspNetCoreException(StatusCodes.Status400BadRequest, "Unable to create entity.");
            }
            finally
            {
            }
        }

        /// <summary>
        /// delete account by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationTok"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BaseHttpException), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseHttpException), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAccount([FromRoute] string id, CancellationToken cancellationToken)
        {
            var requestedAccountId = await HttpContext.GetAccountIdentifierFromJWT();
            try
            {
                await _accountManager.DeleteAccountAsync(requestedAccountId, cancellationToken);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }

        }
    }
}
