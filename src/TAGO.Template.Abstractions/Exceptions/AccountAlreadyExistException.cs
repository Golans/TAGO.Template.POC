namespace TAGO.Template.Abstractions.Exceptions
{
    public class AccountAlreadyExistException : AlreadyExistException
    {
        public AccountAlreadyExistException() : base()
        {

        }

        public AccountAlreadyExistException(string message, Exception inner = null) : base(message, inner)
        {

        }
    }
}
