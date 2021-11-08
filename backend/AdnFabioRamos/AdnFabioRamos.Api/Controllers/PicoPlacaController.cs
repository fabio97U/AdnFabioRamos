using AdnFabioRamos.Application.CQRS.Dpp;
using AutoMapper;
using estacionamiento_adn.Models;
using estacionamiento_adn.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdnFabioRamos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicoPlacaController : ControllerBase
    {
        private readonly IMediator _Mediator;
        public PicoPlacaController(IMediator mediator)
        {
            _Mediator = mediator;
        }

        [HttpGet("consultarPicoPlaca/{tipo_vehiculo}/{placa}")]
        public Task<RespuestaPicoPlaca> GetconsultarPicoPlaca(int tipo_vehiculo, string placa)
         => _Mediator.Send(new GetDetallePicoPlacaByPlacaVehiculoQuery { TipoVehiculo = tipo_vehiculo, Placa = placa });


        [HttpPost]
        public Task<DetallePicoPlaca> Postdpp_detalle_pico_placa(DetallePicoPlaca dpp_detalle_pico_placa)
         => _Mediator.Send(new PostDetallePicoPlaca { DetallePicoPlaca = dpp_detalle_pico_placa });
    }
}
