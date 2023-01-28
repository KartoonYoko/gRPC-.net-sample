


namespace TelegramBot.BLL.Services;
public class AccountRepository : IAccountRepository
{
    private readonly TelegramBotContext _context;
    public AccountRepository(TelegramBotContext context) {
        _context = context;
    }

    public async Task<Domain.Models.Account> AddAsync(string token, 
        Telegram.Bot.Types.User user, 
        Telegram.Bot.Types.WebhookInfo? webhookInfo = null) {

        var account = await _context.Accounts
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.Token == token);
        if (account != null) return account.ToDomain();

        account = new DAL.Database.Entities.Account()
        {
            Token = token,
        };

        var userDB = await _context.Users.FirstOrDefaultAsync(u => u.TelegramId == user.Id);
        if (userDB is null)
        {
            userDB = new()
            {
                FirstName = user.FirstName,
                IsBot = user.IsBot,
                LastName = user.LastName,
                TelegramId = user.Id,
                UserName = user.Username
            };
            await _context.Users.AddAsync(userDB);
            await _context.SaveChangesAsync();
        }
        account.User = userDB;
        if (webhookInfo != null) { 
            // TODO add webhook info
        }

        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
        return account.ToDomain();
    }


    public async Task<Domain.Models.Account?> GetAsync(long id)
    {
        var res = await _context.Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
        if (res == null) return null;
        return res.ToDomain();
    }

    public async Task<Domain.Models.Account?> GetAsync(string token)
    {
        var res = await _context.Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Token == token);
        if (res == null) return null;
        return res.ToDomain();
    }
}
