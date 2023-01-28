

namespace Files.DAL.Database;


public class FilesContext : DbContext
{
    public DbSet<Entities.File> Files { get; set; } = null!;

    public FilesContext(DbContextOptions<FilesContext> options) : base(options){
        Database.EnsureCreated();
    }
}
