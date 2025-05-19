using CatCareApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CatCareApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Application> Applications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>()
            .HasIndex(a => a.PhoneNumber)
            .IsUnique(); // номер телефону визначає унікальність
    }
}