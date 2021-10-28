using AutoMapper;

namespace AdnFabioRamos.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AdnFabioRamos.Domain.Entities.Person, AdnFabioRamos.Application.Person.Queries.PersonDto>();
            CreateMap<AdnFabioRamos.Application.Person.Queries.PersonDto, AdnFabioRamos.Domain.Entities.Person>();           
        }
    }
}
