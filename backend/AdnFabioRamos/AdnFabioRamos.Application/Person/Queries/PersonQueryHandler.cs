using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AdnFabioRamos.Domain.Ports;
using MediatR;

namespace AdnFabioRamos.Application.Person.Queries
{
    public class PersonQueryHandler : IRequestHandler<PersonQuery, PersonDto>, IDisposable
    {

        private readonly IGenericRepository<AdnFabioRamos.Domain.Entities.Person> _PersonRepository;
        private readonly IMapper _mapper;

        public PersonQueryHandler(IGenericRepository<AdnFabioRamos.Domain.Entities.Person> personRepository, IMapper mapper)
        {
            _PersonRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<PersonDto> Handle(PersonQuery request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException("request", "request object needed to handle this task");
            return _mapper.Map<PersonDto>(await _PersonRepository.GetByIdAsync(request.Id));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            this._PersonRepository.Dispose();
        }
    }
}