using System;

namespace estacionamiento_adn.Models.DTOs
{
    public class MovimientoVehiculoPutDTO
    {
        public int tipt_codigo { get; set; }
        public int cap_valor_hora { get; set; }
        public int cap_valor_dia { get; set; }
        public Guid movp_codigo { get; set; }
        public string? movp_placa { get; set; }

        public int movp_cilindraje { get; set; }
        public DateTime movp_hora_entrada { get; set; }

        public DateTime hora_salida { get; set; }
        public double cantidad_horas { get; set; }
        public double dias_9h { get; set; }
        public double horas_restantes { get; set; }
        public double total_pagar_dias_9h { get; set; }
        public double total_pagar_horas_restantes { get; set; }
        public double cantidad_pagar { get; set; }
    }
}
