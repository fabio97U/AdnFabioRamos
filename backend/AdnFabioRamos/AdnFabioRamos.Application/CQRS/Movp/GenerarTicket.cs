using AdnFabioRamos.Application.Utilidades;
using AdnFabioRamos.Domain.Ports;
using estacionamiento_adn.Models.DTOs;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AdnFabioRamos.Application.CQRS.Movp
{

    public class GenerarTicket : IRequest<MovimientoVehiculoPutDto>
    {
        public Guid IdParqueo { get; set; }
        public MovimientoVehiculoPutDto MovimientoVehiculoPut { get; set; } = new MovimientoVehiculoPutDto();

        public class GenerarTicketHandler : IRequestHandler<GenerarTicket, MovimientoVehiculoPutDto>
        {
            private readonly IMovimientoParqueo _repository;
            public GenerarTicketHandler(IMovimientoParqueo repository)
            {
                _repository = repository;
            }

            public async Task<MovimientoVehiculoPutDto> Handle(GenerarTicket request, CancellationToken cancellationToken)
            {
                if (request != null)
                {
                    ParqueoLogica.CalcularTotalPagar(request.MovimientoVehiculoPut);

                    return await _repository.PutGenerarTicketAsync(request.IdParqueo, request.MovimientoVehiculoPut);
                }
                else
                {
                    throw new System.ArgumentNullException(nameof(request), "No se envio el Movimiento y Guid");
                }
            }
        }
    }
}
