using estacionamiento_adn.Models.DTOs;
using System;

namespace AdnFabioRamos.Application.Utilidades
{
    public static class ParqueoLogica
    {
        public static MovimientoVehiculoPutDTO CalcularTotalPagar(MovimientoVehiculoPutDTO movp)
        {
            const int TotalHorasDia = 9;
            const int CilindrajeMaximo = 500;
            const int MontoExtraCilindrajeMaximo = 2000;
            if (movp == null)
            {
                return new MovimientoVehiculoPutDTO();
            }

            var hora_entrada = movp.HoraEntrada;
            movp.HoraSalida = DateTime.Now;

            TimeSpan result = movp.HoraSalida.Subtract(hora_entrada);

            movp.CantidadHoras = result.TotalHours;

            var cantidad_dias_9h = (movp.CantidadHoras / TotalHorasDia);

            movp.Dias9h = Math.Truncate(cantidad_dias_9h);//1
            movp.HorasRestantes = Math.Truncate(movp.CantidadHoras - (Convert.ToDouble(movp.Dias9h) * TotalHorasDia));//3

            movp.TotalPagarDias9h = movp.Dias9h * movp.ValorDia;
            movp.TotalPagarHorasRestantes = movp.HorasRestantes * movp.ValorHora;

            movp.CantidadPagar = movp.TotalPagarDias9h + movp.TotalPagarHorasRestantes;


            if (movp.CodigoTipoTransporte == 1 && movp.Cilindraje > CilindrajeMaximo && movp.CantidadPagar > 0)//Es moto
            {
                movp.CantidadPagar = movp.CantidadPagar + MontoExtraCilindrajeMaximo;
            }

            return movp;
        }
    }
}
