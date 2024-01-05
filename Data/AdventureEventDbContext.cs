using Adventure_MVC.Models;
using Microsoft.EntityFrameworkCore;

public class AdventureEventDbContext : DbContext
{
    public AdventureEventDbContext(DbContextOptions<AdventureEventDbContext> options)
        : base(options)
    {
    }

    // DbSets for each model
    public DbSet<User> Users { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<UserEvent> UserEvents { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure many-to-many relationship between User and Event
        modelBuilder.Entity<UserEvent>()
            .HasKey(ue => new { ue.UserID, ue.EventID });

        modelBuilder.Entity<UserEvent>()
            .HasOne(ue => ue.User)
            .WithMany(u => u.UserEvents)
            .HasForeignKey(ue => ue.UserID);

        modelBuilder.Entity<UserEvent>()
            .HasOne(ue => ue.Event)
            .WithMany(e => e.UserEvents)
            .HasForeignKey(ue => ue.EventID);
    }
}
