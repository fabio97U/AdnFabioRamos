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
    public class ParqueoTestIntegracion
    {
        readonly WebapiAppFactory<Startup> _AppFactory;
        protected HttpClient TestClient;
        public ParqueoTestIntegracion()
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
                services.GetRequiredService<AdnCeibaContext>();
                DBInicializer.Initialize(services);
            }
        }

        [TestMethod]
        [DataRow(1, "123")]
        [DataRow(2, "1234")]
        public void ValidarVehiculoEstaEnPicoPlaca(int CodigoTipoTransporte, string Placa)
        {
            //Preparacion
            var respuestaPicoPlaca = new RespuestaPicoPlaca();

            //Prueba
            var c = this.TestClient.GetAsync($"api/PicoPlaca/consultarPicoPlaca/{CodigoTipoTransporte}/{Placa}").Result;
            var response = c.Content.ReadAsStringAsync().Result;
            respuestaPicoPlaca = System.Text.Json.JsonSerializer.Deserialize<RespuestaPicoPlaca>(response);

            //Verificacion
            Assert.AreEqual(true, respuestaPicoPlaca.PermitirSalirAhora);
        }

        [TestMethod]
        [DataRow(1, "223")]
        [DataRow(2, "2234")]
        public void ValidarVehiculoNoEstaEnPicoPlaca(int CodigoTipoTransporte, string Placa)
        {
            //Preparacion
            var respuestaPicoPlaca = new RespuestaPicoPlaca();

            //Prueba
            var clienteHttp = this.TestClient.GetAsync($"api/PicoPlaca/consultarPicoPlaca/{CodigoTipoTransporte}/{Placa}").Result;
            var response = clienteHttp.Content.ReadAsStringAsync().Result;
            respuestaPicoPlaca = System.Text.Json.JsonSerializer.Deserialize<RespuestaPicoPlaca>(response);

            //Verificacion
            Assert.AreEqual(false, respuestaPicoPlaca.PermitirSalirAhora);
        }

        [TestMethod]
        [DataRow(1, "123", 2, 0, 1)]
        [DataRow(1, "1423", 2, 0, 1)]
        public void IngresarVehiculoParqueo(int CodigoParqueo, string Placa, int CodigoTipoTransporte, int Cilindraje, int ParqueoNumero)
        {
            //Preparacion
            var movimiento = new MovimientoVehiculoPostDto()
            {
                CodigoParqueo = CodigoParqueo,
                Cilindraje = Cilindraje,
                CodigoTipoTransporte = CodigoTipoTransporte,
                ParqueoNumero = ParqueoNumero,
                Placa = Placa,
                FechaIngreso = DateTime.Now.AddHours(-10)
            };

            //Prueba
            var stringContent = new StringContent(JsonConvert.SerializeObject(movimiento), Encoding.UTF8, "application/json");

            var clienteHttp = this.TestClient.PostAsync($"api/MovimientosParqueo/GuardarMovimientoVehiculo", stringContent).Result;
            var response = clienteHttp.Content.ReadAsStringAsync().Result;
            var respuestaMovimiento = System.Text.Json.JsonSerializer.Deserialize<MovimientoVehiculoPostDto>(response);

            //Verificacion
            Assert.AreEqual(true, clienteHttp.IsSuccessStatusCode);
        }

        [TestMethod]
        [DataRow(1, "123", 2, 6500, 1, 500, 4000)]
        //[DataRow(1, "123", 2, 500, 1, 500, 4000)]
        //[DataRow(2, "123", 2, 0, 1, 1000, 8000)]
        public void GenerarTicketParqueo(int CodigoParqueo, string Placa, int CodigoTipoTransporte, int Cilindraje, int ParqueoNumero, int ValorHora, int ValorDia)
        {
            #region Se inserta el movimiento
            var movpPut = new MovimientoVehiculoPostDto()
            {
                CodigoParqueo = CodigoParqueo,
                Cilindraje = Cilindraje,
                CodigoTipoTransporte = CodigoTipoTransporte,
                ParqueoNumero = ParqueoNumero,
                Placa = Placa,
                FechaIngreso = DateTime.Now.AddHours(-10)
            };
            //Prueba
            var stringContentPut = new StringContent(JsonConvert.SerializeObject(movpPut), Encoding.UTF8, "application/json");

            var clienteHttp = this.TestClient.PostAsync($"api/MovimientosParqueo/GuardarMovimientoVehiculo", stringContentPut).Result;
            var response = clienteHttp.Content.ReadAsStringAsync().Result;
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

            clienteHttp = this.TestClient.PutAsync($"api/MovimientosParqueo/GenerarTicket/{respuestaMovpPut.Codigo}", stringContent).Result;
            response = clienteHttp.Content.ReadAsStringAsync().Result;
            var respuestaMovp = System.Text.Json.JsonSerializer.Deserialize<MovimientoVehiculoPutDto>(response);
            #endregion

            //Verificacion
            Assert.AreEqual(true, clienteHttp.IsSuccessStatusCode);
        }

        [TestMethod]
        [DataRow(1)]
        public void ValidarCapacidadParqueo(int CodigoParqueo)
        {
            //Preparacion
            IEnumerable<VehiculosDisponiblesParqueoDto> capacidadParqueo;

            //Prueba
            var clienteHttp = this.TestClient.GetAsync($"api/Capacidad/{CodigoParqueo}").Result;
            var response = clienteHttp.Content.ReadAsStringAsync().Result;
            capacidadParqueo = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<VehiculosDisponiblesParqueoDto>>(response);

            //Verificacion
            Assert.IsTrue(capacidadParqueo.Any());
        }

        [TestMethod]
        [DataRow(2, 1, 1, "1", "I", "00:00", "23:59", 11)]
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

            var clienteHttp = this.TestClient.PostAsync($"api/PicoPlaca", stringContent).Result;
            var response = clienteHttp.Content.ReadAsStringAsync().Result;

            //Verificacion
            Assert.AreEqual(true, clienteHttp.IsSuccessStatusCode);
        }

        [TestMethod]
        [DataRow(1)]
        public void OptenerLosMovimientosDeParqueo(int CodigoParqueo)
        {
            //Preparacion
            IEnumerable<SpMovimientosParqueoResult> MovimientosDisponibles;

            //Prueba
            var clienteHttp = this.TestClient.GetAsync($"api/MovimientosParqueo/Codigo/{CodigoParqueo}").Result;
            var response = clienteHttp.Content.ReadAsStringAsync().Result;
            MovimientosDisponibles = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<SpMovimientosParqueoResult>>(response);

            //Verificacion
            Assert.IsTrue(MovimientosDisponibles.Any());
        }
    }
}
