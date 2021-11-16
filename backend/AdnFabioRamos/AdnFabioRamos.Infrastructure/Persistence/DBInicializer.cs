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
            using (var _context = new AdnCeibaContext(serviceProvider.GetRequiredService<DbContextOptions<AdnCeibaContext>>()))
            {
                // Tipos de transporte
                if (_context.TipoTransporte.Any())
                {
                    return;
                }

                _context.TipoTransporte.AddRange(
                    new TipoTransporte { Tipo = "Moto", Descripcion = "Toda tipo de moto MEMORIA" },
                    new TipoTransporte { Tipo = "Carro", Descripcion = "Toda tipo de carro MEMORIA" }
                 );

                _context.SaveChanges();

                // Encabezado PicoPlaca
                if (_context.PicoPlaca.Any())
                {
                    return;
                }

                _context.PicoPlaca.AddRange(
                    new PicoPlaca { Anio = 2021, Descripcion = "Pico placa para el año 2021 en Colombia MEMORIA" }
                 );

                _context.SaveChanges();

                // Detalle PicoPlaca
                if (_context.DetallePicoPlaca.Any())
                {
                    return;
                }

                _context.DetallePicoPlaca.AddRange(
                    new DetallePicoPlaca { CodigoPicoPlaca = 1, CodigoTipoTransporte = 1, Mes = 11, HoraInicio = "00:00", HoraFin = "23:59", Digito = "1", DiaSemana = 1 },
                    new DetallePicoPlaca { CodigoPicoPlaca = 1, CodigoTipoTransporte = 1, Mes = 11, HoraInicio = "00:00", HoraFin = "23:59", Digito = "2", DiaSemana = 1 },

                    new DetallePicoPlaca { CodigoPicoPlaca = 1, CodigoTipoTransporte = 2, Mes = 11, HoraInicio = "00:00", HoraFin = "23:59", Digito = "1", DiaSemana = 1 },
                    new DetallePicoPlaca { CodigoPicoPlaca = 1, CodigoTipoTransporte = 2, Mes = 11, HoraInicio = "00:00", HoraFin = "23:59", Digito = "2", DiaSemana = 1 }
                 );

                _context.SaveChanges();

                // Parqueaderos
                if (_context.Parqueo.Any())
                {
                    return;
                }

                _context.Parqueo.AddRange(
                    new Parqueo { Nombre = "Parqueo Ceiba MEMORIA", Direccion = "Cl. 8B ##65-191, Medellín, Antioquia, Colombia" }
                 );

                _context.SaveChanges();

                // Capacidad de parqueaderos
                if (_context.Capacidad.Any())
                {
                    return;
                }

                _context.Capacidad.AddRange(
                    new Capacidad { CodigoParqueo = 1, CodigoTipoTransporte = 1, Capacidad1 = 20, ValorHora = 500, ValorDia = 4000 },
                    new Capacidad { CodigoParqueo = 1, CodigoTipoTransporte = 2, Capacidad1 = 20, ValorHora = 1000, ValorDia = 8000 }
                 );

                _context.SaveChanges();


            }
        }
    }
}
