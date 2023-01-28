




namespace Files.gRPC.Services.v1;

public class FileService : gRPC.v1.FileService.FileServiceBase
{
    private readonly IFileService _fileService;
    public FileService(IFileService fileService) {
        _fileService = fileService;
    }

    public override async Task<FileDownloadReply?> DownloadFile(FileDownloadRequest request, ServerCallContext context)
    {
        return (await _fileService.AddFile(request.Url)).ToDownloadFileReply();
    }

    public override async Task<FileByIdReply?> GetFileById(FileByIdRequest request, ServerCallContext context)
    {
        return (await _fileService.GetFileById(request.Id)).ToFileByIdReply();
    }

}
