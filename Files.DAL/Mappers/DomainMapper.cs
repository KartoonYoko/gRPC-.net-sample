




namespace Files.DAL.Mappers;


internal static class DomainMapper
{
    public static FileModel? ToDomain(this Database.Entities.File? file) {
        if (file == null) return null;
        return new()
        {
            Id = file.Id,
            Hash = file.Hash,
            Path = file.Path
        };
    }
}
