
using Microsoft.Extensions.Configuration;
using estacionamiento_adn.Models.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AdnFabioRamos.Application.Utilidades;
using Microsoft.Extensions.DependencyInjection;
using AdnFabioRamos.Infrastructure.Persistence;

namespace AdnFabioRamos.Api.Tests
{
    [TestClass]
    public class ParqueoTest
    {
        readonly WebapiAppFactory<Startup> _AppFactory;
        protected HttpClient TestClient;
        public ParqueoTest()
        {
            _AppFactory = new WebapiAppFactory<Startup>();
            TestClient = _AppFactory.CreateClient();
            SeedDataBase(_AppFactory.Services);
        }

        void SeedDataBase(IServiceProvider provider)
        {
            using (var scope = provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<Adn_CeibaContext>();
                DBInicializer.Initialize(services);
            }
        }

        [TestMethod]
        [DataRow(1, "123")]
        [DataRow(2, "1234")]
        public void ValidarVehiculoEstaEnPicoPlaca(int tipo_vehiculo, string placa)
        {
            //Preparacion
            var respuestaPicoPlaca = new RespuestaPicoPlaca();

            //Prueba
            var c = this.TestClient.GetAsync($"api/dpp_detalle_pico_placa/consultarPicoPlaca/{tipo_vehiculo}/{placa}").Result;
            var response = c.Content.ReadAsStringAsync().Result;
            respuestaPicoPlaca = System.Text.Json.JsonSerializer.Deserialize<RespuestaPicoPlaca>(response);

            //Verificacion
            //Assert.AreEqual(true, respuestaPicoPlaca.permitir_salir_ahora);
            Assert.AreEqual(false, respuestaPicoPlaca.permitir_salir_ahora);
        }

        [TestMethod]
        [DataRow(1, "223")]
        [DataRow(2, "2234")]
        public void ValidarVehiculoNoEstaEnPicoPlaca(int tipo_vehiculo, string placa)
        {
            //Preparacion
            var respuestaPicoPlaca = new RespuestaPicoPlaca();

            //Prueba
            var c = this.TestClient.GetAsync($"api/dpp_detalle_pico_placa/consultarPicoPlaca/{tipo_vehiculo}/{placa}").Result;
            var response = c.Content.ReadAsStringAsync().Result;
            respuestaPicoPlaca = System.Text.Json.JsonSerializer.Deserialize<RespuestaPicoPlaca>(response);

            //Verificacion
            Assert.AreEqual(false, respuestaPicoPlaca.permitir_salir_ahora);
        }

        [TestMethod]
        [DataRow(1, "123", 2, 0, 1)]
        public void IngresarVehiculoParqueo(int movp_codpar, string movp_placa, int movp_codtipt, int movp_cilindraje, int movp_parqueo_numero)
        {
            //Preparacion
            var movp = new MovimientoVehiculoPostDTO()
            {
                movp_codpar = movp_codpar,
                movp_cilindraje = movp_cilindraje,
                movp_codtipt = movp_codtipt,
                movp_parqueo_numero = movp_parqueo_numero,
                movp_placa = movp_placa,
                fecha_ingreso = DateTime.Now.AddHours(-10)
            };

            //Prueba
            var stringContent = new StringContent(JsonConvert.SerializeObject(movp), Encoding.UTF8, "application/json");

            var c = this.TestClient.PostAsync($"api/movp_movimiento_parqueo/GuardarMovimientoVehiculo", stringContent).Result;
            var response = c.Content.ReadAsStringAsync().Result;
            var respuestaMovp = System.Text.Json.JsonSerializer.Deserialize<MovimientoVehiculoPostDTO>(response);

            //Verificacion
            Assert.AreEqual(true, c.IsSuccessStatusCode);
        }

        [TestMethod]
        [DataRow(1, 500, 4000, 650, -10, 6500)]//Moto cilindraje > 500
        [DataRow(1, 500, 4000, 500, -10, 4500)]//Moto cilindraje < 500
        [DataRow(2, 1000, 8000, 0, -12, 11000)]//Carro
        public void ValidarSalidaVehiculoParqueo(int tipt_codigo, int cap_valor_hora, int cap_valor_dia, int movp_cilindraje, int horas, int valor_esperado)
        {
            //Preparacion
            var movpPut = new MovimientoVehiculoPutDTO()
            {
                tipt_codigo = tipt_codigo,
                cap_valor_hora = cap_valor_hora,
                cap_valor_dia = cap_valor_dia,
                movp_cilindraje = movp_cilindraje, 
                movp_hora_entrada = DateTime.Now.AddHours(horas)
            };

            //Prueba
            ParqueoLogica.calcularTotalPagar(movpPut);

            //Verificacion
            Assert.AreEqual(valor_esperado, movpPut.cantidad_pagar);
        }
    }
}
