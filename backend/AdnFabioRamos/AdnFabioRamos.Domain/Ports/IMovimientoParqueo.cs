using estacionamiento_adn.Models;
using estacionamiento_adn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdnFabioRamos.Domain.Ports
{
    public interface IMovimientoParqueo
    {
        IEnumerable<SpMovimientosParqueoResult> Getmovp_movimiento_x_parqueo(int id);
        Task<MovimientoVehiculoPostDto> Post_GuardarMovimientoVehiculo(MovimientoVehiculoPostDto _movp);
        Task<MovimientoVehiculoPutDto> PutGenerarTicket(Guid id, MovimientoVehiculoPutDto movp);
    }
}
