using Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BLL.Data.Repositories
{
    public class FavouriteRepository : IFavouriteRepository
    {
        private readonly DrinkAppContext _context;

        public FavouriteRepository(DrinkAppContext context)
        {
            _context = context;
        }

        public void AddToFavourites(string userId, Drink drink)
        {
            if (IsFavourite(userId, drink.DrinkId))
            {
                return;
            }

            _context.UserDrinks.Add(new UserDrink
            {
                DrinkId = drink.DrinkId,
                Drink = drink,
                AppUserId = userId,
                AppUser = _context.Users.Find(userId)
            });
            _context.SaveChanges();
        }

        public IQueryable<Drink> GetUserFavouriteDrinks(string userId)
        {
            return _context.UserDrinks.Include(x => x.Drink).ThenInclude(x => x.DrinkReviews).Where(x => x.AppUserId == userId).Select(x => x.Drink);
        }

        public bool IsFavourite(string userId, string drinkId)
        {
            return _context.UserDrinks.FirstOrDefault(x => x.AppUserId == userId && x.DrinkId == drinkId) != null;
        }

        public void RemoveFromFavourites(string userId, string drinkId)
        {
            var toBeRemoved = _context.UserDrinks.FirstOrDefault(x => x.AppUserId == userId && x.DrinkId == drinkId);
            if (toBeRemoved != null)
            {
                _context.UserDrinks.Remove(toBeRemoved);
                _context.SaveChanges();
            }
        }
    }
}
