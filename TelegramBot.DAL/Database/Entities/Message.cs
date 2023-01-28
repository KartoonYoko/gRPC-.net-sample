



namespace TelegramBot.DAL.Database.Entities;


public class Message
{
    public long Id { get; set; }
    public long MessageId { get; set; }

    /// <summary>
    /// Статус отправки сообщения
    /// </summary>
    public MessageSendingStatus MessageSendingStatus { get; set; } = MessageSendingStatus.Sending;
    
    public long ChatId { get; set; }
    public Chat Chat { get; set; }

    public DateTime SentDate { get; set; }
    public DateTime? EditedDate { get; set; } = null;

    public string? Text { get; set; }
    public List<PhotoSize>? Photo { get; set; }

    public long? DocumentId { get; set; }
    public Document? Document { get; set; }

    public long? AudioId { get; set; }
    public Audio? Audio { get; set; }


    public bool? DeleteChatPhoto { get; set; }
    public bool? GroupChatCreated { get; set; }
    public bool? SupergroupChatCreated { get; set; }
    public bool? ChannelChatCreated { get; set; }

    public long? MigrateToChatId { get; set; }
    public long MegrateFromChatId { get; set; }

    public long? FromId { get; set; }
    public User? From { get; set; }

    public long? SenderChatId { get; set; }
    public Chat? SenderChat { get; set; }

    /// <summary>
    /// For forwarded messages, sender of the original message
    /// </summary>
    public long? ForwardFromId { get; set; }
    public User? ForwardFrom { get; set; }

    /// <summary>
    /// For messages forwarded from channels or from anonymous administrators, 
    /// information about the original sender chat
    /// </summary>
    public long? ForwardFromChatId { get; set; }
    public Chat? ForwardFromChat { get; set; }

    /// <summary>
    /// For messages forwarded from channels, identifier of the original message in the channel
    /// </summary>
    public long? ForwardFromMessageId { get; set; }

    /// <summary>
    /// For replies, the original message. 
    /// </summary>
    public long? ReplyToMessageId { get; set; }
    public Message? ReplyToMessage { get; set; }
}
