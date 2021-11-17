using System;
using System.Linq;
using AdnFabioRamos.Domain.Ports;
using AdnFabioRamos.Domain.Services;
using AdnFabioRamos.Infrastructure.Adapters;
using Microsoft.Extensions.DependencyInjection;

namespace AdnFabioRamos.Infrastructure.Extensions
{
    public static class ApiExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            var _services = AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => assembly.FullName.Contains("Domain", StringComparison.InvariantCulture))
                .SelectMany(s => s.GetTypes())
                .Where(p => p.CustomAttributes.Any(x => x.AttributeType == typeof(DomainServiceAttribute)));

            foreach (var _service in _services)
            {
                services.AddTransient(_service);
            }

            return services;
        }
    }
}
