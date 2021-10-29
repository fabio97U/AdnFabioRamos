using estacionamiento_adn.Models;
using estacionamiento_adn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdnFabioRamos.Domain.Ports
{
    public interface IMovimientoParqueo
    {
        Task<IEnumerable<sp_movimientos_parqueoResult>> Getmovp_movimiento_x_parqueo(int id);
        Task<MovimientoVehiculoPostDTO> Post_GuardarMovimientoVehiculo(MovimientoVehiculoPostDTO _movp);
        Task<MovimientoVehiculoPutDTO> PutGenerarTicket(Guid id, MovimientoVehiculoPutDTO movp);
    }
}
