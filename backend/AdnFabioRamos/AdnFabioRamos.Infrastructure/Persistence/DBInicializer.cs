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
                _context.TipoTransporte.AddRange(
                    new TipoTransporte { Tipo = "Moto", Descripcion = "Toda tipo de moto MEMORIA" },
                    new TipoTransporte { Tipo = "Carro", Descripcion = "Toda tipo de carro MEMORIA" }
                 );

                _context.SaveChanges();

                _context.PicoPlaca.AddRange(
                    new PicoPlaca { Anio = ValoresDBInicializer.Manio, Descripcion = "Pico placa para el año 2021 en Colombia MEMORIA" }
                 );

                _context.SaveChanges();

                _context.DetallePicoPlaca.AddRange(
                    new DetallePicoPlaca
                    {
                        CodigoPicoPlaca = ValoresDBInicializer.McodigoPicoPlaca,
                        CodigoTipoTransporte = ValoresDBInicializer.McodigoMoto,
                        Mes = Convert.ToByte(ValoresDBInicializer.MesActual),
                        HoraInicio = ValoresDBInicializer.MhoraInicio,
                        HoraFin = ValoresDBInicializer.MhoraFin,
                        Digito = "1",
                        DiaSemana = ValoresDBInicializer.MdiaSemanaActual
                    },

                    new DetallePicoPlaca
                    {
                        CodigoPicoPlaca = ValoresDBInicializer.McodigoPicoPlaca,
                        CodigoTipoTransporte = ValoresDBInicializer.McodigoCarro,
                        Mes = Convert.ToByte(ValoresDBInicializer.MesActual),
                        HoraInicio = ValoresDBInicializer.MhoraInicio,
                        HoraFin = ValoresDBInicializer.MhoraFin,
                        Digito = "1",
                        DiaSemana = ValoresDBInicializer.MdiaSemanaActual
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
                        CodigoParqueo = ValoresDBInicializer.McodigoParqueo,
                        CodigoTipoTransporte = ValoresDBInicializer.McodigoMoto,
                        Capacidad1 = ValoresDBInicializer.CapacidadMoto,
                        ValorHora = ValoresDBInicializer.ValorHoraMoto,
                        ValorDia = ValoresDBInicializer.ValorDiaMoto
                    },
                    new Capacidad
                    {
                        CodigoParqueo = ValoresDBInicializer.McodigoParqueo,
                        CodigoTipoTransporte = ValoresDBInicializer.McodigoCarro,
                        Capacidad1 = ValoresDBInicializer.CapacidadCarro,
                        ValorHora = ValoresDBInicializer.ValorHoraCarro,
                        ValorDia = ValoresDBInicializer.ValorDiaCarro
                    }
                 );

                _context.SaveChanges();
            }
        }
    }
}
