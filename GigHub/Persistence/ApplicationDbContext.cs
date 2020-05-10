using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GigHub.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // There is maybe a better way to do this. 
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Gig>()
                .HasMany(g => g.Attendees)
                .WithMany(u => u.Gigs)
                .Map(m =>
                {
                    m.ToTable("Attendances");
                    m.MapLeftKey("GigId");
                    m.MapRightKey("AttendeeId");
                });

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Followers)
                .WithRequired(f => f.Followee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Followees)
                .WithRequired(f => f.Follower)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Notification>()
                .HasRequired(n => n.Gig)
                .WithMany()
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}