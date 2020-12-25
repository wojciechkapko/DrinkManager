using Domain;
using System.Linq;

namespace BLL.Data.Repositories
{
    public interface IFavouriteRepository
    {
        void AddToFavourites(string userId, Drink drink);
        IQueryable<Drink> GetUserFavouriteDrinks(string userId);
        bool IsFavourite(string userId, string drinkId);
        void RemoveFromFavourites(string userId, string drinkId);
    }
}