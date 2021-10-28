using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AdnFabioRamos.Api.Filters;
using AdnFabioRamos.Domain.Ports;
using AdnFabioRamos.Infrastructure.Adapters;
using AdnFabioRamos.Infrastructure.Extensions;
using AdnFabioRamos.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus;

namespace AdnFabioRamos.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PersistenceContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("database"), sqlopts =>
                {
                    sqlopts.MigrationsHistoryTable("_MigrationHistory", Configuration.GetValue<string>("SchemaName"));
                });
            });

            services.AddMediatR(Assembly.Load("AdnFabioRamos.Application"), typeof(Startup).Assembly);
            var applicationAssemblyName = typeof(Startup).Assembly.GetReferencedAssemblies()
                .FirstOrDefault(x => x.Name.Equals("AdnFabioRamos.Application", StringComparison.InvariantCulture));

            services.AddAutoMapper(Assembly.Load(applicationAssemblyName.FullName));
            services.AddDomainServices();
            services.AddControllers( options => {
                options.Filters.Add(typeof(AppExceptionFilterAttribute));
            });
            services.AddSwaggerDocument();
            services.AddResponseCompression();
            services.AddHealthChecks()
                .AddDbContextCheck<AdnFabioRamos.Infrastructure.Persistence.PersistenceContext>()
                .ForwardToPrometheus();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            // app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapMetrics();
            });

            app.UseResponseCompression();

        }
    }
}
