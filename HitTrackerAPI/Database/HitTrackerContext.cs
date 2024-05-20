using HitTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HitTrackerAPI.Database;

public class HitTrackerContext(DbContextOptions<HitTrackerContext> option) : DbContext(option)
{
    // ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
    public DbSet<Account> Accounts { get; init; } = null!;
    public DbSet<Run> Runs { get; init; } = null!;
    public DbSet<Split> Splits { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Account>();
        modelBuilder.Entity<Run>();
        modelBuilder.Entity<Split>();
    }
}