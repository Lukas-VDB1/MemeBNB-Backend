using AutoMapper;
using Programming_Web_API.DTO;
using Programming_Web_API.Models;

namespace Programming_Web_API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<CampingSpot, CampingSpotDto>();
            CreateMap<CampingSpotDto, CampingSpot>();

            CreateMap<Booking, BookingDto>();
            CreateMap<BookingDto, Booking>();

            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();
        }

    }
}
