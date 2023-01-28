


namespace TelegramBot.BLL.Services;
public class MessageService : IMessageService
{
    private readonly TelegramBotClientCreator _creator;
    private readonly IMessageRepository _messageRepository;
    public MessageService(TelegramBotClientCreator creator,
        IMessageRepository messageRepository) { 
        _creator = creator;
        _messageRepository = messageRepository;
    }

    public async Task<Message> SendMessage(long accId, long chatId, string text) {
        var client = await _creator.CreateAsync(accId);
        var message = await client.SendTextMessageAsync(chatId, text);
        
        await _messageRepository.AddAsync(message);

        return message;
    }
}
