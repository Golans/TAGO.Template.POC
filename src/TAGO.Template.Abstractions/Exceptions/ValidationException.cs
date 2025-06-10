namespace TAGO.Template.Abstractions.Exceptions
{
    public class ValidationException : BaseException
    {
        public ValidationException() : base()
        {

        }

        public ValidationException(string message, Exception inner = null) : base(message, inner)
        {

        }
    }
}
