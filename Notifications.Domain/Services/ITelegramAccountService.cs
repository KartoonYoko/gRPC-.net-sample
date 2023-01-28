namespace Notifications.Domain.Services;
public interface ITelegramAccountService
{
    Task CreateAsync(long id, string token);
    Task AddWebhookAsync(SetWebhookModel model);
    Task DeleteWebhookAsync(DeleteWebhookModel model); 
}
