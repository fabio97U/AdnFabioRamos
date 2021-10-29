using System;

namespace estacionamiento_adn.Models.DTOs
{
    public class MovimientoVehiculoPutDTO
    {
        public int tipt_codigo { get; set; }
        public int cap_valor_hora { get; set; }
        public int cap_valor_dia { get; set; }
        public Guid movp_codigo { get; set; }
        public string movp_placa { get; set; }
        public int movp_total_pagar { get; set; }
    }
}
