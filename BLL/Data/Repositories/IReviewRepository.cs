using System.Collections.Generic;

namespace BLL.Data.Repositories
{
    public interface IReviewRepository
    {
        IEnumerable<Drink> GetUserReviewedDrinks(string userId);

        bool CanUserReviewDrink(string userId, string drinkId);
    }
}