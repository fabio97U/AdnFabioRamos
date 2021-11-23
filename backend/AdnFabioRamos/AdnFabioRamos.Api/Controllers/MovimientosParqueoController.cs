using AdnFabioRamos.Application.CQRS.Movp;
using estacionamiento_adn.Models;
using estacionamiento_adn.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        [HttpGet("Codigo/{id}")]
        public Task<IEnumerable<SpMovimientosParqueoResult>> GetMovimientosxParqueo(int id) => _Mediator.Send(new GetMovimientosParqueByIdQuery { CodigoParqueo = id });

        [HttpPost("GuardarMovimientoVehiculo")]
        public Task<MovimientoVehiculoPostDto> PostGuardarMovimientoVehiculo(MovimientoVehiculoPostDto _movp)
         => _Mediator.Send(new CreateMovimiento { MovimientoVehiculo = _movp });

        [HttpPut("GenerarTicket/{id}")]
        public Task<MovimientoVehiculoPutDto> PutGenerarTicket(Guid id, MovimientoVehiculoPutDto movp)
         => _Mediator.Send(new GenerarTicket { IdParqueo = id, MovimientoVehiculoPut = movp });
    }
}
