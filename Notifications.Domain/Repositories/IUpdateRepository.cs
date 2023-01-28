


namespace Notifications.Domain.Repositories;
public interface IUpdateRepository
{
    Task AddAsync(long accId, long updateId);
    Task<long?> GetAsync(long accId, long updateId);
    Task<long?> GetLastAsync(long accId);
}
