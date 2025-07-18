using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace DataAccess;

public class DatabaseContext : DbContext
{
    public DbSet<Subdivision> Subdivisions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Statement> Statements { get; set; }
    public DbSet<Vacation> Vacations { get; set; }

    public DatabaseContext() 
    {
        Database.EnsureCreated();
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Subdivision>()
            .HasMany(s => s.Employees)
            .WithOne(e => e.Subdivision);

        modelBuilder.Entity<Subdivision>()
            .HasOne(s => s.Manager);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Statement)
            .WithOne(s => s.User)
            .HasForeignKey<Statement>();

        modelBuilder.Entity<Statement>()
            .HasMany(s => s.Vacations)
            .WithOne(v => v.Statement);

        // Keys
        modelBuilder.Entity<Subdivision>().HasKey(i => i.Id);
        modelBuilder.Entity<User>().HasKey(i => i.Id);
        modelBuilder.Entity<Statement>().HasKey(i => i.Id);
        modelBuilder.Entity<Vacation>().HasKey(i => i.Id);

        Seed(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            "Host=localhost;" +
            "Port=5432;" +
            "Database=VacationPlanning;" +
            "Username=postgres;" +
            "Password=superpass"
        );

        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    }

    private static void Seed(ModelBuilder modelBuilder)
    {
        User user = new()
        {
            Id = Guid.NewGuid(),
            Name = "Петров Пётр Петрович",
            Email = "PetrovPP@VacationPlanning.world"
        };

        modelBuilder.Entity<User>().HasData(
            user 
        );

        modelBuilder.Entity<Subdivision>().HasData(
            new Subdivision()
            {
                Id = Guid.NewGuid(),
                ManagerId = user.Id,
                EmployeeIds = [user.Id]
            }
        );
    }
}
