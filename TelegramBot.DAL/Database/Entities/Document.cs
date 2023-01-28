


namespace TelegramBot.DAL.Database.Entities;

public class Document
{
    public long Id { get; set; }
    public string TelegramFileId { get; set; }
    public string TelegramFileUniqueId { get; set; }
    public string? FileName { get; set; }
    public string? MimeType { get; set; }
    public int? FileSize { get; set; }


    public long? ThumbId { get; set; }
    public PhotoSize? Thumb { get; set; }

    public long FileId { get; set; }

}
