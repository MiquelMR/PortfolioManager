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
            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Asset, AssetDto>().ReverseMap();

            CreateMap<Portfolio, PortfolioDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<PortfolioAsset, PortfolioDto>()
                .ForMember(dest => dest.PortfolioAssets, opt => opt.MapFrom(src => src));
        }
    }
}
