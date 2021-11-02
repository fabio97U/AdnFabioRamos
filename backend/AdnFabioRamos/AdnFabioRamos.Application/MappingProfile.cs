using AutoMapper;
using estacionamiento_adn.Models;
using estacionamiento_adn.Models.DTOs;

namespace AdnFabioRamos.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AdnFabioRamos.Domain.Entities.Person, AdnFabioRamos.Application.Person.Queries.PersonDto>();
            CreateMap<AdnFabioRamos.Application.Person.Queries.PersonDto, AdnFabioRamos.Domain.Entities.Person>();


            SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
            DestinationMemberNamingConvention = new PascalCaseNamingConvention();

            AllowNullCollections = true;
            CreateMap<movp_movimiento_parqueo, MovimientoVehiculoPostDTO>();


            CreateMap<MovimientoVehiculoPostDTO, movp_movimiento_parqueo>()
                .ForMember(
                dest => dest.movp_hora_entrada,
                org => org.MapFrom(src => src.fecha_ingreso));
        }
    }
}
