using AdnFabioRamos.Domain.Ports;
using AdnFabioRamos.Infrastructure.Persistence;
using estacionamiento_adn.Models.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdnFabioRamos.Application.CQRS.Cap
{
    public class GetCapacidadByIdQuery: IRequest<IEnumerable<VehiculosDisponiblesParqueoDTO>>
    {
        public int codigo_parqueo { get; set; }
        public class GetCapacidadQueryHandler: IRequestHandler<GetCapacidadByIdQuery, IEnumerable<VehiculosDisponiblesParqueoDTO>>
        {
            private readonly ICapacidadRepository _capacidadRepository;
            public GetCapacidadQueryHandler(ICapacidadRepository capacidadRepository)
            {
                _capacidadRepository = capacidadRepository;
            }
            
            public async Task<IEnumerable<VehiculosDisponiblesParqueoDTO>> Handle(GetCapacidadByIdQuery request, CancellationToken cancellationToken)
            {
                return await _capacidadRepository.GetCapacidadxPorParqueo(request.codigo_parqueo);
            }

            private Task<IEnumerable<VehiculosDisponiblesParqueoDTO>> NotFound(object p)
            {
                throw new NotImplementedException();
            }
        }
    }
}
