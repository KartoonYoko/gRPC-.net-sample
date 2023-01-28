

namespace TelegramBot.DAL.Mappers;
internal static class TelegramMapper
{
    internal static Domain.Models.Account ToDomain(this Database.Entities.Account model)
    {
        return new Domain.Models.Account()
        {
            Id = model.Id,
            Token = model.Token,
            User = model.User.ToDomain()
        };
    }


    internal static Telegram.Bot.Types.User ToDomain(this User model) {
        return new Telegram.Bot.Types.User()
        {
            Id = model.TelegramId,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Username = model.UserName,
            IsBot = model.IsBot
        };
    }

    internal static Telegram.Bot.Types.Chat ToDomain(this Chat model) {
        return new()
        {
            Id = model.ChatId,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Username = model.Username,
            Title = model.Title,
            Type = Enum.Parse<Telegram.Bot.Types.Enums.ChatType>(model.Type.ToString())
        };
    }

    internal static Telegram.Bot.Types.Message ToDomain(this Message model, 
        User? from = null,
        Chat? senderChat = null)
    {
        return new()
        {
            MessageId = (int)model.MessageId,
            Text = model.Text,
            From = from == null ? null : from.ToDomain(),
            SenderChat = senderChat == null ? null : senderChat.ToDomain()
        };
    }

}
