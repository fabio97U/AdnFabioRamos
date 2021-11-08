using System;

namespace estacionamiento_adn.Models.DTOs
{
    public class MovimientoVehiculoPutDTO
    {
        public int CodigoTipoTransporte { get; set; }
        public int ValorHora { get; set; }
        public int ValorDia { get; set; }
        public Guid CodigoMovimiento { get; set; }
        public string? Placa { get; set; }

        public int Cilindraje { get; set; }
        public DateTime HoraEntrada { get; set; }

        public DateTime HoraSalida { get; set; }
        public double CantidadHoras { get; set; }
        public double Dias9h { get; set; }
        public double HorasRestantes { get; set; }
        public double TotalPagarDias9h { get; set; }
        public double TotalPagarHorasRestantes { get; set; }
        public double CantidadPagar { get; set; }
    }
}
