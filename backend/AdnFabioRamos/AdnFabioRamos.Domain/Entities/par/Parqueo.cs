using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estacionamiento_adn.Models
{
    [Table("Parqueo", Schema = "par")]
    public partial class Parqueo
    {
        [Key]
        public int Codigo { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = "";
        [Required]
        [StringLength(255)]
        public string Direccion { get; set; } = "";
        [Column(TypeName = "datetime")]
        public DateTime? FechaCreacion { get; set; }
    }
}
