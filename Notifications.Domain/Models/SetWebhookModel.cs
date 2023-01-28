

namespace Notifications.Domain.Models;
public record SetWebhookModel
{

    public long AccountId { get; init; }
    public string Url { get; init; }
    public byte[]? Certificate { get; init; }
}
