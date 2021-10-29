﻿using AdnFabioRamos.Domain.Ports;
using estacionamiento_adn.Models.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdnFabioRamos.Application.CQRS.Dpp
{
    public class GetDetallePicoPlacaByPlacaVehiculoQuery : IRequest<RespuestaPicoPlaca>
    {
        public int tipo_vehiculo { get; set; }
        public string placa { get; set; }
        public class GetDetallePicoPlacaHandler : IRequestHandler<GetDetallePicoPlacaByPlacaVehiculoQuery, RespuestaPicoPlaca>
        {
            private readonly IDetallePicoPlaca _DetallePicoPlacaRepository;
            public GetDetallePicoPlacaHandler(IDetallePicoPlaca repository)
            {
                _DetallePicoPlacaRepository = repository;
            }

            public async Task<RespuestaPicoPlaca> Handle(GetDetallePicoPlacaByPlacaVehiculoQuery request, CancellationToken cancellationToken)
            {
                return await _DetallePicoPlacaRepository.GetconsultarPicoPlaca(request.tipo_vehiculo, request.placa);
            }
        }
    }
}