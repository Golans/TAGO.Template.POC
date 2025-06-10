namespace TAGO.Template.Abstractions.Exceptions
{
    public class AlreadyExistException : BaseException
    {
        public AlreadyExistException() : base()
        {

        }

        public AlreadyExistException(string message, Exception inner = null) : base(message, inner)
        {

        }
    }
}
