using AutoMapper;
using CarWash.Domain.Models;
using CarWash.BL.DTOs;

namespace CarWash.BL.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map between User and AuthDtos
            CreateMap<User, RegisterDto>().ReverseMap();
            CreateMap<User, LoginDto>().ReverseMap();

            // Map between WashRequest and WashRequestDto
            CreateMap<WashRequest, WashRequestDto>().ReverseMap();

            // Map LeaderboardEntry to LeaderboardDto
            CreateMap<Leaderboard, LeaderboardDto>();
        }
    }
}