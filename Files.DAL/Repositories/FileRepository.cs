




namespace Files.DAL.Repositories;

public class FileRepository : IFileRepository
{
    private readonly FilesContext _filesContext;
    public FileRepository(FilesContext context) {
        _filesContext = context;
    }

    public async Task<FileModel> AddFile(AddFileModel model) {
        var old = await _filesContext.Files.FirstOrDefaultAsync(f => f.Hash == model.Hash);

        if (old == null) {
            var newEntity = new Database.Entities.File() {
                Hash = model.Hash,
                Path = model.Path,
            };

            await _filesContext.Files.AddAsync(newEntity);
            await _filesContext.SaveChangesAsync();
            return newEntity.ToDomain()!;
        }

        return old.ToDomain()!;
    }

    public async Task<FileModel?> GetFileById(long id) => 
        (await _filesContext.Files.FindAsync(id)).ToDomain();

    public async Task<FileModel?> GetFileByHash(string hash) => 
        (await _filesContext.Files.SingleOrDefaultAsync(f => f.Hash == hash)).ToDomain();


}
