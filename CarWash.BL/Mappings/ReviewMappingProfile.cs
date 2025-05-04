using AutoMapper;
using CarWash.BL.DTOs;
using CarWash.Domain.Models;

namespace CarWash.BL.Mappings
{
    public class ReviewMappingProfile : Profile
    {
        public ReviewMappingProfile()
        {
            CreateMap<Review, ReviewDto>();
            CreateMap<CreateReviewDto, Review>();
        }
    }
}