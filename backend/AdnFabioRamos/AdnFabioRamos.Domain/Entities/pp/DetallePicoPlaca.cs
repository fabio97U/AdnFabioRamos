using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estacionamiento_adn.Models
{
    [Table("DetallePicoPlaca", Schema = "pp")]
    public class DetallePicoPlaca
    {
        [Key]
        public int Codigo { get; set; }
        public int CodigoPicoPlaca { get; set; }
        public int CodigoTipoTransporte { get; set; }
        /// <summary>
        /// 1: Enero ... 12: Diciembre
        /// </summary>
        public byte Mes { get; set; }
        [Required]
        public string HoraInicio { get; set; } = "";
        [Required]
        public string HoraFin { get; set; } = "";
        /// <summary>
        /// 0: Domingo ... 6: Sabado
        /// </summary>
        public int DiaSemana { get; set; }
        public string Digito { get; set; }
        /// <summary>
        /// Que la placa I: Inicie, F: Finalice
        /// </summary>
        [Required]
        public string DigitoInicioFinal { get; set; } = "";
        [Column(TypeName = "datetime")]
        public DateTime? FechaCreacion { get; set; }
    }
}
