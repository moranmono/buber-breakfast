using BuberBreakfast.Models;
using BuberBreakfast.Persistence;
using BuberBreakfast.ServiceErrors;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{
    private readonly BuberBreakfastDbContext _breakfastsDbContext;

    public BreakfastService(BuberBreakfastDbContext buberBreakfastDbContext)
    {
        _breakfastsDbContext = buberBreakfastDbContext;
    }

    public ErrorOr<Created> CreateBreakfast(Breakfast breakfast)
    {
        _breakfastsDbContext.Add(breakfast);
        _breakfastsDbContext.SaveChanges();
        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteBreakfast(Guid id)
    {
        var breakfast = _breakfastsDbContext.Breakfasts.Find(id);
        if (breakfast is null)
        {
            return Errors.Breakfast.NotFound;
        }
        _breakfastsDbContext.Remove(breakfast);
        _breakfastsDbContext.SaveChanges();

        return Result.Deleted;
    }

    public ErrorOr<Breakfast> GetBreakfast(Guid id)
    {
        if (_breakfastsDbContext.Breakfasts.Find(id) is Breakfast breakfast)
        {
            return breakfast;
        }

        return Errors.Breakfast.NotFound;
    }

    public ErrorOr<UpsertedBreakfast> UpsertBreakfast(Breakfast breakfast)
    {
        var isNewlyCreated = _breakfastsDbContext.Breakfasts.Find(breakfast.Id) is not Breakfast dbBreakfast;
        if (isNewlyCreated)
        {
            _breakfastsDbContext.Add(breakfast);
        }
        else
        {
            _breakfastsDbContext.Update(breakfast);
        }
        _breakfastsDbContext.SaveChanges();
        return new UpsertedBreakfast(isNewlyCreated);
    }
}
