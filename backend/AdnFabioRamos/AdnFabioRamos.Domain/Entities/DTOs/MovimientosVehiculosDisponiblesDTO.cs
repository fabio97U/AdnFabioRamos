using System;

namespace estacionamiento_adn.Models.DTOs
{
    public class MovimientosVehiculosDisponiblesDTO
    {
        public int par_codigo { get; set; }
        public string par_nombre { get; set; }
        public int tipt_codigo { get; set; }
        public string tipt_tipo { get; set; }
        public int cap_capacidad { get; set; }
        public double cap_valor_hora { get; set; }
        public double cap_valor_dia { get; set; }

        public int numero { get; set; }
        public Guid movp_codigo { get; set; }
        public int movp_codpar { get; set; }
        public string movp_placa { get; set; }
        public int movp_codtipt { get; set; }
        public int movp_cilindraje { get; set; }
        public int movp_parqueo_numero { get; set; }
        public DateTime movp_hora_entrada { get; set; }
        public DateTime movp_hora_salida { get; set; }
        public float movp_total_pagar { get; set; }
        public DateTime movp_fecha_creacion { get; set; }
        public DateTime movp_fecha_salida { get; set; }
    }
}
