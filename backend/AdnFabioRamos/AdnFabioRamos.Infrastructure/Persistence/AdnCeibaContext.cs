using estacionamiento_adn.Models;
using Microsoft.EntityFrameworkCore;

namespace AdnFabioRamos.Infrastructure.Persistence
{
    public class AdnCeibaContext : DbContext
    {
        public AdnCeibaContext(DbContextOptions<AdnCeibaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Capacidad> Capacidad { get; set; }
        public virtual DbSet<DetallePicoPlaca> DetallePicoPlaca { get; set; }
        public virtual DbSet<MovimientoParqueo> MovimientoParqueo { get; set; }
        public virtual DbSet<Parqueo> Parqueo { get; set; }
        public virtual DbSet<PicoPlaca> PicoPlaca { get; set; }
        public virtual DbSet<TipoTransporte> TipoTransporte { get; set; }
    }
}
