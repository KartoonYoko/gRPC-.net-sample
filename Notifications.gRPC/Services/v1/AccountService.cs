using Google.Protobuf.WellKnownTypes;
using Notifications.gRPC.Protos.v1;

namespace Notifications.gRPC.Services.v1;

public class AccountService : Protos.v1.AccountService.AccountServiceBase
{
    private readonly ITelegramAccountService _telegramAccountService;
    private readonly FileService.FileServiceClient _fileServiceClient;
    private readonly static HttpClient _httpClient;
    private readonly GlobalOptions _globalOptions;

    static AccountService()
    {
        _httpClient = new();
    }

    public AccountService(ITelegramAccountService telegramAccountService,
        FileService.FileServiceClient fileServiceClient,
        GlobalOptions globalOptions)
    {

        _globalOptions = globalOptions;
        _telegramAccountService = telegramAccountService;
        _fileServiceClient = fileServiceClient;
    }

    public override async Task<Empty> Create(CreateRequest request, ServerCallContext context)
    {
        await _telegramAccountService.CreateAsync(request.AccountId, request.Token);

        return new();
    }

    public override async Task<Empty> AddWebhook(AddWebhookRequest request, ServerCallContext context)
    {
        string url = _globalOptions.HostName;
        var fileByIdReply = _fileServiceClient.GetFileById(new()
        {
            Id = request.CretificateId
        });
        var certBytes = await _httpClient.GetByteArrayAsync(fileByIdReply.Url);
        await _telegramAccountService.AddWebhookAsync(new()
        {
            AccountId = request.AccountId,
            Url = url,
            Certificate = certBytes
        });

        return new();
    }

    public override async Task<Empty> RemoveWebhook(RemoveWebhookRequest request, ServerCallContext context)
    {
        await _telegramAccountService.DeleteWebhookAsync(new Domain.Models.DeleteWebhookModel()
        {
            AccountId = request.AccountId
        });

        return new();
    }
}
