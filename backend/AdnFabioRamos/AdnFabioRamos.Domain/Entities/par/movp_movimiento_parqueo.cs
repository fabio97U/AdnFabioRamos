﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estacionamiento_adn.Models
{
    [Table("movp_movimiento_parqueo", Schema = "par")]
    public partial class movp_movimiento_parqueo
    {
        [Key]
        public Guid movp_codigo { get; set; }
        public int movp_codpar { get; set; }
        [Required]
        [StringLength(50)]
        public string movp_placa { get; set; }
        public int movp_codtipt { get; set; }
        /// <summary>
        /// Las motos con un cilindraje mayor a 500cc tienen un sobrecargo de $2000
        /// </summary>
        public int? movp_cilindraje { get; set; }
        public short movp_parqueo_numero { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? movp_hora_entrada { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? movp_hora_salida { get; set; }
        public double? movp_total_pagar { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? movp_fecha_creacion { get; set; }
    }
}