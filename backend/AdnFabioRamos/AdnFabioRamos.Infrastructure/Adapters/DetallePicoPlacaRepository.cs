using AdnFabioRamos.Domain.Ports;
using AdnFabioRamos.Infrastructure.Persistence;
using estacionamiento_adn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdnFabioRamos.Infrastructure.Adapters
{
    public class DetallePicoPlacaRepository : IDetallePicoPlaca
    {
        readonly Adn_CeibaContext _context;
        private readonly Adn_CeibaContextProcedures _contextProcedures;
        public DetallePicoPlacaRepository(Adn_CeibaContext context)
        {
            _context = context;
            _contextProcedures = new Adn_CeibaContextProcedures(context);
        }
        public async Task<RespuestaPicoPlaca> GetconsultarPicoPlaca(int tipo_vehiculo, string placa)
        {
            var lst_dpp_detalle_pico_placa = _contextProcedures.sp_validar_pico_placaAsync(tipo_vehiculo, placa).Result.ToList();
            var respuesta = new RespuestaPicoPlaca();

            if (lst_dpp_detalle_pico_placa.Where(x => x.tipo == 0).Count() > 0)//Tiene permitido salir ahora
                respuesta.permitir_salir_ahora = true;

            lst_dpp_detalle_pico_placa.Where(x => x.tipo == 1).ToList().ForEach(x =>
                respuesta.dias_permitidos_salir += x.dia_nombre + " de " + x.dpp_hora_inicio + " a " + x.dpp_hora_fin + " "
            );

            return respuesta;
        }
    }
}
