using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AdnFabioRamos.Application.Person.Command;
using AdnFabioRamos.Application.Person.Queries;
using AdnFabioRamos.Domain.Entities;
using AdnFabioRamos.Domain.Ports;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdnFabioRamos.Api.Tests
{
    [TestClass]
    public class PersonControllerTest
    {
        readonly WebapiAppFactory<Startup> _AppFactory;
        readonly Guid _personId;
        public PersonControllerTest()
        {
            _AppFactory = new WebapiAppFactory<Startup>();
            _personId = Guid.NewGuid();
            SeedDataBase(_AppFactory.Services);
        }

        void SeedDataBase(IServiceProvider provider)
        {
            using (var scope = provider.CreateScope())
            {
                var _personRepository = scope.ServiceProvider
                    .GetRequiredService<IGenericRepository<Person>>();
                _ = _personRepository.AddAsync(new Person
                {
                    Id = _personId,
                    FirstName = "john",
                    LastName = "doe",
                    Email = "johndoe@foo.bar",
                    DateOfBirth = DateTime.Now.AddYears(-40)
                }).Result;
            }
        }

        [TestMethod]
        public async Task FindPersonSuccess()
        {
            var client = _AppFactory.CreateClient();
            var response = await client.GetAsync($"/api/person/{_personId.ToString()}");
            response.EnsureSuccessStatusCode();
            var person = JsonSerializer
            .Deserialize<PersonDto>(await response.Content.ReadAsStringAsync(), new System.Text.Json.JsonSerializerOptions
            {
                PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
            });
            Assert.IsTrue(person.FirstName.Equals("john"));
        }

        [TestMethod]
        public async Task RegisterPersonSuccess()
        {
            var client = _AppFactory.CreateClient();
            var request = new CreatePersonCommand("john", "doe", "johndoe@foo.bar", DateTime.Now.AddYears(-25));
            var requestContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/person", requestContent);
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task RegisterPersonAgeFailure()
        {
            HttpResponseMessage response = null;
            try
            {
                var client = _AppFactory.CreateClient();
                var request = new CreatePersonCommand("john", "doe", "johndoe@foo.bar", DateTime.Now.AddYears(-10));
                var requestContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                response = await client.PostAsync("/api/person", requestContent);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [TestMethod]
        public async Task RegisterPersonEmailFailure()
        {
            HttpResponseMessage response = null;
            try
            {
                var client = _AppFactory.CreateClient();
                var request = new CreatePersonCommand("john", "doe", "johndoe", DateTime.Now.AddYears(-30));
                var requestContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                response = await client.PostAsync("/api/person", requestContent);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }

        }
    }
}
