using AdnFabioRamos.Domain.Ports;
using estacionamiento_adn.Models.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AdnFabioRamos.Application.CQRS.Movp
{
    public class CreateMovimiento : IRequest<MovimientoVehiculoPostDto>
    {
        public MovimientoVehiculoPostDto MovimientoVehiculo { get; set; } = new MovimientoVehiculoPostDto();
        public class CreateMovimientoHandler : IRequestHandler<CreateMovimiento, MovimientoVehiculoPostDto>
        {
            private readonly IMovimientoParqueo _repository;
            public CreateMovimientoHandler(IMovimientoParqueo repository)
            {
                _repository = repository;
            }

            public async Task<MovimientoVehiculoPostDto> Handle(CreateMovimiento request, CancellationToken cancellationToken)
            {
                if (request != null)
                {
                    return await _repository.Post_GuardarMovimientoVehiculo(request.MovimientoVehiculo);
                }
                else
                {
                    throw new System.ArgumentNullException(nameof(request), "No se envio el Movimiento");
                }
            }
        }
    }
}
