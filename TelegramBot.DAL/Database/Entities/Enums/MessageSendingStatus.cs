

namespace TelegramBot.DAL.Database.Entities.Enums;


public enum MessageSendingStatus {
    Sending,        // отправляется
    NotSent,        // не отправлено
    Arrived,        // получено
    Readed,         // прочитано получателем
}
