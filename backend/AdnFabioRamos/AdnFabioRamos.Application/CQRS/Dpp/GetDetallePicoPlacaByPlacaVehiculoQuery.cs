using AdnFabioRamos.Domain.Ports;
using estacionamiento_adn.Models.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AdnFabioRamos.Application.CQRS.Dpp
{
    public class GetDetallePicoPlacaByPlacaVehiculoQuery : IRequest<RespuestaPicoPlaca>
    {
        public int TipoVehiculo { get; set; }
        public string Placa { get; set; } = "";
        public class GetDetallePicoPlacaHandler : IRequestHandler<GetDetallePicoPlacaByPlacaVehiculoQuery, RespuestaPicoPlaca>
        {
            private readonly IDetallePicoPlaca _DetallePicoPlacaRepository;
            public GetDetallePicoPlacaHandler(IDetallePicoPlaca repository)
            {
                _DetallePicoPlacaRepository = repository;
            }

            public async Task<RespuestaPicoPlaca> Handle(GetDetallePicoPlacaByPlacaVehiculoQuery request, CancellationToken cancellationToken)
            {
                if (request != null)
                {
                    return await _DetallePicoPlacaRepository.GetconsultarPicoPlaca(request.TipoVehiculo, request.Placa);
                }
                else
                {
                    throw new System.ArgumentNullException(nameof(request), "No se envio el Tipo Vehiculo y Placa");
                }
            }
        }
    }
}
