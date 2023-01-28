

namespace TelegramBot.Domain.Models;
public class Account
{
    public long Id { get; set; }
    public string Token { get; set; }
    public User User { get; set; }
    public WebhookInfo? WebhookInfo { get; set; }
}
