using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using CarWash.Domain.Models;

namespace CarWash.DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Represents tables in the database
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<WashRequest> WashRequests { get; set; }
        public DbSet<WashPackage> WashPackages { get; set; }
        public DbSet<AddOn> AddOns { get; set; }
        public DbSet<WashRequestAddOn> WashRequestAddOns { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Leaderboard> Leaderboard { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Primary Keys
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<Role>().HasKey(r => r.RoleId);
            modelBuilder.Entity<Car>().HasKey(c => c.CarId);
            modelBuilder.Entity<WashRequest>().HasKey(w => w.RequestId);
            modelBuilder.Entity<WashPackage>().HasKey(w => w.PackageId);
            modelBuilder.Entity<AddOn>().HasKey(a => a.AddOnId);
            modelBuilder.Entity<Review>().HasKey(r => r.ReviewId); // Configure primary key for Review
            modelBuilder.Entity<Leaderboard>().HasKey(l => l.EntryId); // Configure EntryId as the primary key for Leaderboard

            // Composite Keys
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<WashRequestAddOn>().HasKey(w => new { w.RequestId, w.AddOnId });

            // Configure Geospatial Data (User.Location)
            modelBuilder.Entity<User>()
                .Property(u => u.Location)
                .HasColumnType("geometry"); // Map to GEOMETRY column
            // If you need to map DTOs, ensure you map latitude/longitude to Point (X=longitude, Y=latitude)

            // Configure Enum Mapping (WashRequest.Status)
            modelBuilder.Entity<WashRequest>()
                .Property(w => w.Status)
                .HasConversion<string>(); // Store as string in DB

            // Relationships
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<WashRequestAddOn>()
                .HasOne(w => w.WashRequest)
                .WithMany(w => w.WashRequestAddOns)
                .HasForeignKey(w => w.RequestId);

            modelBuilder.Entity<WashRequestAddOn>()
                .HasOne(w => w.AddOn)
                .WithMany()
                .HasForeignKey(w => w.AddOnId);

            // Relationships for WashRequest
            modelBuilder.Entity<WashRequest>()
                .HasOne(w => w.Customer)
                .WithMany()
                .HasForeignKey(w => w.CustomerId)
                .OnDelete(DeleteBehavior.NoAction); // Prevent cascade delete

            modelBuilder.Entity<WashRequest>()
                .HasOne(w => w.Washer)
                .WithMany()
                .HasForeignKey(w => w.WasherId)
                .OnDelete(DeleteBehavior.NoAction); // Prevent cascade delete

            modelBuilder.Entity<WashRequest>()
                .HasOne(w => w.Car)
                .WithMany()
                .HasForeignKey(w => w.CarId)
                .OnDelete(DeleteBehavior.NoAction); // Optional: Adjust as needed

            modelBuilder.Entity<WashRequest>()
                .HasOne(w => w.Package)
                .WithMany()
                .HasForeignKey(w => w.PackageId)
                .OnDelete(DeleteBehavior.NoAction); // Optional: Adjust as needed

            // Configure decimal precision for AddOn.Price
            modelBuilder.Entity<AddOn>()
                .Property(a => a.Price)
                .HasPrecision(18, 2); // Adjust precision/scale as needed

            // Configure decimal precision for WashPackage.Price
            modelBuilder.Entity<WashPackage>()
                .Property(w => w.Price)
                .HasPrecision(18, 2); // Adjust precision/scale as needed

            // Configure Review entity
            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.ReviewId); // Use ReviewId as primary key

                entity.Property(e => e.Rating)
                    .IsRequired();

                entity.Property(e => e.Comments) // Use Comments (not Comment)
                    .HasMaxLength(500);

                entity.Property(e => e.ReviewDate)
                    .IsRequired();

                entity.HasOne(e => e.WashRequest) // Ensure WashRequest navigation property exists
                    .WithMany()
                    .HasForeignKey(e => e.RequestId) // Use RequestId
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User) // Define the relationship
                .WithMany(u => u.Notifications) // A user can have many notifications
                .HasForeignKey(n => n.UserId) // Foreign key in Notification
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete notifications when a user is deleted
        }
    }
}