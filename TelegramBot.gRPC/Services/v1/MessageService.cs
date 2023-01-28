using TelegramBot.gRPC.Protos.v1;

namespace TelegramBot.gRPC.Services.v1;

public class MessageService : Protos.v1.MessageService.MessageServiceBase
{
    private readonly IMessageService _messageService;
    public MessageService(IMessageService messageService) {
        _messageService = messageService;
    }

    public override async Task<SendMessageReply> SendMessage(SendMessageRequest request, ServerCallContext context)
    {
        var message = await _messageService.SendMessage(request.AccountId, 
            request.ChatId, request.Text);

        return new() { 
            Id = message.MessageId
        };
    }
}
