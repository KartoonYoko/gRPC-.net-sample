

namespace Notifications.Domain.Models;
public record UpdateTelegramAccountModel
{
    public long Id { get; set; }
    public string? Token { get; set; } = null;
    public WebhookInfo? WebhookInfo { get; set; }
}
