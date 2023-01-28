

namespace Notifications.BLL.Services;
public class TelegramAccountService : ITelegramAccountService
{
    private readonly ITelegramAccountRepository _repository;
    private readonly TelegramClientCreator _creator;
    public TelegramAccountService(ITelegramAccountRepository telegramAccountRepository,
        TelegramClientCreator creator) { 

        _repository = telegramAccountRepository;    
        _creator = creator;
    }
    public Task CreateAsync(long id, string token) {
        return _repository.AddAsync(id, token);
    }

    public async Task AddWebhookAsync(SetWebhookModel model) {
        var client = await _creator.CreateAsync(model.AccountId);
        if (client == null) throw new Exception();

        InputFileStream? certFile = null;
        if (model.Certificate != null) {
            Stream certStream = new MemoryStream(model.Certificate);
            certFile = new InputFileStream(certStream, "certificate");
        }

        await client.SetWebhookAsync(model.Url, certificate: certFile);
        var info = await client.GetWebhookInfoAsync();

        if (info == null) throw new Exception("Empty webhook");

        await _repository.UpdateAsync(new() { 
            Id = model.AccountId,
            WebhookInfo = info
        });
    }

    public async Task DeleteWebhookAsync(DeleteWebhookModel model)
    {
        var client = await _creator.CreateAsync(model.AccountId);
        if (client == null) throw new Exception();

        var account = await _repository.GetAsync(model.AccountId);
        if (account == null) throw new Exception("Not found acc with id " + model.AccountId);
        if (account.WebhookUrl == null) return;

        await client.DeleteWebhookAsync();
        await _repository.UpdateAsync(new UpdateTelegramAccountModel() { 
            Id = account.Id,
            Token = account.Token,
            WebhookInfo = null
        });
    }

}
