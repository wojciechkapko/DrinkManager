using Domain;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public interface IReviewRepository
    {
        IQueryable<DrinkReview> GetDrinkReviews(string drinkId);
        Task<bool> AddReview(DrinkReview newReview);
    }
}