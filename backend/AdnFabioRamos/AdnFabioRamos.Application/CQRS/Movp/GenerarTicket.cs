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

namespace AdnFabioRamos.Application.CQRS.Movp
{

    public class GenerarTicket : IRequest<MovimientoVehiculoPutDTO>
    {
        public Guid id { get; set; }
        public MovimientoVehiculoPutDTO movimientoVehiculoPutDTO { get; set; }

        public class GenerarTicketHandler : IRequestHandler<GenerarTicket, MovimientoVehiculoPutDTO>
        {
            private readonly IMovimientoParqueo _repository;
            public GenerarTicketHandler(IMovimientoParqueo repository)
            {
                _repository = repository;
            }

            public async Task<MovimientoVehiculoPutDTO> Handle(GenerarTicket request, CancellationToken cancellationToken)
            {
                return await _repository.PutGenerarTicket(request.id, request.movimientoVehiculoPutDTO);
            }
        }
    }
}
