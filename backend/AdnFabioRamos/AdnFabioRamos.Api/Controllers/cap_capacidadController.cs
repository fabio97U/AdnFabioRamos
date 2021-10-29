using AdnFabioRamos.Application.CQRS.Cap;
using AdnFabioRamos.Infrastructure.Persistence;
using estacionamiento_adn.Models;
using estacionamiento_adn.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdnFabioRamos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class cap_capacidadController : ControllerBase
    {
        private readonly IMediator _Mediator;

        public cap_capacidadController(IMediator mediator)
        {
            _Mediator = mediator;
        }

        [HttpGet("{id}")]
        public Task<IEnumerable<VehiculosDisponiblesParqueoDTO>> Getcap_capacidad(int id) => _Mediator.Send(new GetCapacidadByIdQuery { codigo_parqueo = id });
    }
}
