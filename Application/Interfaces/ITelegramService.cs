namespace Application.Interfaces
{
    public interface ITelegramService
    {
        Task<bool> SendMessageAsync(string msg);
    }
}