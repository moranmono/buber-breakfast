using BuberBreakfast.Models;
using Microsoft.EntityFrameworkCore;

namespace BuberBreakfast.Persistence;

public class BuberBreakfastDbContext: DbContext
{
    public BuberBreakfastDbContext(DbContextOptions<BuberBreakfastDbContext> options): 
        base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BuberBreakfastDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Breakfast> Breakfasts { get; set; } = null!;
}
