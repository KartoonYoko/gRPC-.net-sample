namespace TelegramBot.BLL.Services;
public class UpdateHandler : IUpdateHandler
{
    private readonly IMessageRepository _messageRepository;
    public UpdateHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task MessageAsync(string token, Message message) {
        var mes = await _messageRepository.GetByTelegramIdAsync(message.Chat.Id, message.MessageId);
        if (mes != null) return;

        await _messageRepository.AddAsync(message);
    }
    public async Task EditMessageAsync(string token, Message message) { 
        throw new NotImplementedException();
    }
    public async Task ChannelPostAsync(string token, Message message) {
        throw new NotImplementedException();
    }

}
