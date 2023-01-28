

namespace TelegramBot.BLL.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly TelegramBotClientCreator _creator;

    public AccountService(IAccountRepository accountRepository,
        TelegramBotClientCreator creator) { 
        _creator = creator;
        _accountRepository = accountRepository;
    }

    public async Task<Account> CreateAccountAsync(string token) {
        var client = await _creator.CreateAsync(token);
        var me = await client.GetMeAsync();
        var webHookInfo = await client.GetWebhookInfoAsync();
        
        return await _accountRepository.AddAsync(token, me, webHookInfo);

    }


}
