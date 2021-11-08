using AdnFabioRamos.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace AdnFabioRamos.Api.Tests
{
    public class WebapiAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            var host = 
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseStartup<TStartup>().ConfigureServices(services =>
                    {
                        var dbCtxOpts = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AdnCeibaContext>));

                        if (dbCtxOpts != null)
                        {
                            services.Remove(dbCtxOpts);
                        }

                        services.AddDbContext<AdnCeibaContext>(options =>
                        {
                            //options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=adn_ceiba;Integrated Security=True;multipleactiveresultsets=true;Connection Timeout=0");
                            options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                        });
                    });
                });

            

            return host;
        }
    }
}
