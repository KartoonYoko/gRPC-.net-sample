

namespace Files.Domain.Services;


public interface IFileService
{
    Task<FileUrlModel> AddFile(string Url);
    Task<FileUrlModel?> GetFileById(long id);
}
