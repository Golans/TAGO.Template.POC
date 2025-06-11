using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Tago.Extensions.AspNetCore;
using TAGO.Template.Abstractions;
using TAGO.Template.Abstractions.Interfaces;

namespace TAGO.Template.RestApi.Controllers
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //TODO : Rename
    public class AccountController : ControllerBase
    {
        private readonly IAccountsManager _dataAccess;

        public AccountController(
                ILogger<AccountController> logger,
                IConfiguration config,
                IAccountsManager dataAccess
            )
        {
            this._dataAccess = dataAccess;
        }

        // GET: api/Accounts
        [HttpGet("Accounts")]
        [ProducesResponseType(typeof(IEnumerable<Account>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BaseHttpException), StatusCodes.Status400BadRequest)]
        //TODO : Rename
        public async Task<ActionResult> GetAccounts([FromQuery] string? code)
        {
            List<Account> accounts = new List<Account>();
            //TODO : Rename

            try
            {
                var result = await _dataAccess.GetAsync();
                if (result != null)
                {
                    accounts.AddRange(result);
                }
                if (null == accounts || 0 == accounts.Count)
                    return NoContent();

                return Ok(accounts);
            }
            catch (Exception ex)
            {
                throw new BaseAspNetCoreException(StatusCodes.Status400BadRequest, "Unable to retrieve data from source.");
            }
            finally
            {
            }

        }
    }
}
