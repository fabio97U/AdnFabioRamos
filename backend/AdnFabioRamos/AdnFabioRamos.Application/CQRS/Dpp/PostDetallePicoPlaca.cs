using AdnFabioRamos.Domain.Ports;
using estacionamiento_adn.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdnFabioRamos.Application.CQRS.Dpp
{
    public class PostDetallePicoPlaca : IRequest<dpp_detalle_pico_placa>
    {
        public dpp_detalle_pico_placa _dpp_detalle_pico_placa { get; set; }

        public class PostDetallePicoPlacaHandler : IRequestHandler<PostDetallePicoPlaca, dpp_detalle_pico_placa>
        {
            private readonly IDetallePicoPlaca _DetallePicoPlacaRepository;
            public PostDetallePicoPlacaHandler(IDetallePicoPlaca repository)
            {
                _DetallePicoPlacaRepository = repository;
            }

            public async Task<dpp_detalle_pico_placa> Handle(PostDetallePicoPlaca request, CancellationToken cancellationToken)
            {
                return await _DetallePicoPlacaRepository.PostDetallePicoPlaca(request._dpp_detalle_pico_placa);
            }
        }
    }
}
