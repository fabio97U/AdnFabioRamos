using AdnFabioRamos.Domain.Ports;
using estacionamiento_adn.Models.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AdnFabioRamos.Application.CQRS.Movp
{
    public class CreateMovimiento : IRequest<MovimientoVehiculoPostDTO>
    {
        public MovimientoVehiculoPostDTO MovimientoVehiculo { get; set; } = new MovimientoVehiculoPostDTO();
        public class CreateMovimientoHandler : IRequestHandler<CreateMovimiento, MovimientoVehiculoPostDTO>
        {
            private readonly IMovimientoParqueo _repository;
            public CreateMovimientoHandler(IMovimientoParqueo repository)
            {
                _repository = repository;
            }

            public async Task<MovimientoVehiculoPostDTO> Handle(CreateMovimiento request, CancellationToken cancellationToken)
            {
                return await _repository.Post_GuardarMovimientoVehiculo(request.MovimientoVehiculo);
            }
        }
    }
}
