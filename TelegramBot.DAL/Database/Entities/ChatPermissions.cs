

namespace TelegramBot.DAL.Database.Entities;


public class ChatPermissions
{
    public long UserId { get; set; }
    public User User { get; set; }

    public long ChatId { get; set; }
    public Chat Chat { get; set; }

    public bool CanSendMessages { get; set; }
    public bool CanSendMediaMessages { get; set; }
    public bool CanSendPolls { get; set; }
    public bool CanSendOtherMessages { get; set; }
    public bool CanAddWebPagePreviews { get; set; }
    public bool CanChangeInfo { get; set; }
    public bool CanInviteUsers { get; set; }
    public bool CanPinMessages { get; set; }
}


