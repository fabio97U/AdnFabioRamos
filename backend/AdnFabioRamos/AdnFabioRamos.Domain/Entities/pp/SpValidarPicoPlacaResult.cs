namespace estacionamiento_adn.Models
{
    public class SpValidarPicoPlacaResult
    {
        public int Codigo { get; set; }
        public int CodigoPicoPlaca { get; set; }
        public int CodigoTipoTransporte { get; set; }
        public byte Mes { get; set; }
        public string HoraInicio { get; set; } = "";
        public string HoraFin { get; set; } = "";
        public int DiaSemana { get; set; }
        public string DiaNombre { get; set; } = "";
        public string Digito { get; set; }
        public string DigitoInicioFinal { get; set; } = "";
        public string Salida { get; set; } = "";
        public int Tipo { get; set; }
    }
}
