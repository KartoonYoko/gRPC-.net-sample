

namespace Notifications.DAL.Repositories;
public class TelegramAccountRepository : ITelegramAccountRepository
{
    private readonly NotificationContext _context;
    public TelegramAccountRepository(NotificationContext context) {
        _context = context;
    }
    public async Task<TelegramAccountModel> AddAsync(long id, 
        string token, 
        WebhookInfo? webhookInfo = null) {

        var check = await _context.TelegramAccounts.FindAsync(id);
        if (check != null) return check.ToDomain();

        var acc = await _context.TelegramAccounts.AddAsync(new() { 
            Id = id,
            Token = token,
            Url = ""
        });
        await _context.SaveChangesAsync();

        return acc.Entity.ToDomain();
    }
    public async Task<TelegramAccountModel> UpdateAsync(UpdateTelegramAccountModel update) {
        var account = await _context.TelegramAccounts.FindAsync(update.Id);
        if (account == null) throw new Exception("Not exists t account with id " + update.Id);

        account.Url = update.WebhookInfo == null ? "" : update.WebhookInfo.Url;
        account.PendingUpdateCount = update.WebhookInfo.PendingUpdateCount;
        account.LastErrorMessage = update.WebhookInfo.LastErrorMessage;
        account.LastErrorDate = update.WebhookInfo.LastErrorDate;
        account.IpAddress = update.WebhookInfo.IpAddress;
        account.MaxConnections = update.WebhookInfo.MaxConnections;
        account.LastSynchronizationErrorDate = update.WebhookInfo.LastSynchronizationErrorDate;

        _context.Update(account);
        await _context.SaveChangesAsync();
        return account.ToDomain();
    }
    public async Task<TelegramAccountModel?> GetAsync(long id) {
        var res = await _context.TelegramAccounts.FindAsync(id);
        if (res == null) return null;
        return res.ToDomain();
    }
    public async Task<TelegramAccountModel?> GetAsync(string token) {
        var res = await _context.TelegramAccounts
            .FirstOrDefaultAsync(a => a.Token == token);
        if (res == null) return null;
        return res.ToDomain();
    }

    public async Task<TelegramAccountModel[]> GetAllAsync() { 
        var accounts = await _context.TelegramAccounts
            .ToListAsync();

        return (accounts.Select(acc => acc.ToDomain())).ToArray();
    }
}
