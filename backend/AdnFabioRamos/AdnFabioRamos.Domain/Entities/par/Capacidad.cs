using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estacionamiento_adn.Models
{
    [Table("Capacidad", Schema = "par")]
    public partial class Capacidad
    {
        [Key]
        public int Codigo { get; set; }
        public int CodigoParqueo { get; set; }
        public int CodigoTipoTransporte { get; set; }
        [Column("Capacidad")]
        public short Capacidad1 { get; set; }
        public double ValorHora { get; set; }
        public double ValorDia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FechaCreacion { get; set; }
    }
}
