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
            var iconsDirectory = AppConfig.GetResourcePath("AssetIcons");
            // FinancialAssetDto -> FinancialAsset
            CreateMap<FinancialAssetDto, FinancialAsset>()
                .ForMember(dest => dest.IconPath, opt => opt.MapFrom(src => Path.Combine(iconsDirectory, src.IconFilename)));

            // FinancialAsset -> FinancialAssetDto
            CreateMap<FinancialAsset, FinancialAssetDto>()
                .ForMember(dest => dest.IconFilename, opt => opt.MapFrom(src => Path.GetFileName(src.IconPath)));
        }
    }
}
