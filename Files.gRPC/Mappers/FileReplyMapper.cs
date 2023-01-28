



namespace Files.gRPC.Mappers;

public static class ReplyMapper
{
    public static v1.FileDownloadReply? ToDownloadFileReply(this FileUrlModel? model) {
        if (model == null) return null;
        return new() { 
            Id = model.Id,
            Url = model.Url
        };
    }

    public static v1.FileByIdReply? ToFileByIdReply(this FileUrlModel? model) {
        if (model == null) return null;
        return new()
        {
            Id = model.Id,
            Url = model.Url
        };
    }

}
