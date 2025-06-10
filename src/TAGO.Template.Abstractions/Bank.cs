namespace TAGO.Template.Abstractions
{
    public class Bank
    {
        public int Id { get; set; }
        public IEnumerable<Branch> Branches { get; set; }
    }
}