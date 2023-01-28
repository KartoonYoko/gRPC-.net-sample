

namespace Notifications.BLL.Services;
public class TelegramClientCreator
{
    private static HttpClient _httpClient;
    private readonly ITelegramAccountRepository _telegramAccountRepository;
    static TelegramClientCreator()
    {
        _httpClient = new HttpClient();
    }
    public TelegramClientCreator(ITelegramAccountRepository telegramAccountRepository)
    {
        _telegramAccountRepository = telegramAccountRepository;
    }

    public async Task<TelegramBotClient> CreateAsync(long accId)
    {
        var acc = await _telegramAccountRepository.GetAsync(accId);
        if (acc is null) throw new Exception("Account not exists");
        return new(acc.Token, _httpClient);
    }

    public Task<TelegramBotClient> CreateAsync(string token) =>
        Task.FromResult(new TelegramBotClient(token, _httpClient));
}
