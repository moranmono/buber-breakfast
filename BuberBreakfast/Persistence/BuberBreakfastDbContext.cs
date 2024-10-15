using BuberBreakfast.Models;
using Microsoft.EntityFrameworkCore;

namespace BuberBreakfast.Persistence;

public class BuberBreakfastDbContext: DbContext
{
    public BuberBreakfastDbContext(DbContextOptions<BuberBreakfastDbContext> options): 
        base(options)
    {
        
    }

    public DbSet<Breakfast> Breakfasts { get; set; } = null!;
}
