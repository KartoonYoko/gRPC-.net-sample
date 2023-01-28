

namespace TelegramBot.DAL.Database.Entities;

public class PhotoSize
{
    public long Id { get; set; }
    public string TelegramFileId { get; set; }
    public string TelegramFileUniqueId { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int? FileSize { get; set; }

    public long FildeId { get; set; }
}
