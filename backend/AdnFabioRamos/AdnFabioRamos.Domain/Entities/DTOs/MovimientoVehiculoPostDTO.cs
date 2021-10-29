using System;

namespace estacionamiento_adn.Models.DTOs
{
    public class MovimientoVehiculoPostDTO
    {
        public int movp_codpar { get; set; }
        public string movp_placa { get; set; }
        public int movp_codtipt { get; set; }
        public int movp_cilindraje { get; set; }
        public int movp_parqueo_numero { get; set; }
    }
}
