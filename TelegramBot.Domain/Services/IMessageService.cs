
namespace TelegramBot.Domain.Services;
public interface IMessageService
{
    Task<Message> SendMessage(long accId, long chatId, string text);
}
