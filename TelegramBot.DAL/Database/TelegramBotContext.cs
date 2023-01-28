


namespace TelegramBot.DAL.Database;


public class TelegramBotContext : DbContext
{
    public DbSet<Message> Messages { get; set; } = null!;
    public DbSet<Chat> Chats { get; set; } = null!;
    public DbSet<Entities.Account> Accounts { get; set; } = null!;
    public DbSet<Audio> Audios { get; set; } = null!;
    public DbSet<Document> Documents { get; set; } = null!;
    public DbSet<ChatPermissions> ChatPermissions { get; set; } = null!;
    public DbSet<PhotoSize> Photos { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Video> Videos { get; set; } = null!;

    public TelegramBotContext(DbContextOptions<TelegramBotContext> options) : base(options) {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChatPermissions>()
            .HasKey(cp => new { cp.UserId, cp.ChatId });
        modelBuilder.Entity<ChatPermissions>()
            .HasOne(cp => cp.User)
            .WithMany(u => u.Permissions)
            .HasForeignKey(cp => cp.UserId);
        modelBuilder.Entity<ChatPermissions>()
            .HasOne(cp => cp.Chat)
            .WithMany(c => c.Permissions)
            .HasForeignKey(cp => cp.ChatId);

        modelBuilder.Entity<Entities.Account>()
            .HasOne(a => a.User)
            .WithOne(w => w.Account)
            .HasForeignKey<User>(w => w.AccountId);
        modelBuilder.Entity<Entities.Account>()
            .HasIndex(a => a.Token)
            .IsUnique();
    }
}
