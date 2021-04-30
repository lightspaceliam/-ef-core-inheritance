using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class PocDbContext : DbContext
    {
        public PocDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions) { }

        public PocDbContext() { }

        public DbSet<User> Users { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<AnnouncementPreference> AnnouncementPreferences { get; set; }
        public DbSet<UserPreference> UserPreferences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Inheritance

            modelBuilder.Entity<AnnouncementPreference>()
                .HasBaseType(typeof(Preference));

            modelBuilder.Entity<UserPreference>()
                .HasBaseType(typeof(Preference));

            modelBuilder.Entity<Preference>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<AnnouncementPreference>("AnnouncementPreference")
                .HasValue<UserPreference>("UserPreference");

            #endregion

            #region Relational Configuration

            modelBuilder.Entity<Preference>()
                .HasOne(e => e.User)
                .WithMany(e => e.Preferences)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion
        }
    }
}
