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

        modelBuilder.Entity<Vacation>()
            .HasOne(v => v.Statement)
            .WithMany(s => s.Vacations);

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
        User userWithUnconfirmedStatement = new()
        {
            Id = Guid.NewGuid(),
            Name = "Петров Пётр Петрович",
            Email = "PetrovPP@VacationPlanning.world"
        };

        User userWithConfirmedStatement = new()
        {
            Id = Guid.NewGuid(),
            Name = "Сидоров Сидор Сидорович",
            Email = "SidorovSS@VacationPlanning.world"
        };

        modelBuilder.Entity<User>().HasData(
            userWithUnconfirmedStatement,
            userWithConfirmedStatement
        );

        modelBuilder.Entity<Subdivision>().HasData(
            new Subdivision()
            {
                Id = Guid.NewGuid(),
                ManagerId = userWithUnconfirmedStatement.Id,
                EmployeeIds = [
                    userWithUnconfirmedStatement.Id,
                    userWithConfirmedStatement.Id
                ]
            }
        );

        Statement unconfirmedStatement = new()
        {
            Id = Guid.NewGuid(),
            UserId = userWithUnconfirmedStatement.Id
        };

        Statement confirmedStatement = new()
        {
            Id = Guid.NewGuid(),
            UserId = userWithConfirmedStatement.Id,
            IsConfirmed = true
        };

        modelBuilder.Entity<Statement>().HasData(
            unconfirmedStatement,
            confirmedStatement
        );

        modelBuilder.Entity<Vacation>().HasData(
            new Vacation()
            {
                Id = Guid.NewGuid(),
                StartDate = new DateTime(2026, 1, 10, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2026, 1, 13, 0, 0, 0, DateTimeKind.Utc),
                StatementId = confirmedStatement.Id,
            },
            new Vacation()
            {
                Id = Guid.NewGuid(),
                StartDate = new DateTime(2026, 1, 20, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2026, 1, 23, 0, 0, 0, DateTimeKind.Utc),
                StatementId = confirmedStatement.Id,
            },
            new Vacation()
            {
                Id = Guid.NewGuid(),
                StartDate = new DateTime(2026, 1, 15, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2026, 1, 18, 0, 0, 0, DateTimeKind.Utc),
                StatementId = unconfirmedStatement.Id,
            },
            new Vacation()
            {
                Id = Guid.NewGuid(),
                StartDate = new DateTime(2026, 1, 25, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2026, 1, 28, 0, 0, 0, DateTimeKind.Utc),
                StatementId = unconfirmedStatement.Id,
            }
        );
    }
}
