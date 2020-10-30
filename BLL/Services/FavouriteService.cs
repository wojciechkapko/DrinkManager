using BLL.Data.Repositories;
using System.Collections.Generic;

namespace BLL.Services
{
    public class FavouriteService : IFavouriteService
    {
        private readonly IFavouriteRepository _favouriteRepository;

        public FavouriteService(IDrinkRepository drinkRepository, IFavouriteRepository favouriteRepository)
        {
            _favouriteRepository = favouriteRepository;
        }

        public void AddToFavourites(string userId, Drink drink)
        {
            _favouriteRepository.AddToFavourites(userId, drink);
        }

        public IEnumerable<Drink> GetUserFavouriteDrinks(string userId)
        {
            return _favouriteRepository.GetUserFavouriteDrinks(userId);
        }

        public bool IsFavourite(string userId, string drinkId)
        {
            return _favouriteRepository.IsFavourite(userId, drinkId);
        }

        public void RemoveFromFavourites(string userId, string drinkId)
        {
            _favouriteRepository.RemoveFromFavourites(userId, drinkId);
        }
    }
}
