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
        readonly Adn_CeibaContext _context;
        public CapacidadRespository(Adn_CeibaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VehiculosDisponiblesParqueoDTO>> GetCapacidadxPorParqueo(int codigo_parqueo)
        {
            List<VehiculosDisponiblesParqueoDTO> lst_model = new List<VehiculosDisponiblesParqueoDTO>();
            VehiculosDisponiblesParqueoDTO model = new VehiculosDisponiblesParqueoDTO();
            var datos_select =
                from cap in _context.cap_capacidad
                join par in _context.par_parqueo on cap.cap_codpar equals par.par_codigo
                join tipt in _context.tipt_tipo_transporte on cap.cap_codtipt equals tipt.tipt_codigo
                where par.par_codigo == codigo_parqueo
                select new { cap, par, tipt };

            //if (datos_select.Count() == 0)
            //{
            //    return NotFound(new { respuesta = "No existe capacidad para el parqueo con id " + codigo_parqueo });
            //}

            foreach (var item in datos_select)
            {
                model.par_codigo = item.par.par_codigo;
                model.par_nombre = item.par.par_nombre;
                model.tipt_codigo = item.tipt.tipt_codigo;
                model.tipt_tipo = item.tipt.tipt_tipo;
                model.cap_capacidad = item.cap.cap_capacidad1;
                model.cap_valor_hora = item.cap.cap_valor_hora;
                model.cap_valor_dia = item.cap.cap_valor_dia;
                lst_model.Add(model);
                model = new VehiculosDisponiblesParqueoDTO();
            }

            return lst_model.ToList();
        }
    }
}
