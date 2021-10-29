using AdnFabioRamos.Application.CQRS.Movp;
using AutoMapper;
using estacionamiento_adn.Models;
using estacionamiento_adn.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdnFabioRamos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class movp_movimiento_parqueoController : ControllerBase
    {
        private readonly IMediator _Mediator;

        public movp_movimiento_parqueoController(IMediator mediator)
        {
            _Mediator = mediator;
        }

        [HttpGet("codpar/{id}")]
        public Task<IEnumerable<sp_movimientos_parqueoResult>> Getmovp_movimiento_x_parqueo(int id) => _Mediator.Send(new GetMovimientosParqueByIdQuery { codigo_parqueo = id });

        [HttpPost("GuardarMovimientoVehiculo")]
        public Task<MovimientoVehiculoPostDTO> Post_GuardarMovimientoVehiculo(MovimientoVehiculoPostDTO _movp)
         => _Mediator.Send(new CreateMovimiento { mov = _movp });

        [HttpPut("GenerarTicket/{id}")]
        public Task<MovimientoVehiculoPutDTO> PutGenerarTicket(Guid id, MovimientoVehiculoPutDTO movp)
         => _Mediator.Send(new GenerarTicket { id = id, movimientoVehiculoPutDTO = movp });
    }
}