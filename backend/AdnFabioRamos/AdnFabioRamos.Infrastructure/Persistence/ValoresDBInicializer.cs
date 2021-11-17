using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdnFabioRamos.Infrastructure.Persistence
{
    public class ValoresDBInicializer
    {
        public int McodigoParqueo { get; set; } = 1;
        public int Manio { get; set; } = 2021;
        public int McodigoPicoPlaca { get; set; } = 1;
        public int McodigoMoto { get; set; } = 1;
        public int McodigoCarro { get; set; } = 2;
        public int MesActual { get; set; } = 11;
        public int MdiaSemanaActual { get; set; } = 1;
        public string MhoraInicio { get; set; } = "00:00";
        public string MhoraFin { get; set; } = "23:59";

        public short CapacidadMoto { get; set; } = 10;
        public short CapacidadCarro { get; set; } = 20;

        public int ValorHoraMoto { get; set; } = 500;
        public int ValorHoraCarro { get; set; } = 1000;

        public int ValorDiaMoto { get; set; } = 4000;
        public int ValorDiaCarro { get; set; } = 8000;
    }
}
