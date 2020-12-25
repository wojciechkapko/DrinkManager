using AutoMapper;
using Domain;
using DrinkManager.API.Contracts.Responses;

namespace DrinkManager.API.MapperProfiles
{
    public class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<Ingredient, GetDrinkListResponse.DrinkListIngredient>();

        }
    }
}
