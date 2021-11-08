using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estacionamiento_adn.Models
{
    [Table("Parqueo", Schema = "par")]
    public class Parqueo
    {
        [Key]
        public int Codigo { get; set; }
        [Required]
        public string Nombre { get; set; } = "";
        [Required]
        public string Direccion { get; set; } = "";
        [Column(TypeName = "datetime")]
        public DateTime? FechaCreacion { get; set; }
    }
}
