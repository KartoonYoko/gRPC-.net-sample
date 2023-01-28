


namespace TelegramBot.DAL.Repositories;
public class UserRepository : IUserRepository
{
    private readonly TelegramBotContext _context;
    public UserRepository(TelegramBotContext context) { 
        _context = context;
    }

    public async Task<Telegram.Bot.Types.User> AddAsync(Telegram.Bot.Types.User user) {
        User userDB = new() {
            FirstName = user.FirstName,
            IsBot = user.IsBot,
            LastName = user.LastName,
            TelegramId = user.Id,
            UserName = user.Username
        };

        await _context.Users.AddAsync(userDB);
        await _context.SaveChangesAsync();

        return (await _context.Users.AddAsync(userDB)).Entity.ToDomain();
    }
    public async Task<Telegram.Bot.Types.User?> GetByTelegramIdAsync(long id)
    {
        var res = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.TelegramId == id);
        if (res == null) return null;
        return res.ToDomain();
    }

    public async Task<Telegram.Bot.Types.User?> GetByIdAsync(long id) {
        var res = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
        if (res == null) return null;
        return res.ToDomain();
    }
        
}
