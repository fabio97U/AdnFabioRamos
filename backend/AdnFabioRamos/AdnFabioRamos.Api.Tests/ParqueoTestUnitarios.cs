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
        public void ValidarSalidaVehiculoParqueo(int CodigoTipoTransporte, int ValorHora, int ValorDia, int Cilindraje, int Horas, int ValorEsperado)
        {
            //Preparacion
            var movpPut = new MovimientoVehiculoPutDto()
            {
                CodigoTipoTransporte = CodigoTipoTransporte,
                ValorHora = ValorHora,
                ValorDia = ValorDia,
                Cilindraje = Cilindraje,
                HoraEntrada = DateTime.Now.AddHours(Horas)
            };

            //Prueba
            ParqueoLogica.CalcularTotalPagar(movpPut);

            //Verificacion
            Assert.AreEqual(ValorEsperado, movpPut.CantidadPagar);
        }
    }
}
