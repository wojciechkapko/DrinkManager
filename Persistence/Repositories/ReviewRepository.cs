using Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Persistence.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DrinkAppContext _context;

        public ReviewRepository(DrinkAppContext context)
        {
            _context = context;
        }

        public IQueryable<Drink> GetUserReviewedDrinks(string userId) =>
            _context.Reviews
                .Include(r => r.Drink)
                .ThenInclude(r => r.DrinkReviews)
                .Where(r => r.Author.Id.Equals(userId))
                .Select(r => r.Drink);

        public bool CanUserReviewDrink(string userId, string drinkId)
        {
            return _context.Reviews.FirstOrDefault(x => x.Author.Id.Equals(userId) && x.Drink.DrinkId.Equals(drinkId)) == null;
        }
    }
}
