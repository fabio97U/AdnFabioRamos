using estacionamiento_adn.Models;
using estacionamiento_adn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdnFabioRamos.Domain.Ports
{
    public interface IMovimientoParqueo
    {
        Task<IEnumerable<SpMovimientosParqueoResult>> GetMovimientosxParqueoAsync(int id);
        Task<MovimientoVehiculoPostDto> PostGuardarMovimientoVehiculoAsync(MovimientoVehiculoPostDto movimientoPost);
        Task<MovimientoVehiculoPutDto> PutGenerarTicketAsync(Guid id, MovimientoVehiculoPutDto movimientoPut);
    }
}
