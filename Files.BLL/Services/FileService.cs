


namespace Files.BLL.Services;

public class FileService : IFileService
{
    private readonly IFileRepository _fileRepository;
    private readonly GlobalOptions _globalOptions;
    private static HttpClient _httpClient;


    static FileService() {
        _httpClient = new();
    }
    public FileService(IFileRepository repository, GlobalOptions globalOptions)
    {
        _fileRepository = repository;
        _globalOptions = globalOptions;
    }

    public async Task<FileUrlModel> AddFile(string url) {
        var bytes = await DownloadUrl(url);
        var hash = GetHash(bytes);
        
        var file = await _fileRepository.GetFileByHash(hash);

        if (file == null) {
            var path = await SaveFileToFolder(bytes, hash);
            var fileModel = await _fileRepository.AddFile(new AddFileModel() { 
                Hash = hash,
                Path = path
            });
            return new() { 
                Id = fileModel.Id,
                Url = GetUrlFile(fileModel.Hash)
            };
        }

        return new()
        {
            Id = file.Id,
            Url = GetUrlFile(file.Hash)
        };
    }

    public async Task<FileUrlModel?> GetFileById(long id) {
        var file = await _fileRepository.GetFileById(id);
        if (file == null) return null;
        return new FileUrlModel()
        {
            Id = file.Id,
            Url = GetUrlFile(file.Hash)
        };
    }

    private async Task<byte[]> DownloadUrl(string url) {
        return await _httpClient.GetByteArrayAsync(url);
    }

    private string GetHash(byte[] file)
    {
        using SHA256 sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(file);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    }

    private async Task<string> SaveFileToFolder(byte[] bytes, string hash)
    {
        var fileName = GetFullFileName(hash);
        if (File.Exists(fileName)) return fileName;

        using var fStream = File.Create(fileName);
        await fStream.WriteAsync(bytes);

        return fileName;
    }

    private string GetFullFileName(string hash) => _globalOptions.FileFolder +  @"\" + hash;
    private string GetUrlFile(string name) => "https://" + _globalOptions.HostName + "/StaticFiles/" + name;

}
