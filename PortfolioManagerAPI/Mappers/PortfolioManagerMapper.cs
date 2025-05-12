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

            CreateMap<FinancialAsset, FinancialAssetDto>().ReverseMap();
            CreateMap<PortfolioAsset, PortfolioAssetDto>().ReverseMap();
            CreateMap<Portfolio, PortfolioDto>().ReverseMap();

            // PortfolioAsset -> PortfolioAssetDto
            CreateMap<PortfolioAsset, PortfolioAssetDto>()
                .ForMember(dest => dest.FinancialAssetDto, opt => opt.MapFrom(src => src.FinancialAsset));

            // PortfolioAssetDto -> PortfolioAsset
            CreateMap<PortfolioAssetDto, PortfolioAsset>()
                .ForMember(dest => dest.FinancialAsset, opt => opt.MapFrom(src => src.FinancialAssetDto));
        }
    }
}
