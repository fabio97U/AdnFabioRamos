using AdnFabioRamos.Domain.Ports;
using AdnFabioRamos.Infrastructure.Persistence;
using estacionamiento_adn.Models;
using estacionamiento_adn.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AdnFabioRamos.Infrastructure.Adapters
{
    public class DetallePicoPlacaRepository : IDetallePicoPlaca
    {
        readonly AdnCeibaContext _context;
        public DetallePicoPlacaRepository(AdnCeibaContext context)
        {
            _context = context;
        }
        public enum DiasSemana { LUNES = 1, MARTES = 2, MIERCOLES = 3, JUEVES = 4, VIERNES = 5, SABADO = 6, DOMINGO = 0 }

        public static string DiaSemana(int numero)
        {
            string dia = "";
            switch (numero)
            {
                case (int)DiasSemana.LUNES:
                    dia = "lunes";
                    break;
                case (int)DiasSemana.MARTES:
                    dia = "martes";
                    break;
                case (int)DiasSemana.MIERCOLES:
                    dia = "miercoles";
                    break;
                case (int)DiasSemana.JUEVES:
                    dia = "jueves";
                    break;
                case (int)DiasSemana.VIERNES:
                    dia = "viernes";
                    break;
                case (int)DiasSemana.SABADO:
                    dia = "sabado";
                    break;
                case (int)DiasSemana.DOMINGO:
                    dia = "domingo";
                    break;
                default:
                    dia = "";
                    break;

            }
            return dia;
        }

        public bool FiltrarEncabezadoPicoPlaca(DetallePicoPlaca dpp, int codigo_picoplaca, DateTime fecha_actual, int tipo_vehiculo)
        {
            return dpp.CodigoPicoPlaca == codigo_picoplaca && dpp.Mes == fecha_actual.Month
                && dpp.CodigoTipoTransporte == tipo_vehiculo;
        }
        public bool FiltrarFechasBetween(DetallePicoPlaca dpp, DateTime fecha_actual)
        {
            return fecha_actual >= DateTime.ParseExact(dpp.HoraInicio, "HH:mm", CultureInfo.InvariantCulture)
                && fecha_actual <= DateTime.ParseExact(dpp.HoraFin, "HH:mm", CultureInfo.InvariantCulture);
        }
        public async Task<RespuestaPicoPlaca> GetconsultarPicoPlaca(int tipo_vehiculo, string placa)
        {
            List<SpValidarPicoPlacaResult> lst_dpp_detalle_pico_placa = new List<SpValidarPicoPlacaResult>();
            var model = new SpValidarPicoPlacaResult();

            const int codigo_picoplaca = 1;
            var fecha_actual = DateTime.Now;
            var dia_semana_actual = (int)fecha_actual.DayOfWeek;

            var datos_select =
                (
                from dpp in await _context.DetallePicoPlaca.ToListAsync()
                where
                FiltrarEncabezadoPicoPlaca(dpp, codigo_picoplaca, fecha_actual, tipo_vehiculo)
                && FiltrarFechasBetween(dpp, fecha_actual)

                && string.Compare(dpp.Digito, (dpp.DigitoInicioFinal == "I" ? placa.Substring(0, 1) : placa.Substring(placa.Length - 1, 1)), false, CultureInfo.InvariantCulture) == 0
                && string.Compare(dpp.DiaSemana.ToString(), dia_semana_actual.ToString(), false, CultureInfo.InvariantCulture) == 0

                select new
                {
                    Codigo = dpp.Codigo,
                    CodigoPicoPlaca = dpp.CodigoPicoPlaca,
                    CodigoTipoTransporte = dpp.CodigoTipoTransporte,
                    Mes = dpp.Mes,
                    HoraInicio = dpp.HoraInicio,
                    HoraFin = dpp.HoraFin,
                    DiaSemana = dpp.DiaSemana,
                    DiaNombre = DiaSemana(dpp.DiaSemana),
                    Digito = dpp.Digito,
                    DigitoInicioFinal = dpp.DigitoInicioFinal,
                    Salida = "Puede salir el vehiculo este dia y hora",
                    Tipo = 0
                })

                .Concat(
                    from dpp in await _context.DetallePicoPlaca.ToListAsync()
                    where
                    FiltrarEncabezadoPicoPlaca(dpp, codigo_picoplaca, fecha_actual, tipo_vehiculo)
                    && string.Compare(dpp.Digito, (dpp.DigitoInicioFinal == "I" ? placa.Substring(0, 1) : placa.Substring(placa.Length - 1, 1)), false, CultureInfo.InvariantCulture) == 0
                    select new
                    {
                        Codigo = dpp.Codigo,
                        CodigoPicoPlaca = dpp.CodigoPicoPlaca,
                        CodigoTipoTransporte = dpp.CodigoTipoTransporte,
                        Mes = dpp.Mes,
                        HoraInicio = dpp.HoraInicio,
                        HoraFin = dpp.HoraFin,
                        DiaSemana = dpp.DiaSemana,
                        DiaNombre = DiaSemana(dpp.DiaSemana),
                        Digito = dpp.Digito,
                        DigitoInicioFinal = dpp.DigitoInicioFinal,
                        Salida = "Dias y horas que puede salir el vehiculo",
                        Tipo = 1
                    }
                    );

            datos_select.ToList().ForEach(item =>
            {
                model.Codigo = item.Codigo;
                model.CodigoPicoPlaca = item.CodigoPicoPlaca;
                model.CodigoTipoTransporte = item.CodigoTipoTransporte;
                model.Mes = item.Mes;
                model.HoraInicio = item.HoraInicio;
                model.HoraFin = item.HoraFin;
                model.DiaSemana = item.DiaSemana;
                model.DiaNombre = item.DiaNombre;
                model.Digito = item.Digito;
                model.DigitoInicioFinal = item.DigitoInicioFinal;
                model.Salida = item.Salida;
                model.Tipo = item.Tipo;
                lst_dpp_detalle_pico_placa.Add(model);
                model = new SpValidarPicoPlacaResult();
            });

            var respuesta = new RespuestaPicoPlaca();

            respuesta.PermitirSalirAhora = (lst_dpp_detalle_pico_placa.Any(x => x.Tipo == 0));

            lst_dpp_detalle_pico_placa.Where(x => x.Tipo == 1).ToList().ForEach(x =>
                respuesta.DiasPermitidosSalir += x.DiaNombre + " de " + x.HoraInicio + " a " + x.HoraFin + " "
            );

            return respuesta;
        }

        public async Task<DetallePicoPlaca> PostDetallePicoPlaca(DetallePicoPlaca dpp_Detalle_Pico_Placa)
        {
            _context.DetallePicoPlaca.Add(dpp_Detalle_Pico_Placa);

            await _context.SaveChangesAsync();

            return dpp_Detalle_Pico_Placa;
        }
    }
}
