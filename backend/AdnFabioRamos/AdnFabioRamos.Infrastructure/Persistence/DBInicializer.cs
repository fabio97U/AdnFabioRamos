using estacionamiento_adn.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AdnFabioRamos.Infrastructure.Persistence
{
    public static class DBInicializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var _context = new AdnCeibaContext(serviceProvider.GetRequiredService<DbContextOptions<AdnCeibaContext>>()))
            {
                ValoresDBInicializer MvaloresDbInicializer = new ValoresDBInicializer();

                _context.TipoTransporte.AddRange(
                    new TipoTransporte { Tipo = "Moto", Descripcion = "Toda tipo de moto MEMORIA" },
                    new TipoTransporte { Tipo = "Carro", Descripcion = "Toda tipo de carro MEMORIA" }
                 );

                _context.SaveChanges();

                _context.PicoPlaca.AddRange(
                    new PicoPlaca { Anio = MvaloresDbInicializer.Manio, Descripcion = "Pico placa para el año 2021 en Colombia MEMORIA" }
                 );

                _context.SaveChanges();

                _context.DetallePicoPlaca.AddRange(
                    new DetallePicoPlaca
                    {
                        CodigoPicoPlaca = MvaloresDbInicializer.McodigoPicoPlaca,
                        CodigoTipoTransporte = MvaloresDbInicializer.McodigoMoto,
                        Mes = Convert.ToByte(MvaloresDbInicializer.MesActual),
                        HoraInicio = MvaloresDbInicializer.MhoraInicio,
                        HoraFin = MvaloresDbInicializer.MhoraFin,
                        Digito = "1",
                        DiaSemana = MvaloresDbInicializer.MdiaSemanaActual,
                        DigitoInicioFinal = MvaloresDbInicializer.MDigitoInicioFinal
                    },

                    new DetallePicoPlaca
                    {
                        CodigoPicoPlaca = MvaloresDbInicializer.McodigoPicoPlaca,
                        CodigoTipoTransporte = MvaloresDbInicializer.McodigoCarro,
                        Mes = Convert.ToByte(MvaloresDbInicializer.MesActual),
                        HoraInicio = MvaloresDbInicializer.MhoraInicio,
                        HoraFin = MvaloresDbInicializer.MhoraFin,
                        Digito = "1",
                        DiaSemana = MvaloresDbInicializer.MdiaSemanaActual,
                        DigitoInicioFinal = MvaloresDbInicializer.MDigitoInicioFinal
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
                        CodigoParqueo = MvaloresDbInicializer.McodigoParqueo,
                        CodigoTipoTransporte = MvaloresDbInicializer.McodigoMoto,
                        Capacidad1 = MvaloresDbInicializer.CapacidadMoto,
                        ValorHora = MvaloresDbInicializer.ValorHoraMoto,
                        ValorDia = MvaloresDbInicializer.ValorDiaMoto
                    },
                    new Capacidad
                    {
                        CodigoParqueo = MvaloresDbInicializer.McodigoParqueo,
                        CodigoTipoTransporte = MvaloresDbInicializer.McodigoCarro,
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
