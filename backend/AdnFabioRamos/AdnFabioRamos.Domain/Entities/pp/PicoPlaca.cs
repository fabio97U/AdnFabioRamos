using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estacionamiento_adn.Models
{
    [Table("PicoPlaca", Schema = "pp")]
    public partial class PicoPlaca
    {
        [Key]
        public int Codigo { get; set; }
        public int Anio { get; set; }
        [Required]
        [StringLength(255)]
        public string Descripcion { get; set; } = "";
        [Column(TypeName = "datetime")]
        public DateTime? FechaCreacion { get; set; }
    }
}