using estacionamiento_adn.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdnFabioRamos.Infrastructure.Persistence
{
    public static class DBInicializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var _context = new Adn_CeibaContext(serviceProvider.GetRequiredService<DbContextOptions<Adn_CeibaContext>>()))
            {
                // Tipos de transporte
                if (_context.tipt_tipo_transporte.Any())
                {
                    return;
                }

                _context.tipt_tipo_transporte.AddRange(
                    new tipt_tipo_transporte { tipt_tipo = "Moto", tipt_descripcion = "Toda tipo de moto" },
                    new tipt_tipo_transporte { tipt_tipo = "Carro", tipt_descripcion = "Toda tipo de carro" }
                 );

                _context.SaveChanges();

                // Encabezado PicoPlaca
                if (_context.pp_pico_placa.Any())
                {
                    return;
                }

                _context.pp_pico_placa.AddRange(
                    new pp_pico_placa { pp_anio = 2021, pp_descripcion = "Pico placa para el año 2021 en Colombia" }
                 );

                _context.SaveChanges();

                // Detalle PicoPlaca
                if (_context.dpp_detalle_pico_placa.Any())
                {
                    return;
                }

                _context.dpp_detalle_pico_placa.AddRange(
                    new dpp_detalle_pico_placa { dpp_codpp = 2021, dpp_codtipt = 1, dpp_mes = 11, dpp_hora_inicio = "00:00", dpp_hora_fin = "23:59", dpp_digito = 1, dpp_dia_semana = 2 },
                    new dpp_detalle_pico_placa { dpp_codpp = 2021, dpp_codtipt = 1, dpp_mes = 11, dpp_hora_inicio = "00:00", dpp_hora_fin = "23:59", dpp_digito = 2, dpp_dia_semana = 3 },

                    new dpp_detalle_pico_placa { dpp_codpp = 2021, dpp_codtipt = 2, dpp_mes = 11, dpp_hora_inicio = "00:00", dpp_hora_fin = "23:59", dpp_digito = 1, dpp_dia_semana = 2 },
                    new dpp_detalle_pico_placa { dpp_codpp = 2021, dpp_codtipt = 2, dpp_mes = 11, dpp_hora_inicio = "00:00", dpp_hora_fin = "23:59", dpp_digito = 2, dpp_dia_semana = 3 }
                 );

                _context.SaveChanges();

                // Parqueaderos
                if (_context.par_parqueo.Any())
                {
                    return;
                }

                _context.par_parqueo.AddRange(
                    new par_parqueo { par_nombre = "Parqueo Ceiba", par_direccion = "Cl. 8B ##65-191, Medellín, Antioquia, Colombia" }
                 );

                _context.SaveChanges();

                // Capacidad de parqueaderos
                if (_context.cap_capacidad.Any())
                {
                    return;
                }

                _context.cap_capacidad.AddRange(
                    new cap_capacidad { cap_codpar = 1, cap_codtipt = 1, cap_capacidad1 = 10, cap_valor_hora = 500, cap_valor_dia = 4000 },
                    new cap_capacidad { cap_codpar = 1, cap_codtipt = 2, cap_capacidad1 = 20, cap_valor_hora = 1000, cap_valor_dia = 8000 }
                 );

                _context.SaveChanges();


            }
        }
    }
}
