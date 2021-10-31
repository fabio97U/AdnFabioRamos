using estacionamiento_adn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdnFabioRamos.Application.Utilidades
{
    public static class ParqueoLogica
    {
        public static MovimientoVehiculoPutDTO calcularTotalPagar(MovimientoVehiculoPutDTO movp)
        {
            var hora_entrada = movp.movp_hora_entrada;
            movp.hora_salida = DateTime.Now;

            TimeSpan result = movp.hora_salida.Subtract(hora_entrada);

            movp.cantidad_horas = result.TotalHours;

            var cantidad_dias_9h = (movp.cantidad_horas / 9);

            movp.dias_9h = Math.Truncate(cantidad_dias_9h);//1
            movp.horas_restantes = Math.Truncate(movp.cantidad_horas - (Convert.ToDouble(movp.dias_9h) * 9));//3

            movp.total_pagar_dias_9h = movp.dias_9h * movp.cap_valor_dia;
            movp.total_pagar_horas_restantes = movp.horas_restantes * movp.cap_valor_hora;

            movp.cantidad_pagar = movp.total_pagar_dias_9h + movp.total_pagar_horas_restantes;


            if (movp.tipt_codigo == 1 && movp.movp_cilindraje > 500 && movp.cantidad_pagar > 0)//Es moto
                movp.cantidad_pagar = movp.cantidad_pagar + 2000;

            return movp;
        }
    }
}
