using AdnFabioRamos.Domain.Ports;
using AdnFabioRamos.Infrastructure.Persistence;
using estacionamiento_adn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdnFabioRamos.Infrastructure.Adapters
{
    public class CapacidadRespository: ICapacidadRepository
    {
        readonly AdnCeibaContext _context;
        public CapacidadRespository(AdnCeibaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VehiculosDisponiblesParqueoDto>> GetCapacidadxPorParqueo(int codigo_parqueo)
        {
            List<VehiculosDisponiblesParqueoDto> lst_model = new List<VehiculosDisponiblesParqueoDto>();
            VehiculosDisponiblesParqueoDto model = new VehiculosDisponiblesParqueoDto();
            var datos_select =
                from cap in _context.Capacidad
                join par in _context.Parqueo on cap.CodigoParqueo equals par.Codigo
                join tipt in _context.TipoTransporte on cap.CodigoTipoTransporte equals tipt.Codigo
                where par.Codigo == codigo_parqueo
                select new { cap, par, tipt };

            //if (datos_select.Count() == 0)W
            //{
            //    return NotFound(new { respuesta = "No existe capacidad para el parqueo con id " + codigo_parqueo });
            //}

            foreach (var item in datos_select)
            {
                model.CodigoParqueo = item.par.Codigo;
                model.ParqueoNombre = item.par.Nombre;
                model.CodigoTipoTransporte = item.tipt.Codigo;
                model.TipoTransporte = item.tipt.Tipo;
                model.Capacidad = item.cap.Capacidad1;
                model.ValorHora = item.cap.ValorHora;
                model.ValorDia = item.cap.ValorDia;
                lst_model.Add(model);
                model = new VehiculosDisponiblesParqueoDto();
            }

            return lst_model.ToList();
        }
    }
}
