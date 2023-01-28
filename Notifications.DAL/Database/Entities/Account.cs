
namespace Notifications.DAL.Database.Entities;
public class Account
{
    public long Id { get; set; }
    public string Token { get; set; }


    public string Url { get; set; }
    public bool HasCustomCertificate { get; set; }
    public int PendingUpdateCount { get; set; }
    public string? IpAddress { get; set; }
    public DateTime? LastErrorDate { get; set; }
    public string? LastErrorMessage { get; set; }
    public DateTime? LastSynchronizationErrorDate { get; set; }
    public int? MaxConnections { get; set; }
    public string? AllowedUpdates { get; set; }

    public List<AccountUpdates> Updates { get; set; } = new();
}
