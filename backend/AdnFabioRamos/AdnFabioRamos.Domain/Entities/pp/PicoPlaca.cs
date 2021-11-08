using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estacionamiento_adn.Models
{
    [Table("PicoPlaca", Schema = "pp")]
    public class PicoPlaca
    {
        [Key]
        public int Codigo { get; set; }
        public int Anio { get; set; }
        [Required]
        public string Descripcion { get; set; } = "";
        [Column(TypeName = "datetime")]
        public DateTime? FechaCreacion { get; set; }
    }
}
