

using System.ComponentModel.DataAnnotations;

namespace TAGO.Template.Abstractions
{
    //TODO : Create real model

    public class Account
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "BankId is required")]
        [Range(1, 999)]
        public int BankId { get; set; }

        [Required(ErrorMessage = "BranchId is required")]
        public int BranchId { get; set; }

        [Required(ErrorMessage = "AccountId is required")]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Date created is required")]
        public DateTime DateCreated { get; set; }
        public decimal TotalAmount { get; set; }
    }


    public class AccountIdentifier
    {
        public int BranchId { get; set; }
        public int AccountId { get; set; }
    }
}