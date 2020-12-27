using Domain;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DrinkAppContext _context;

        public ReviewRepository(DrinkAppContext context)
        {
            _context = context;
        }

        public IQueryable<DrinkReview> GetDrinkReviews(string drinkId)
        {
            return _context.Reviews.Where(review => review.DrinkId.Equals(drinkId));
        }

        public async Task<bool> AddReview(DrinkReview newReview)
        {
            _context.Reviews.Add(newReview);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
