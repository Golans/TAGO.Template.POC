namespace TAGO.Template.Abstractions
{
    public class Branch
    {
        public decimal Id { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
    }
}