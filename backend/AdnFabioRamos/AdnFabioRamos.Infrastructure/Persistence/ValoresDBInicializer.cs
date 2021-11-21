using System;

namespace AdnFabioRamos.Infrastructure.Persistence
{
    public class ValoresDBInicializer
    {
        public int CodigoParqueo { get; set; } = 1;
        public int CodigoPicoPlaca { get; set; } = 1;
        public int Anio { get; set; } = 2021;
        public int CodigoMoto { get; set; } = 1;
        public int CodigoCarro { get; set; } = 2;
        public int MesActual { get; set; } = 11;
        public int DiaSemanaActual { get; set; } = (int)DateTime.Now.DayOfWeek;
        public string HoraInicio { get; set; } = "00:00";
        public string HoraFin { get; set; } = "23:59";
        public string DigitoInicioFinal { get; set; } = "I";

        public short CapacidadMoto { get; set; } = 10;
        public short CapacidadCarro { get; set; } = 20;

        public int ValorHoraMoto { get; set; } = 500;
        public int ValorHoraCarro { get; set; } = 1000;

        public int ValorDiaMoto { get; set; } = 4000;
        public int ValorDiaCarro { get; set; } = 8000;
    }
}
