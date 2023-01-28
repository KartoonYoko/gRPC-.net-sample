

namespace TelegramBot.gRPC.Services.v1;

public class AccountService : Protos.v1.AccountService.AccountServiceBase
{
    private readonly IAccountService _accountService;
    public AccountService(IAccountService accountService) {
        _accountService = accountService;
    }

    public override async Task<Protos.v1.CreateReply> Create(Protos.v1.CreateRequest request, ServerCallContext context)
    {
        var account = await _accountService.CreateAccountAsync(request.Token);

        return new() { 
            Id = account.Id,
            Token = account.Token
        };
    }
}
