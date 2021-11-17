using AdnFabioRamos.Domain.Ports;
using AutoMapper;
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
            private readonly IDetallePicoPlaca _detallePicoPlaca;
            private readonly IMapper _mapper;

            public CreateMovimientoHandler(IMovimientoParqueo repository, IDetallePicoPlaca detallePicoPlaca, IMapper mapper)
            {
                _repository = repository;
                _detallePicoPlaca = detallePicoPlaca;
                _mapper = mapper;
            }

            public async Task<MovimientoVehiculoPostDto> Handle(CreateMovimiento request, CancellationToken cancellationToken)
            {
                if (request != null)
                {
                    var respuestaPicoPlaca = await _detallePicoPlaca.GetconsultarPicoPlaca(request.MovimientoVehiculo.CodigoTipoTransporte, request.MovimientoVehiculo.Placa);

                    _mapper.Map(respuestaPicoPlaca, request.MovimientoVehiculo);

                    if (respuestaPicoPlaca.PermitirSalirAhora)
                    {
                        return await _repository.Post_GuardarMovimientoVehiculo(request.MovimientoVehiculo);
                    }
                    else
                    {
                        return request.MovimientoVehiculo;
                    }
                }
                else
                {
                    throw new System.ArgumentNullException(nameof(request), "No se envio el Movimiento");
                }
            }
        }
    }
}
