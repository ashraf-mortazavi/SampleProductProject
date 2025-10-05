using Microsoft.EntityFrameworkCore;
using SampleProductProject.Models;
using System.Reflection;

namespace SampleProductProject.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserProduct> UserProducts { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<UserProduct>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<UserProduct>()
            .HasIndex(p => new { p.UserId, p.Title })
            .IsUnique();

        modelBuilder.Entity<UserProduct>()
            .HasIndex(p => new { p.UserId, p.Code })
            .IsUnique();

        modelBuilder.Entity<UserProduct>()
            .Property(p => p.Title)
            .HasMaxLength(100)
            .IsRequired(true);

        modelBuilder.Entity<UserProduct>()
            .Property(p => p.Code)
            .IsRequired(true)
            .HasMaxLength(100);

        modelBuilder.Entity<UserProduct>()
            .Property(p => p.Price)
            .IsRequired(false);

        modelBuilder.Entity<UserProduct>()
            .HasOne(p => p.User)
            .WithMany(u => u.Products)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

