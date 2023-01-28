


namespace TelegramBot.DAL.Database.Entities;

public class Chat
{
    public long Id { get; set; }

    public long ChatId { get; set; }
    public ChatType Type { get; set; }
    public string? Title { get; set; }
    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Description { get; set; }

    public List<ChatPermissions> Permissions { get; set; } = new();
}
