namespace Notifications.WebAPI.Services;

public class HostService : IHostedService, IDisposable
{
    private readonly IServiceScopeFactory _scopeFactory;
    private Timer? _timer;
    private readonly static HttpClient _httpClient;
    static HostService() {
        _httpClient = new();
    }
    public HostService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    public Task StartAsync(CancellationToken cancellationToken) {
        TimerCallback callback = new(async (obj) => await CheckPolling());
        _timer = new Timer(callback, null, TimeSpan.Zero, TimeSpan.FromSeconds(2));

        return Task.CompletedTask;
    }

    public async Task CheckPolling() {
        try
        {
            await CheckTelegram();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return;
    }

    private async Task CheckTelegram() {
        using var scope = _scopeFactory.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<ITelegramAccountRepository>();
        if (repository is null) return;

        var clientCreator = scope.ServiceProvider.GetRequiredService<TelegramClientCreator>();
        if (clientCreator == null) return;

        var updateRepository = scope.ServiceProvider.GetRequiredService<IUpdateRepository>();
        if (updateRepository == null) return;

        var accounts = await repository.GetAllAsync();
        for (int i = 0; i < accounts.Length; i++)
        {
            var account = accounts[i];

            if (string.IsNullOrEmpty(account.WebhookUrl))
            {
                var client = await clientCreator.CreateAsync(account.Token);
                var updateId = await updateRepository.GetLastAsync(account.Id);
                var updates = await client.GetUpdatesAsync(updateId == null ? 0 : (int)updateId + 1);

                for (int j = 0; j < updates.Length; j++)
                {
                    var update = updates[j];
                    // TODO send update
                    Console.WriteLine(update.Id);
                    await _httpClient.PostAsync();
                    await updateRepository.AddAsync(account.Id, update.Id);
                }
            }
        }

        return;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
