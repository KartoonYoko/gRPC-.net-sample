


namespace Notifications.DAL.Database;

public class NotificationContext : DbContext
{
    public NotificationContext(DbContextOptions<NotificationContext> options) : base(options) {
        Database.EnsureCreated();
    }

    public DbSet<Account> TelegramAccounts { get; set; } = null!;
    public DbSet<AccountUpdates> AccountUpdates { get; set; } = null!;
    public DbSet<Entities.Update> Updates { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Account>()
            .HasIndex(a => a.Token)
            .IsUnique();

        modelBuilder.Entity<AccountUpdates>()
            .HasKey(au => new { au.UpdateId, au.AccountId });
        modelBuilder.Entity<AccountUpdates>()
            .HasOne(au => au.Update)
            .WithMany(u => u.Accounts)
            .HasForeignKey(au => au.UpdateId);
        modelBuilder.Entity<AccountUpdates>()
            .HasOne(au => au.Account)
            .WithMany(a => a.Updates)
            .HasForeignKey(au => au.AccountId);

    }
}
