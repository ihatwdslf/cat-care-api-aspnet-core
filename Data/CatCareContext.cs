using CatCareApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CatCareApi.Data;

public class CatCareContext : DbContext
{
    public CatCareContext(DbContextOptions<CatCareContext> options) : base(options)
    {
    }

    public DbSet<Owner> Owners => Set<Owner>();
    public DbSet<Pet> Pets => Set<Pet>();
    public DbSet<ServiceType> ServiceTypes => Set<ServiceType>();
    public DbSet<Caretaker> Caretakers => Set<Caretaker>();
    public DbSet<Application> Applications => Set<Application>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Owner configuration
        modelBuilder.Entity<Owner>()
            .HasIndex(o => o.PhoneNumber)
            .IsUnique();

        // Pet configuration
        modelBuilder.Entity<Pet>()
            .HasOne(p => p.Owner)
            .WithMany()
            .HasForeignKey(p => p.OwnerId)
            .HasConstraintName("FK_Pets_Owners_OwnerId")
            .OnDelete(DeleteBehavior.Cascade);

        // Application configuration
        modelBuilder.Entity<Application>()
            .HasOne(a => a.Pet)
            .WithMany()
            .HasForeignKey(a => a.PetId)
            .HasConstraintName("FK_Applications_Pets_PetId")
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Application>()
            .HasOne(a => a.Owner)
            .WithMany()
            .HasForeignKey(a => a.OwnerId)
            .HasConstraintName("FK_Applications_Owners_OwnerId")
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Application>()
            .HasOne(a => a.ServiceType)
            .WithMany()
            .HasForeignKey(a => a.ServiceTypeId)
            .HasConstraintName("FK_Applications_ServiceTypes_ServiceTypeId")
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Application>()
            .HasOne(a => a.Caretaker)
            .WithMany()
            .HasForeignKey(a => a.CaretakerId)
            .HasConstraintName("FK_Applications_Caretakers_CaretakerId")
            .OnDelete(DeleteBehavior.SetNull);

        base.OnModelCreating(modelBuilder);
    }
}