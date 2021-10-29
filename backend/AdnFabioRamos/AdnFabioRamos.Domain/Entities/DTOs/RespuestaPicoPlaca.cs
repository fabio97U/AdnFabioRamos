namespace estacionamiento_adn.Models.DTOs
{
    public class RespuestaPicoPlaca
    {
        public bool permitir_salir_ahora { get; set; } = false;
        public string dias_permitidos_salir { get; set; } = "";
    }
}
