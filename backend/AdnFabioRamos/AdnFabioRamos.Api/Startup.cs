using System;
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
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
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

            services.AddDbContext<Adn_CeibaContext>();
            services.AddDbContext<Adn_CeibaContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));



            //// ********************
            //// Setup CORS
            //// ********************
            //var corsBuilder = new CorsPolicyBuilder();
            //corsBuilder.AllowAnyHeader();
            //corsBuilder.AllowAnyMethod();
            //corsBuilder.AllowAnyOrigin(); // For anyone access.
            ////corsBuilder.WithOrigins("http://localhost:4200"); // for a specific url. Don't add a forward slash on the end!
            ////corsBuilder.AllowCredentials();

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
            //});

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //// ********************
            //// USE CORS - might not be required.
            //// ********************
            //app.UseCors("SiteCorsPolicy");
            app.UseCors(MyAllowSpecificOrigins);
            //app.UseCors("AllowOrigin");

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
