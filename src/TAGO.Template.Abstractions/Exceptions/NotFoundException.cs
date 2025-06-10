namespace TAGO.Template.Abstractions.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException() : base()
        {

        }

        public NotFoundException(string message, Exception inner = null) : base(message, inner)
        {

        }
    }
}
