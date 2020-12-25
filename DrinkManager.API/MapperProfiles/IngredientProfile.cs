using AutoMapper;
using BLL.Contracts.Responses;
using Domain;

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
