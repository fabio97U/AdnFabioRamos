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
    public class MovimientosParqueoController : ControllerBase
    {
        private readonly IMediator _Mediator;

        public MovimientosParqueoController(IMediator mediator)
        {
            _Mediator = mediator;
        }

        [HttpGet("codpar/{id}")]
        public Task<IEnumerable<SpMovimientosParqueoResult>> Getmovp_movimiento_x_parqueo(int id) => _Mediator.Send(new GetMovimientosParqueByIdQuery { CodigoParqueo = id });

        [HttpPost("GuardarMovimientoVehiculo")]
        public Task<MovimientoVehiculoPostDTO> Post_GuardarMovimientoVehiculo(MovimientoVehiculoPostDTO _movp)
         => _Mediator.Send(new CreateMovimiento { MovimientoVehiculo = _movp });

        [HttpPut("GenerarTicket/{id}")]
        public Task<MovimientoVehiculoPutDTO> PutGenerarTicket(Guid id, MovimientoVehiculoPutDTO movp)
         => _Mediator.Send(new GenerarTicket { IdParqueo = id, MovimientoVehiculoPut = movp });
    }
}
