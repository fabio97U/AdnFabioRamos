using estacionamiento_adn.Models.DTOs;
using System;

namespace AdnFabioRamos.Application.Utilidades
{
    public static class ParqueoLogica
    {
        public static MovimientoVehiculoPutDto CalcularTotalPagar(MovimientoVehiculoPutDto movp)
        {
            const int TotalHorasDia = 9;
            const int CilindrajeMaximo = 500;
            const int MontoExtraCilindrajeMaximo = 2000;
            if (movp == null)
            {
                return new MovimientoVehiculoPutDto();
            }

            var hora_entrada = movp.HoraEntrada;
            movp.HoraSalida = DateTime.Now;

            TimeSpan result = movp.HoraSalida.Subtract(hora_entrada);

            movp.CantidadHoras = result.TotalHours;

            var cantidad_dias_9h = (movp.CantidadHoras / TotalHorasDia);

            movp.Dias9H = Math.Truncate(cantidad_dias_9h);//1
            movp.HorasRestantes = Math.Truncate(movp.CantidadHoras - (Convert.ToDouble(movp.Dias9H) * TotalHorasDia));//3

            movp.TotalPagarDias9H = movp.Dias9H * movp.ValorDia;
            movp.TotalPagarHorasRestantes = movp.HorasRestantes * movp.ValorHora;

            movp.CantidadPagar = movp.TotalPagarDias9H + movp.TotalPagarHorasRestantes;


            if (movp.CodigoTipoTransporte == 1 && movp.Cilindraje > CilindrajeMaximo && movp.CantidadPagar > 0)//Es moto
            {
                movp.CantidadPagar = movp.CantidadPagar + MontoExtraCilindrajeMaximo;
            }

            return movp;
        }
    }
}
