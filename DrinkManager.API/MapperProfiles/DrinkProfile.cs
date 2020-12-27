using AutoMapper;
using BLL.Contracts.Responses;
using Domain;

namespace DrinkManager.API.MapperProfiles
{
    public class DrinkProfile : Profile
    {
        public DrinkProfile()
        {
            CreateMap<Drink, DrinkListResponse>()
                .ForMember(drink => drink.Id, options => options.MapFrom(drink => drink.DrinkId));
            CreateMap<Drink, DrinkDetailsResponse>();
        }
    }
}
