using System.Collections.Generic;

namespace BLL.Services
{
    public interface IFavouriteService
    {
        void AddToFavourites(string userId, Drink drink);
        IEnumerable<Drink> GetUserFavouriteDrinks(string userId);
        bool IsFavourite(string userId, string drinkId);
        void RemoveFromFavourites(string userId, string drinkId);
    }
}