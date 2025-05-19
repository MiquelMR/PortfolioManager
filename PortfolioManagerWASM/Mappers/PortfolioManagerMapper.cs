using AutoMapper;
using PortfolioManagerWASM.Helpers;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Models.DTOs;

namespace PortfolioManagerWASM.Mappers
{
    public class PortfolioManagerMapper : Profile
    {
        public PortfolioManagerMapper()
        {
            var assetIconsDirectory = AppConfig.GetResourcePath("AssetIcons");
            var portfolioIconsDirectory = AppConfig.GetResourcePath("PortfolioIcons");

            // FinancialAssetDto -> FinancialAsset
            CreateMap<FinancialAssetDto, FinancialAsset>()
                .ForMember(dest => dest.IconPath, opt => opt.MapFrom(src => Path.Combine(assetIconsDirectory, src.IconFilename)));

            // FinancialAsset -> FinancialAssetDto
            CreateMap<FinancialAsset, FinancialAssetDto>()
                .ForMember(dest => dest.IconFilename, opt => opt.MapFrom(src => Path.GetFileName(src.IconPath)));

            // PortfolioDto -> Portfolio
            CreateMap<PortfolioDto, Portfolio>()
                .ForMember(dest => dest.IconPath, opt => opt.MapFrom(src => Path.Combine(portfolioIconsDirectory, src.IconFilename)));

            // Portfolio -> PortfolioDto
            CreateMap<Portfolio, PortfolioDto>()
                .ForMember(dest => dest.IconFilename, opt => opt.MapFrom(src => Path.GetFileName(src.IconPath)));

            // PortfolioAsset -> PortfolioAssetDto
            CreateMap<PortfolioAsset, PortfolioAssetDto>()
                .ForMember(dest => dest.FinancialAssetDto, opt => opt.MapFrom(src => src.FinancialAsset));

            // PortfolioAssetDto -> PortfolioAsset
            CreateMap<PortfolioAssetDto, PortfolioAsset>()
                .ForMember(dest => dest.FinancialAsset, opt => opt.MapFrom(src => src.FinancialAssetDto));
        }
    }
}
