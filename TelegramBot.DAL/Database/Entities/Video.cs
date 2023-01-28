

namespace TelegramBot.DAL.Database.Entities;

public class Video
{
    public long Id { get; set; }
    public string FileId { get; set; }

    public string FileUniqueId { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int Duration { get; set; }
    public string? FileName { get; set; }
    public string? MimeType { get; set; }
    public int? FileSize { get; set; }

    public long? ThumbId { get; set; }
    public PhotoSize? Thumb { get; set; }
}
