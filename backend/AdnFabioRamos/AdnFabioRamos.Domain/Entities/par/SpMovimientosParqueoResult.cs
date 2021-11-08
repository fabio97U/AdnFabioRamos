using System;

namespace estacionamiento_adn.Models
{
    public partial class SpMovimientosParqueoResult
    {
        public int CodigoParqueo { get; set; }
        public string NombreParqueo { get; set; } = "";
        public int CodigoTipoTransporte { get; set; }
        public string TipoTransporte { get; set; } = "";
        public short Capacidad { get; set; }
        public double ValorHora { get; set; }
        public double ValorDia { get; set; }
        public int? Numero { get; set; }
        public Guid? CodigoMovimientoParqueo { get; set; }
        public int? MovimientoCodigoParqueo { get; set; }
        public string? Placa { get; set; } = "";
        public int? MovimientoCodigoTipoTransporte { get; set; }
        public int? Cilindraje { get; set; }
        public short? ParqueoNumero { get; set; }
        public DateTime? HoraEntrada { get; set; }
        public DateTime? HoraSalida { get; set; }
        public double? TotalPagar { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
