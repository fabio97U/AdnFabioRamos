using estacionamiento_adn.Models;
using estacionamiento_adn.Models.DTOs;
using System.Threading.Tasks;

namespace AdnFabioRamos.Domain.Ports
{
    public interface IDetallePicoPlaca
    {
        Task<RespuestaPicoPlaca> GetconsultarPicoPlacaAsync(int tipo_vehiculo, string placa);
        Task<DetallePicoPlaca> PostDetallePicoPlacaAsync(DetallePicoPlaca dpp_Detalle_Pico_Placa);
    }
}
