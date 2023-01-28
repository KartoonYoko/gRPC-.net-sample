
namespace TelegramBot.BLL.Services;
public class TelegramBotClientCreator
{
    private readonly IAccountRepository _accountRepository;
    private readonly static HttpClient _httpClient;

    static TelegramBotClientCreator() { 
        _httpClient = new HttpClient();
    }
    public TelegramBotClientCreator(IAccountRepository repository) { 
        _accountRepository = repository;

    }

    public async Task<TelegramBotClient> CreateAsync(long accId) {
        var acc = await _accountRepository.GetAsync(accId);
        if (acc is null) throw new Exception("Account not exists");
        return new(acc.Token, _httpClient);
    }
    public Task<TelegramBotClient> CreateAsync(string token) => 
        Task.FromResult(new TelegramBotClient(token, _httpClient));
    
}
