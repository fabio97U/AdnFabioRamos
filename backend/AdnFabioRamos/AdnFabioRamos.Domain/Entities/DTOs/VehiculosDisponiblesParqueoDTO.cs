namespace estacionamiento_adn.Models.DTOs
{
    public class VehiculosDisponiblesParqueoDTO
    {
        public int par_codigo { get; set; }
        public string par_nombre { get; set; }
        public int tipt_codigo { get; set; }
        public string tipt_tipo { get; set; }
        public int cap_capacidad { get; set; }
        public double cap_valor_hora { get; set; }
        public double cap_valor_dia { get; set; }
    }
}
