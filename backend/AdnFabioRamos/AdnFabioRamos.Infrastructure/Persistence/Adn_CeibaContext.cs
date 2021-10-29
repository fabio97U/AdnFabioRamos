using estacionamiento_adn.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdnFabioRamos.Infrastructure.Persistence
{
    public partial class Adn_CeibaContext: DbContext
    {
        public Adn_CeibaContext()
        {
        }

        public Adn_CeibaContext(DbContextOptions<Adn_CeibaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<cap_capacidad> cap_capacidad { get; set; }
        public virtual DbSet<dpp_detalle_pico_placa> dpp_detalle_pico_placa { get; set; }
        public virtual DbSet<movp_movimiento_parqueo> movp_movimiento_parqueo { get; set; }
        public virtual DbSet<par_parqueo> par_parqueo { get; set; }
        public virtual DbSet<pp_pico_placa> pp_pico_placa { get; set; }
        public virtual DbSet<tipt_tipo_transporte> tipt_tipo_transporte { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=adn_ceiba;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cap_capacidad>(entity =>
            {
                entity.HasKey(e => e.cap_codigo)
                    .HasName("PK__cap_capa__DFC2D768C5DCCE43");

                entity.Property(e => e.cap_fecha_creacion).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<dpp_detalle_pico_placa>(entity =>
            {
                entity.HasKey(e => e.dpp_codigo)
                    .HasName("PK__dpp_deta__773CA84ECC64E4FD");

                entity.HasIndex(e => new { e.dpp_digito, e.dpp_dia_semana, e.dpp_hora_inicio, e.dpp_hora_fin })
                    .HasName("index_detalle_pico_placa");

                entity.Property(e => e.dpp_dia_semana).HasComment("1: Lunes ... 7: Domingo");

                entity.Property(e => e.dpp_digito).HasDefaultValueSql("((1))");

                entity.Property(e => e.dpp_digito_inicio_final)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('I')")
                    .HasComment("Que la placa I: Inicie, F: Finalice");

                entity.Property(e => e.dpp_fecha_creacion).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.dpp_hora_fin).IsUnicode(false);

                entity.Property(e => e.dpp_hora_inicio).IsUnicode(false);

                entity.Property(e => e.dpp_mes).HasComment("1: Enero ... 12: Diciembre");
            });

            modelBuilder.Entity<movp_movimiento_parqueo>(entity =>
            {
                entity.HasKey(e => e.movp_codigo)
                    .HasName("PK__movp_mov__3292991CD8AC62B3");

                entity.HasIndex(e => new { e.movp_hora_entrada, e.movp_hora_salida })
                    .HasName("index_entrada_salida");

                entity.HasIndex(e => new { e.movp_codpar, e.movp_placa, e.movp_codtipt })
                    .HasName("index_busqueda_placa");

                entity.Property(e => e.movp_codigo).HasDefaultValueSql("(newid())");

                entity.Property(e => e.movp_cilindraje).HasComment("Las motos con un cilindraje mayor a 500cc tienen un sobrecargo de $2000");

                entity.Property(e => e.movp_fecha_creacion).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.movp_hora_entrada).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.movp_placa).IsUnicode(false);

                entity.Property(e => e.movp_total_pagar).HasDefaultValueSql("((0.0))");
            });

            modelBuilder.Entity<par_parqueo>(entity =>
            {
                entity.HasKey(e => e.par_codigo)
                    .HasName("PK__par_parq__87288BD9C8B44353");

                entity.Property(e => e.par_direccion).IsUnicode(false);

                entity.Property(e => e.par_fecha_creacion).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.par_nombre).IsUnicode(false);
            });

            modelBuilder.Entity<pp_pico_placa>(entity =>
            {
                entity.HasKey(e => e.pp_codigo)
                    .HasName("PK__pp_pico___0720D76C6366372D");

                entity.Property(e => e.pp_descripcion).IsUnicode(false);

                entity.Property(e => e.pp_fecha_creacion).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<tipt_tipo_transporte>(entity =>
            {
                entity.HasKey(e => e.tipt_codigo)
                    .HasName("PK__tipt_tip__C4986682D25D1912");

                entity.Property(e => e.tipt_descripcion).IsUnicode(false);

                entity.Property(e => e.tipt_fecha_creacion).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.tipt_tipo).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
