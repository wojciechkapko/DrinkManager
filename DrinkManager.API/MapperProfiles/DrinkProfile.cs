using AutoMapper;
using BLL;
using DrinkManager.API.Contracts.Responses;

namespace DrinkManager.API.MapperProfiles
{
    public class DrinkProfile : Profile
    {
        public DrinkProfile()
        {
            CreateMap<Drink, GetDrinkListResponse>()
                .ForMember(drink => drink.Id, options => options.MapFrom(drink => drink.DrinkId));
        }
    }
}
