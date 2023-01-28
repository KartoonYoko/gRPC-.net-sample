

namespace TelegramBot.DAL.Database.Entities;

public class Audio
{
    public long Id { get; set; }
    public string FileId { get; set; }
    public string FuleUniqueId { get; set; }
    public int Duration { get; set; }
    public string? Performer { get; set; }
    public string? Title { get; set; }
    public string? FileName { get; set; }
    public string? MimeType { get; set; }
    public int? FileSize { get; set; }

    public long ThumbId { get; set; }
    public PhotoSize? Thumb { get; set; }
}
