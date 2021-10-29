using estacionamiento_adn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdnFabioRamos.Domain.Ports
{
    public interface ICapacidadRepository
    {
        Task<IEnumerable<VehiculosDisponiblesParqueoDTO>> GetCapacidadxPorParqueo(int codigo_parqueo);
    }
}
