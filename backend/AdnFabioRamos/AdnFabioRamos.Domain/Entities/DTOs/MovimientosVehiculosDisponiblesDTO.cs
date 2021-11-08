using System;

namespace estacionamiento_adn.Models.DTOs
{
    public class MovimientosVehiculosDisponiblesDto
    {
        public int CodigoParqueo { get; set; }
        public string NombreParqueo { get; set; } = "";
        public int CodigoTipoTransporte { get; set; }
        public string TipoTransporte { get; set; } = "";
        public int Capacidad { get; set; }
        public double ValorHora { get; set; }
        public double ValorDia { get; set; }

        public int Numero { get; set; }
        public Guid CodigoMovimiento { get; set; }
        public int MovimientoCodigoParqueo { get; set; }
        public string Placa { get; set; } = "";
        public int MovimientoCodigoTipoTransporte { get; set; }
        public int Cilindraje { get; set; }
        public int MovimientoParqueoNumero { get; set; }
        public DateTime MovimientoHoraEntrada { get; set; }
        public DateTime MovimientoHoraSalida { get; set; }
        public float TotalPagar { get; set; }
        public DateTime MovimientoFechaCreacion { get; set; }
    }
}
