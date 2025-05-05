using AutoMapper;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;
using PortfolioManagerAPI.Models.DTOs.UserDto;

namespace PortfolioManagerAPI.Mappers
{
    public class PortfolioManagerMapper : Profile
    {
        public PortfolioManagerMapper()
        {
            // Users Dto
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<UserUpdateDto, User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UserDto, UserUpdateDto>().ReverseMap();
            CreateMap<UserDto, UserRegisterDto>().ReverseMap();

            CreateMap<Asset, AssetDto>().ReverseMap();
            CreateMap<PortfolioAsset, PortfolioAssetDto>().ReverseMap();
            CreateMap<Portfolio, PortfolioDto>().ReverseMap();
        }
    }
}
