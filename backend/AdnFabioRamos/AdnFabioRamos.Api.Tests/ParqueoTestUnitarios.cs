using AdnFabioRamos.Application.Utilidades;
using AdnFabioRamos.Infrastructure.Persistence;
using estacionamiento_adn.Models;
using estacionamiento_adn.Models.DTOs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace AdnFabioRamos.Api.Tests
{
    [TestClass]
    public class ParqueoTestUnitarios
    {
        [TestMethod]
        [DataRow(1, 500, 4000, 650, -10, 6500)]//Moto cilindraje > 500
        [DataRow(1, 500, 4000, 500, -10, 4500)]//Moto cilindraje < 500
        [DataRow(2, 1000, 8000, 0, -12, 11000)]//Carro
        public void ValidarSalidaVehiculoParqueo(int tipt_codigo, int cap_valor_hora, int cap_valor_dia, int movp_cilindraje, int horas, int valor_esperado)
        {
            //Preparacion
            var movpPut = new MovimientoVehiculoPutDto()
            {
                CodigoTipoTransporte = tipt_codigo,
                ValorHora = cap_valor_hora,
                ValorDia = cap_valor_dia,
                Cilindraje = movp_cilindraje,
                HoraEntrada = DateTime.Now.AddHours(horas)
            };

            //Prueba
            ParqueoLogica.CalcularTotalPagar(movpPut);

            //Verificacion
            Assert.AreEqual(valor_esperado, movpPut.CantidadPagar);
        }
    }
}
