using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estacionamiento_adn.Models
{
    [Table("MovimientoParqueo", Schema = "par")]
    public class MovimientoParqueo
    {
        [Key]
        public Guid Codigo { get; set; }
        public int CodigoParqueo { get; set; }
        [Required]
        [StringLength(50)]
        public string Placa { get; set; } = "";
        public int CodigoTipoTransporte { get; set; }
        /// <summary>
        /// Las motos con un cilindraje mayor a 500cc tienen un sobrecargo de $2000
        /// </summary>
        public int? Cilindraje { get; set; }
        public short ParqueoNumero { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HoraEntrada { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HoraSalida { get; set; }
        public double? TotalPagar { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FechaCreacion { get; set; }
    }
}
