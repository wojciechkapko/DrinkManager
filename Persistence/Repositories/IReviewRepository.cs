using Domain;
using System.Linq;

namespace Persistence.Repositories
{
    public interface IReviewRepository
    {
        IQueryable<Drink> GetUserReviewedDrinks(string userId);

        bool CanUserReviewDrink(string userId, string drinkId);
    }
}