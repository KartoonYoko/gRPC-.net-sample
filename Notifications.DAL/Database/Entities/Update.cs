namespace Notifications.DAL.Database.Entities;
public class Update
{
    public long Id { get; set; }

    public List<AccountUpdates> Accounts { get; set; } = new();
}
