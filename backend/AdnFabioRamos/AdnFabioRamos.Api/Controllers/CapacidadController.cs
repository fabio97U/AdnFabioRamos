using AdnFabioRamos.Application.CQRS.Cap;
using estacionamiento_adn.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdnFabioRamos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CapacidadController : ControllerBase
    {
        private readonly IMediator _Mediator;

        public CapacidadController(IMediator mediator)
        {
            _Mediator = mediator;
        }

        [HttpGet("{id}")]
        public Task<IEnumerable<VehiculosDisponiblesParqueoDto>> GetCapacidad(int id) => _Mediator.Send(new GetCapacidadByIdQuery { CodigoParqueo = id });
    }
}
