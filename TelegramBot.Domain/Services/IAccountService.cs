


namespace TelegramBot.Domain.Services;

public interface IAccountService
{
    Task<Account> CreateAccountAsync(string token);
}
