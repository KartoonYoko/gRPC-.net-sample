

namespace Notifications.Domain.Repositories;
public interface ITelegramAccountRepository
{
    Task<TelegramAccountModel> AddAsync(long id, string token, WebhookInfo? webhookInfo = null);
    Task<TelegramAccountModel> UpdateAsync(UpdateTelegramAccountModel update);
    Task<TelegramAccountModel?> GetAsync(long id);
    Task<TelegramAccountModel?> GetAsync(string token);
    Task<TelegramAccountModel[]> GetAllAsync();
}