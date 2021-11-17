using AutoMapper;
using estacionamiento_adn.Models;
using estacionamiento_adn.Models.DTOs;

namespace AdnFabioRamos.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
            DestinationMemberNamingConvention = new PascalCaseNamingConvention();

            AllowNullCollections = true;
            CreateMap<MovimientoParqueo, MovimientoVehiculoPostDto>();


            CreateMap<MovimientoVehiculoPostDto, MovimientoParqueo>()
                .ForMember(
                dest => dest.HoraEntrada,
                org => org.MapFrom(src => src.FechaIngreso));

            CreateMap<RespuestaPicoPlaca, MovimientoVehiculoPostDto>();
        }
    }
}
