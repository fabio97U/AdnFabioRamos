using estacionamiento_adn.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AdnFabioRamos.Infrastructure.Persistence
{
    public static class DBInicializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var _context = new AdnCeibaContext(serviceProvider.GetRequiredService<DbContextOptions<AdnCeibaContext>>()))
            {
                ValoresDBInicializer MvaloresDbInicializer = new ValoresDBInicializer();

                if (_context.TipoTransporte.Any())
                {
                    return;
                }

                _context.TipoTransporte.AddRange(
                    new TipoTransporte { Tipo = "Moto", Descripcion = "Toda tipo de moto MEMORIA" },
                    new TipoTransporte { Tipo = "Carro", Descripcion = "Toda tipo de carro MEMORIA" }
                 );

                _context.SaveChanges();

                _context.PicoPlaca.AddRange(
                    new PicoPlaca { Anio = MvaloresDbInicializer.Anio, Descripcion = "Pico placa para el año 2021 en Colombia MEMORIA", FechaCreacion = DateTime.Now }
                 );

                _context.SaveChanges();

                _context.DetallePicoPlaca.AddRange(
                    new DetallePicoPlaca
                    {
                        CodigoPicoPlaca = MvaloresDbInicializer.CodigoPicoPlaca,
                        CodigoTipoTransporte = MvaloresDbInicializer.CodigoMoto,
                        Mes = Convert.ToByte(MvaloresDbInicializer.MesActual),
                        HoraInicio = MvaloresDbInicializer.HoraInicio,
                        HoraFin = MvaloresDbInicializer.HoraFin,
                        Digito = "1",
                        DiaSemana = MvaloresDbInicializer.DiaSemanaActual,
                        DigitoInicioFinal = MvaloresDbInicializer.DigitoInicioFinal
                    },

                    new DetallePicoPlaca
                    {
                        CodigoPicoPlaca = MvaloresDbInicializer.CodigoPicoPlaca,
                        CodigoTipoTransporte = MvaloresDbInicializer.CodigoCarro,
                        Mes = Convert.ToByte(MvaloresDbInicializer.MesActual),
                        HoraInicio = MvaloresDbInicializer.HoraInicio,
                        HoraFin = MvaloresDbInicializer.HoraFin,
                        Digito = "1",
                        DiaSemana = MvaloresDbInicializer.DiaSemanaActual,
                        DigitoInicioFinal = MvaloresDbInicializer.DigitoInicioFinal
                    }
                 );

                _context.SaveChanges();

                _context.Parqueo.AddRange(
                    new Parqueo { Nombre = "Parqueo Ceiba MEMORIA", Direccion = "Cl. 8B ##65-191, Medellín, Antioquia, Colombia" }
                 );

                _context.SaveChanges();

                _context.Capacidad.AddRange(
                    new Capacidad
                    {
                        CodigoParqueo = MvaloresDbInicializer.CodigoParqueo,
                        CodigoTipoTransporte = MvaloresDbInicializer.CodigoMoto,
                        Capacidad1 = MvaloresDbInicializer.CapacidadMoto,
                        ValorHora = MvaloresDbInicializer.ValorHoraMoto,
                        ValorDia = MvaloresDbInicializer.ValorDiaMoto
                    },
                    new Capacidad
                    {
                        CodigoParqueo = MvaloresDbInicializer.CodigoParqueo,
                        CodigoTipoTransporte = MvaloresDbInicializer.CodigoCarro,
                        Capacidad1 = MvaloresDbInicializer.CapacidadCarro,
                        ValorHora = MvaloresDbInicializer.ValorHoraCarro,
                        ValorDia = MvaloresDbInicializer.ValorDiaCarro
                    }
                 );

                _context.SaveChanges();
            }
        }
    }
}
