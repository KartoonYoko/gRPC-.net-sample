

namespace TelegramBot.DAL.Database.Entities;


public class Account
{
    public long Id { get; set; }
    public string Token { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }

    
    public long? WebhookInfoId { get; set; }    

}
