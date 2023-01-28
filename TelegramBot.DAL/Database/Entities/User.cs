

namespace TelegramBot.DAL.Database.Entities;

public class User
{
    public long Id { get; set; }
    public long TelegramId { get; set; }
    public bool IsBot { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public string? UserName { get; set; }

    public List<ChatPermissions> Permissions { get; set; } = new();

    public long? AccountId { get; set; }
    public Account? Account { get; set; }

}
