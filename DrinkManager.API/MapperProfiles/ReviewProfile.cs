using AutoMapper;
using BLL.Contracts.Responses;
using Domain;

namespace DrinkManager.API.MapperProfiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<DrinkReview, ReviewResponse>();
        }
    }
}
