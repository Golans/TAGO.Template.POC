using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TAGO.Template.Api.model
{
    public class GetAccountRequest
    {

        [Required]
        [FromRoute(Name = "bankId")]
        public int BankId { get; set; }

        [Required]
        [FromRoute(Name = "branchId")]
        public int BranchId { get; set; }


        [Required]
        [FromRoute(Name = "accountId")]
        public int AccountId { get; set; }
    }
}
