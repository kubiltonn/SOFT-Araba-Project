using Microsoft.EntityFrameworkCore;
using static anaproject.Models.Cars;

namespace anaproject.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<TestDrive> TestDrives { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<ServiceAppointment> ServiceAppointments { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Örnek: Car ve Category ilişkisi
            modelBuilder.Entity<Car>()
                .HasOne(c => c.Category)
                .WithMany(cat => cat.Cars)
                .HasForeignKey(c => c.CategoryId);

            // Car ve Color ilişkisi
            modelBuilder.Entity<Car>()
                .HasOne(c => c.Color)
                .WithMany(col => col.Cars)
                .HasForeignKey(c => c.ColorId);

            // Car ve Package ilişkisi
            modelBuilder.Entity<Car>()
                .HasOne(c => c.Package)
                .WithMany(p => p.Cars)
                .HasForeignKey(c => c.PackageId);

            // Car ve CarImage ilişkisi
            modelBuilder.Entity<CarImage>()
      .HasKey(c => c.ImageId);  // Primary Key tanımı

            modelBuilder.Entity<CarImage>()
                .HasOne(ci => ci.Car)
                .WithMany(c => c.CarImages)
                .HasForeignKey(ci => ci.CarId)
                .OnDelete(DeleteBehavior.Cascade);  // Car silinirse resimleri de sil



            // Car ve Review ilişkisi
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Car)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CarId);

            // User ve Review ilişkisi
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId);

            // Car ve TestDrive ilişkisi
            modelBuilder.Entity<TestDrive>()
                .HasOne(td => td.Car)
                .WithMany(c => c.TestDrives)
                .HasForeignKey(td => td.CarId);

            // User ve TestDrive ilişkisi
            modelBuilder.Entity<TestDrive>()
                .HasOne(td => td.User)
                .WithMany()
                .HasForeignKey(td => td.UserId);

            // Product configuration
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Product>()
                .Property(p => p.Category)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Product>()
                .Property(p => p.Description)
                .HasMaxLength(1000);

            modelBuilder.Entity<Product>()
                .Property(p => p.ImageUrl)
                .HasMaxLength(500);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}