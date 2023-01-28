namespace TelegramBot.Domain.Services;
public interface IUpdateHandler
{
    Task MessageAsync(string token, Message message);
    Task EditMessageAsync(string token, Message message);
    Task ChannelPostAsync(string token, Message message);
}
