using AdnFabioRamos.Domain.Ports;
using AutoMapper;
using estacionamiento_adn.Models;
using estacionamiento_adn.Models.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

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
                    IEnumerable<SpMovimientosParqueoResult> rowsParqueos = await _repository.GetMovimientosxParqueoAsync(request.MovimientoVehiculo.CodigoParqueo);

                    if (rowsParqueos.Any(x => x.CodigoTipoTransporte == request.MovimientoVehiculo.CodigoTipoTransporte && x.HoraEntrada == null))
                    {
                        var respuestaPicoPlaca = await _detallePicoPlaca.GetconsultarPicoPlacaAsync(request.MovimientoVehiculo.CodigoTipoTransporte, request.MovimientoVehiculo.Placa);

                        request.MovimientoVehiculo.PermitirSalirAhora = respuestaPicoPlaca.PermitirSalirAhora;
                        request.MovimientoVehiculo.DiasPermitidosSalir = respuestaPicoPlaca.DiasPermitidosSalir;

                        return respuestaPicoPlaca.PermitirSalirAhora
                            ? await _repository.PostGuardarMovimientoVehiculoAsync(request.MovimientoVehiculo)
                            : request.MovimientoVehiculo;
                    }
                    else
                    {
                        throw new System.ArgumentNullException(nameof(request), "Capacidad excedida");
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
