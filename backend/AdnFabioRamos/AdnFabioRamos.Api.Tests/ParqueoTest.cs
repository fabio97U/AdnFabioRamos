
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
using estacionamiento_adn.Models;

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
                var context = services.GetRequiredService<AdnCeibaContext>();
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
            var c = this.TestClient.GetAsync($"api/PicoPlaca/consultarPicoPlaca/{tipo_vehiculo}/{placa}").Result;
            var response = c.Content.ReadAsStringAsync().Result;
            respuestaPicoPlaca = System.Text.Json.JsonSerializer.Deserialize<RespuestaPicoPlaca>(response);

            //Verificacion
            Assert.AreEqual(true, respuestaPicoPlaca.PermitirSalirAhora);
            //Assert.AreEqual(false, respuestaPicoPlaca.PermitirSalirAhora);
        }

        [TestMethod]
        [DataRow(1, "223")]
        [DataRow(2, "2234")]
        public void ValidarVehiculoNoEstaEnPicoPlaca(int tipo_vehiculo, string placa)
        {
            //Preparacion
            var respuestaPicoPlaca = new RespuestaPicoPlaca();

            //Prueba
            var c = this.TestClient.GetAsync($"api/PicoPlaca/consultarPicoPlaca/{tipo_vehiculo}/{placa}").Result;
            var response = c.Content.ReadAsStringAsync().Result;
            respuestaPicoPlaca = System.Text.Json.JsonSerializer.Deserialize<RespuestaPicoPlaca>(response);

            //Verificacion
            Assert.AreEqual(false, respuestaPicoPlaca.PermitirSalirAhora);
        }

        [TestMethod]
        [DataRow(1, "123", 2, 0, 1)]
        public void IngresarVehiculoParqueo(int movp_codpar, string movp_placa, int movp_codtipt, int movp_cilindraje, int movp_parqueo_numero)
        {
            //Preparacion
            var movp = new MovimientoVehiculoPostDto()
            {
                CodigoParqueo = movp_codpar,
                Cilindraje = movp_cilindraje,
                CodigoTipoTransporte = movp_codtipt,
                ParqueoNumero = movp_parqueo_numero,
                Placa = movp_placa,
                FechaIngreso = DateTime.Now.AddHours(-10)
            };

            //Prueba
            var stringContent = new StringContent(JsonConvert.SerializeObject(movp), Encoding.UTF8, "application/json");

            var c = this.TestClient.PostAsync($"api/MovimientosParqueo/GuardarMovimientoVehiculo", stringContent).Result;
            var response = c.Content.ReadAsStringAsync().Result;
            var respuestaMovp = System.Text.Json.JsonSerializer.Deserialize<MovimientoVehiculoPostDto>(response);

            //Verificacion
            Assert.AreEqual(true, c.IsSuccessStatusCode);
        }

        [TestMethod]
        [DataRow(1, "123", 2, 6500, 1, 500, 4000)]
        //[DataRow(1, "123", 2, 500, 1, 500, 4000)]
        //[DataRow(2, "123", 2, 0, 1, 1000, 8000)]
        public void GenerarTicketParqueo(int movp_codpar, string movp_placa, int movp_codtipt, int movp_cilindraje, int movp_parqueo_numero, int ValorHora, int ValorDia)
        {
            #region Se inserta el movimiento
            var movpPut = new MovimientoVehiculoPostDto()
            {
                CodigoParqueo = movp_codpar,
                Cilindraje = movp_cilindraje,
                CodigoTipoTransporte = movp_codtipt,
                ParqueoNumero = movp_parqueo_numero,
                Placa = movp_placa,
                FechaIngreso = DateTime.Now.AddHours(-10)
            };
            //Prueba
            var stringContentPut = new StringContent(JsonConvert.SerializeObject(movpPut), Encoding.UTF8, "application/json");

            var c = this.TestClient.PostAsync($"api/MovimientosParqueo/GuardarMovimientoVehiculo", stringContentPut).Result;
            var response = c.Content.ReadAsStringAsync().Result;
            var respuestaMovpPut = System.Text.Json.JsonSerializer.Deserialize<MovimientoVehiculoPostDto>(response);
            #endregion

            #region Se simula el sacar el vehiculo
            var movp = new MovimientoVehiculoPutDto()
            {
                CodigoMovimiento = respuestaMovpPut.Codigo,
                HoraEntrada = respuestaMovpPut.FechaIngreso,
                CodigoTipoTransporte = respuestaMovpPut.CodigoTipoTransporte,
                Cilindraje = respuestaMovpPut.Cilindraje,
                ValorDia = ValorDia,
                ValorHora = ValorHora
            };

            //Prueba
            var stringContent = new StringContent(JsonConvert.SerializeObject(movp), Encoding.UTF8, "application/json");

            c = this.TestClient.PutAsync($"api/MovimientosParqueo/GenerarTicket/{respuestaMovpPut.Codigo}", stringContent).Result;
            response = c.Content.ReadAsStringAsync().Result;
            var respuestaMovp = System.Text.Json.JsonSerializer.Deserialize<MovimientoVehiculoPutDto>(response);
            #endregion

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

        [TestMethod]
        [DataRow(1)]
        public void ValidarCapacidadParqueo(int CodigoParqueo)
        {
            //Preparacion
            IEnumerable<VehiculosDisponiblesParqueoDto> capacidadParqueo;

            //Prueba
            var c = this.TestClient.GetAsync($"api/Capacidad/{CodigoParqueo}").Result;
            var response = c.Content.ReadAsStringAsync().Result;
            capacidadParqueo = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<VehiculosDisponiblesParqueoDto>>(response);

            //Verificacion
            Assert.IsTrue(capacidadParqueo.Count() > 0);
        }

        [TestMethod]
        [DataRow(1, 1, 1, "1", "I", "00:00", "23:59", 11)]
        public void CrearNuevoPicoPlaca(int CodigoPicoPlaca, int CodigoTipoTransporte, int DiaSemana, string Digito, string DigitoInicioFinal, string HoraInicio, string HoraFin, int Mes)
        {
            //Preparacion
            var dpp = new DetallePicoPlaca()
            {
                CodigoPicoPlaca = CodigoPicoPlaca,
                CodigoTipoTransporte = CodigoTipoTransporte,
                DiaSemana = DiaSemana,
                Digito = Digito,
                DigitoInicioFinal = DigitoInicioFinal,
                HoraInicio = HoraInicio,
                HoraFin = HoraFin,
                Mes = Convert.ToByte(Mes)
            };

            //Prueba
            var stringContent = new StringContent(JsonConvert.SerializeObject(dpp), Encoding.UTF8, "application/json");

            var c = this.TestClient.PostAsync($"api/PicoPlaca", stringContent).Result;
            var response = c.Content.ReadAsStringAsync().Result;
            var respuestaMovp = System.Text.Json.JsonSerializer.Deserialize<DetallePicoPlaca>(response);

            //Verificacion
            Assert.AreEqual(true, c.IsSuccessStatusCode);
        }

        [TestMethod]
        [DataRow(1)]
        public void OptenerLosMovimientosDeParqueo(int CodigoParqueo)
        {
            //Preparacion
            IEnumerable<SpMovimientosParqueoResult> MovimientosDisponibles;

            //Prueba
            var c = this.TestClient.GetAsync($"api/MovimientosParqueo/codpar/{CodigoParqueo}").Result;
            var response = c.Content.ReadAsStringAsync().Result;
            MovimientosDisponibles = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<SpMovimientosParqueoResult>>(response);

            //Verificacion
            Assert.IsTrue(MovimientosDisponibles.Any());
        }
    }
}
