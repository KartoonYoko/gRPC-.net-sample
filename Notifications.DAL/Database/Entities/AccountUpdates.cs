namespace Notifications.DAL.Database.Entities;
public class AccountUpdates
{
    public long AccountId { get; set; }
    public Account Account { get; set; }


    public long UpdateId { get; set; }
    public Update Update { get; set; }
}
