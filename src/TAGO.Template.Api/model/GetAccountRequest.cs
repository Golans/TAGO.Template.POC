using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TAGO.Template.Api.model
{
    public class GetAccountRequest
    {

        [Required]
        [FromRoute(Name = "accountId")]
        public string AccountId { get; set; }
    }
}
