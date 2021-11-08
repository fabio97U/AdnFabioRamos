using estacionamiento_adn.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdnFabioRamos.Domain.Ports
{
    public interface ICapacidadRepository
    {
        Task<IEnumerable<VehiculosDisponiblesParqueoDto>> GetCapacidadxPorParqueo(int codigo_parqueo);
    }
}
