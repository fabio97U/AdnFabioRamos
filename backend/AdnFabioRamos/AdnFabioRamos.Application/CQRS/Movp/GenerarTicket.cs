using AdnFabioRamos.Application.Utilidades;
using AdnFabioRamos.Domain.Ports;
using estacionamiento_adn.Models.DTOs;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AdnFabioRamos.Application.CQRS.Movp
{

    public class GenerarTicket : IRequest<MovimientoVehiculoPutDTO>
    {
        public Guid IdParqueo { get; set; }
        public MovimientoVehiculoPutDTO MovimientoVehiculoPut { get; set; } = new MovimientoVehiculoPutDTO();

        public class GenerarTicketHandler : IRequestHandler<GenerarTicket, MovimientoVehiculoPutDTO>
        {
            private readonly IMovimientoParqueo _repository;
            public GenerarTicketHandler(IMovimientoParqueo repository)
            {
                _repository = repository;
            }

            public async Task<MovimientoVehiculoPutDTO> Handle(GenerarTicket request, CancellationToken cancellationToken)
            {
                ParqueoLogica.CalcularTotalPagar(request.MovimientoVehiculoPut);

                return await _repository.PutGenerarTicket(request.IdParqueo, request.MovimientoVehiculoPut);
            }
        }
    }
}
