

namespace TelegramBot.Domain.Repositories;
public interface IMessageRepository
{
    Task<Message> AddAsync(Message message);
    Task<Message> GetByIdAsync(long chatId, long messageId);
    Task<Message> GetByTelegramIdAsync(long chatId, long messageId);
}
