namespace estacionamiento_adn.Models.DTOs
{
    public class RespuestaPicoPlaca
    {
        public bool PermitirSalirAhora { get; set; } = false;
        public string DiasPermitidosSalir { get; set; } = "";
    }
}
