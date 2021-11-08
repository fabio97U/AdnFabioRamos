namespace estacionamiento_adn.Models.DTOs
{
    public class VehiculosDisponiblesParqueoDTO
    {
        public int CodigoParqueo { get; set; }
        public string ParqueoNombre { get; set; } = "";
        public int CodigoTipoTransporte { get; set; }
        public string TipoTransporte { get; set; } = "";
        public int Capacidad { get; set; }
        public double ValorHora { get; set; }
        public double ValorDia { get; set; }
    }
}
