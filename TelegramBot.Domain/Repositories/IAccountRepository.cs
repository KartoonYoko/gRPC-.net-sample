

namespace TelegramBot.Domain.Repositories;


public interface IAccountRepository
{
    Task<Account> AddAsync(string token, User user, WebhookInfo? webhookInfo = null);
    Task<Account?> GetAsync(long accId);
    Task<Account?> GetAsync(string token);
}
