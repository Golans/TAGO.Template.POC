

using System.ComponentModel.DataAnnotations;

namespace TAGO.Template.Abstractions
{
    //TODO : Create real model

    public class Account
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "BankId is required")]
        [Range(1, 999)]
        public int BankId { get; set; }

        [Required(ErrorMessage = "BranchId is required")]
        public int BranchId { get; set; }

        [Required(ErrorMessage = "AccountId is required")]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Date created is required")]
        public DateTime DateCreated { get; set; }
    }
}