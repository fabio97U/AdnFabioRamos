using System;
using System.Linq;
using System.Reflection;
using AdnFabioRamos.Api.Filters;
using AdnFabioRamos.Domain.Ports;
using AdnFabioRamos.Infrastructure.Adapters;
using AdnFabioRamos.Infrastructure.Extensions;
using AdnFabioRamos.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus;

namespace AdnFabioRamos.Api
{
    public class Startup
    {
        readonly string _politicaCors = "MipoliticaCors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

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

            services.AddDbContext<AdnCeibaContext>();
            services.AddDbContext<AdnCeibaContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddCors(options =>
            {
                options.AddPolicy(name: _politicaCors,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            services.AddDomainServices();
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(AppExceptionFilterAttribute));
            });

            services
                .AddMvc()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddSwaggerDocument(settings =>
            {
                settings.Title = "Adn API Fabio Ramos";
                settings.Description = "API que consume la aplicacion frondend en Angular";
                settings.Version = "1.0";
            });

            services.AddResponseCompression();
            services.AddHealthChecks()
                .AddDbContextCheck<AdnFabioRamos.Infrastructure.Persistence.PersistenceContext>()
                .ForwardToPrometheus();

            services.AddScoped<ICapacidadRepository, CapacidadRespository>();
            services.AddScoped<IDetallePicoPlaca, DetallePicoPlacaRepository>();
            services.AddScoped<IMovimientoParqueo, MovimientoParqueo>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(_politicaCors);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapMetrics();
            });

            app.UseResponseCompression();
        }
    }
}
