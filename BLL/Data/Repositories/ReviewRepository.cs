using System.Collections.Generic;
using System.Linq;

namespace BLL.Data.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DrinkAppContext _context;

        public ReviewRepository(DrinkAppContext context)
        {
            _context = context;
        }
        public IEnumerable<Drink> GetUserReviewedDrinks(string userId) =>
            _context.Drinks.Where(x => x.DrinkReviews
                .Where(dr => dr.AppUserId.Equals(userId))
                .Select(dr => dr.DrinkId)
                .Contains(x.DrinkId)).AsEnumerable();

        public bool CanUserReviewDrink(string userId, string drinkId)
        {
            return _context.Reviews.FirstOrDefault(x => x.AppUserId.Equals(userId) && x.DrinkId.Equals(drinkId)) == null;
        }
    }
}
