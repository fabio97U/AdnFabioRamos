using AdnFabioRamos.Domain.Ports;
using estacionamiento_adn.Models.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AdnFabioRamos.Application.CQRS.Cap
{
    public class GetCapacidadByIdQuery : IRequest<IEnumerable<VehiculosDisponiblesParqueoDto>>
    {
        public int CodigoParqueo { get; set; }
        public class GetCapacidadQueryHandler : IRequestHandler<GetCapacidadByIdQuery, IEnumerable<VehiculosDisponiblesParqueoDto>>
        {
            private readonly ICapacidadRepository _capacidadRepository;
            public GetCapacidadQueryHandler(ICapacidadRepository capacidadRepository)
            {
                _capacidadRepository = capacidadRepository;
            }

            public async Task<IEnumerable<VehiculosDisponiblesParqueoDto>> Handle(GetCapacidadByIdQuery request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    request = new GetCapacidadByIdQuery();
                }

                return await _capacidadRepository.GetCapacidadxPorParqueo(request.CodigoParqueo);
            }
        }
    }
}
