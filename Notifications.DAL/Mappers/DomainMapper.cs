namespace Notifications.DAL.Mappers;
internal static class DomainMapper
{
    public static TelegramAccountModel ToDomain(this Account model) {
        return new() {
            Id = model.Id,
            Token = model.Token,
            WebhookUrl = model.Url
        };
    }
}
