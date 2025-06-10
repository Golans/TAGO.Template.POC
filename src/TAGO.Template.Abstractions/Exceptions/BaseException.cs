namespace TAGO.Template.Abstractions.Exceptions
{
    public class BaseException : Exception
    {
        public BaseException() : base()
        {

        }

        public BaseException(string message, Exception inner = null) : base(message, inner)
        {

        }
    }
}
