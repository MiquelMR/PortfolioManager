using AutoMapper;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;

namespace PortfolioManagerAPI.Mappers
{
    public class PortfolioManagerMapper : Profile
    {
        public PortfolioManagerMapper()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<Asset, AssetDto>().ReverseMap();
        }
    }
}
