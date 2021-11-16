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
                const int McodigoParqueo = 1;
                const int Manio = 2021;
                const int McodigoPicoPlaca = 1;
                const int McodigoMoto = 1;
                const int McodigoCarro = 2;
                const int MesActual = 11;
                const int MdiaSemanaActual = 1;
                const string MhoraInicio = "00:00";
                const string MhoraFin = "23:59";

                const short CapacidadMoto = 10;
                const short CapacidadCarro = 20;

                const int ValorHoraMoto = 500;
                const int ValorHoraCarro = 1000;

                const int ValorDiaMoto = 4000;
                const int ValorDiaCarro = 8000;

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
                    new PicoPlaca { Anio = Manio, Descripcion = "Pico placa para el año 2021 en Colombia MEMORIA" }
                 );

                _context.SaveChanges();

                // Detalle PicoPlaca
                if (_context.DetallePicoPlaca.Any())
                {
                    return;
                }

                _context.DetallePicoPlaca.AddRange(
                    new DetallePicoPlaca
                    {
                        CodigoPicoPlaca = McodigoPicoPlaca,
                        CodigoTipoTransporte = McodigoMoto,
                        Mes = Convert.ToByte(MesActual),
                        HoraInicio = MhoraInicio,
                        HoraFin = MhoraFin,
                        Digito = "1",
                        DiaSemana = MdiaSemanaActual
                    },
                    new DetallePicoPlaca
                    {
                        CodigoPicoPlaca = McodigoPicoPlaca,
                        CodigoTipoTransporte = McodigoMoto,
                        Mes = Convert.ToByte(MesActual),
                        HoraInicio = MhoraInicio,
                        HoraFin = MhoraFin,
                        Digito = "2",
                        DiaSemana = MdiaSemanaActual
                    },

                    new DetallePicoPlaca
                    {
                        CodigoPicoPlaca = McodigoPicoPlaca,
                        CodigoTipoTransporte = McodigoCarro,
                        Mes = Convert.ToByte(MesActual),
                        HoraInicio = MhoraInicio,
                        HoraFin = MhoraFin,
                        Digito = "1",
                        DiaSemana = MdiaSemanaActual
                    },
                    new DetallePicoPlaca
                    {
                        CodigoPicoPlaca = McodigoPicoPlaca,
                        CodigoTipoTransporte = McodigoCarro,
                        Mes = Convert.ToByte(MesActual),
                        HoraInicio = MhoraInicio,
                        HoraFin = MhoraFin,
                        Digito = "2",
                        DiaSemana = MdiaSemanaActual
                    }
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
                    new Capacidad
                    {
                        CodigoParqueo = McodigoParqueo,
                        CodigoTipoTransporte = McodigoMoto,
                        Capacidad1 = CapacidadMoto,
                        ValorHora = ValorHoraMoto,
                        ValorDia = ValorDiaMoto
                    },
                    new Capacidad
                    {
                        CodigoParqueo = McodigoParqueo,
                        CodigoTipoTransporte = McodigoCarro,
                        Capacidad1 = CapacidadCarro,
                        ValorHora = ValorHoraCarro,
                        ValorDia = ValorDiaCarro
                    }
                 );

                _context.SaveChanges();


            }
        }
    }
}
