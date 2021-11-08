using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estacionamiento_adn.Models
{
    [Table("TipoTransporte", Schema = "par")]
    public class TipoTransporte
    {
        [Key]
        public int Codigo { get; set; }
        [Required]
        public string Tipo { get; set; } = "";
        [Required]
        public string Descripcion { get; set; } = "";
        [Column(TypeName = "datetime")]
        public DateTime? FechaCreacion { get; set; }
    }
}
