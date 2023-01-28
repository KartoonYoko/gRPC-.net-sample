

namespace TelegramBot.Domain.Repositories;
public interface IUserRepository
{
    Task<User> AddAsync(User user);
    Task<User?> GetByTelegramIdAsync(long id);
    Task<User?> GetByIdAsync(long id);
}
