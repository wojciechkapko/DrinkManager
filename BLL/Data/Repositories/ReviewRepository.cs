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
            _context.Reviews
                .Where(r => r.Author.Id.Equals(userId))
                .Select(r => r.Drink)
                .AsEnumerable();

        public bool CanUserReviewDrink(string userId, string drinkId)
        {
            return _context.Reviews.FirstOrDefault(x => x.Author.Id.Equals(userId) && x.Drink.DrinkId.Equals(drinkId)) == null;
        }
    }
}
