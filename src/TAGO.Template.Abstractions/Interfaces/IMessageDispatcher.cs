namespace TAGO.Template.Abstractions.Interfaces
{
    public interface IBFBMessageDispatcher
    {
        Task SendMessageAsync(string text, CancellationToken cancellationToken);
        Task SendAccountMessageAsync(Account account, CancellationToken cancellationToken);
    }

    public interface IBFFMessageDispatcher
    {
        Task SendMessageAsync(string text, CancellationToken cancellationToken);
        Task SendAccountMessageAsync(Account account, CancellationToken cancellationToken);
    }
}