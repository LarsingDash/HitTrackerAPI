using HitTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HitTrackerAPI.Database;

public class HitTrackerContext : DbContext
{
    public HitTrackerContext(DbContextOptions<HitTrackerContext> option) : base(option)
    {
    }
    
    public DbSet<Account> Accounts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Account>();
    }
}