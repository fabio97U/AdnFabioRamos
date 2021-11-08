﻿using System;

namespace estacionamiento_adn.Models.DTOs
{
    public class MovimientoVehiculoPostDTO
    {
        public int CodigoParqueo { get; set; }
        public string Placa { get; set; } = "";
        public int CodigoTipoTransporte { get; set; }
        public int Cilindraje { get; set; }
        public int ParqueoNumero { get; set; }
        public DateTime FechaIngreso { get; set;  } = DateTime.Now;
    }
}
