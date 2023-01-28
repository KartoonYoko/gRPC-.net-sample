



namespace Files.Domain.Repositories;

public interface IFileRepository
{
    Task<FileModel> AddFile(AddFileModel addFileModel);
    Task<FileModel?> GetFileById(long id);
    Task<FileModel?> GetFileByHash(string hash);

}
