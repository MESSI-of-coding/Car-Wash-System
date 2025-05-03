using AutoMapper;
using CarWash.Domain.Models;
using CarWash.BL.DTOs;

namespace CarWash.BL.Mappings
{
    public class WashRequestMappingProfile : Profile
    {
        public WashRequestMappingProfile()
        {
            // Map between WashRequest and WashRequestDto
            CreateMap<WashRequest, WashRequestDto>().ReverseMap();
        }
    }
}