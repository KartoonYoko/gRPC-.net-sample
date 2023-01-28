


namespace TelegramBot.DAL.Repositories;
public class MessageRepository : IMessageRepository
{
    private readonly TelegramBotContext _context;
    public MessageRepository(TelegramBotContext context) { 
        _context = context;
    }

    public async Task<Telegram.Bot.Types.Message> AddAsync(Telegram.Bot.Types.Message message) {
        var check = await _context.Messages
            .FirstOrDefaultAsync(m => m.MessageId == message.MessageId);

        if (check != null) return check.ToDomain();

        var messageToSend = new Message()
        {
            Text = message.Text,
            SentDate = DateTime.UtcNow,
        };

        if (message.Chat is not null) {
            var chat = await _context.Chats.FirstOrDefaultAsync(c => c.ChatId == message.Chat.Id);
            if (chat == null) {
                chat = new() { 
                    ChatId = message.Chat.Id,
                    Description = message.Chat.Description,
                    FirstName = message.Chat.FirstName,
                    LastName = message.Chat.LastName,
                    Username = message.Chat.Username,
                    Title = message.Chat.Username,
                    Type = Enum.Parse<ChatType>(message.Chat.Type.ToString())
                };
                await _context.Chats.AddAsync(chat);
                await _context.SaveChangesAsync();
            }
            messageToSend.Chat = chat;
        }
        if (message.From != null) {
            var from = await _context.Users
                .FirstOrDefaultAsync(u => u.TelegramId == message.From.Id);

            if (from == null) {
                from = new() { 
                    TelegramId = message.From.Id,
                    FirstName = message.From.FirstName,
                    LastName = message.From.LastName,
                    IsBot = message.From.IsBot,
                    UserName = message.From.Username,
                };
                await _context.Users.AddAsync(from);
                await _context.SaveChangesAsync();
            }

            messageToSend.From = from;

        }

        var res = await _context.Messages.AddAsync(messageToSend);
        await _context.SaveChangesAsync();

        return res.Entity.ToDomain();
    }

    public async Task<Telegram.Bot.Types.Message> GetByIdAsync(long chatId, long messageId) {
        var res = await _context.Messages
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ChatId == chatId && m.Id == messageId);
        if (res == null) throw new Exception("Not find message");
        return res.ToDomain();
    }

    public async Task<Telegram.Bot.Types.Message> GetByTelegramIdAsync(long chatId, long messageId) {
        var res = await _context.Messages
            .AsNoTracking()
            .Include(m => m.Chat)
            .FirstOrDefaultAsync(m => m.Chat.ChatId == chatId && m.MessageId == messageId);

        if (res == null) throw new Exception("Not find message");
        return res.ToDomain();
    }

}
