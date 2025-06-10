namespace TAGO.Template.Abstractions.Exceptions
{
    public class AccountNotFoundException : NotFoundException
    {
        public AccountNotFoundException() : base()
        {

        }

        public AccountNotFoundException(string message, Exception inner = null) : base(message, inner)
        {

        }
    }
}
