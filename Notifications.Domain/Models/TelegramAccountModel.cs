namespace Notifications.Domain.Models;
public record TelegramAccountModel
{
    public long Id { get; set; }
    public string Token { get; set; }
    public string? WebhookUrl { get; set; }
}
