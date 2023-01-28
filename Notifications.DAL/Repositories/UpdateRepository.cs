

namespace Notifications.DAL.Repositories;
public class UpdateRepository : IUpdateRepository
{
    private readonly NotificationContext _context;
    public UpdateRepository(NotificationContext context) {
        _context = context;
    }


    public async Task AddAsync(long accId, long updateId) {
        var updateInfo = await _context.AccountUpdates
            .AsNoTracking()
            .FirstOrDefaultAsync(au => (au.UpdateId == updateId &&
            au.AccountId == accId));
        if (updateInfo != null) return;

        var update = new Database.Entities.Update()
        {
            Id = updateId
        };
        var account = await _context.TelegramAccounts
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == accId);
        if (account == null) throw new Exception("Empty account");
        await _context.Updates.AddAsync(update);

        await _context.AccountUpdates
            .AddAsync(new()
            {
                AccountId = accId,
                UpdateId = updateId
            });
        await _context.SaveChangesAsync();
    }

    public async Task<long?> GetAsync(long accId, long updateId) {
        var updateInfo = await _context.AccountUpdates
            .AsNoTracking()
            .FirstOrDefaultAsync(au => (au.UpdateId == updateId &&
            au.AccountId == accId));

        return updateInfo == null ? null : updateInfo.UpdateId;
    }
    public async Task<long?> GetLastAsync(long accId) {
        var list = await _context.AccountUpdates
            .AsNoTracking()
            .OrderBy(au => au.UpdateId)
            .ToListAsync();

        return list.Count > 1 ? list[^1].UpdateId : null;
    }
}
