using estacionamiento_adn.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdnFabioRamos.Infrastructure.Persistence
{
    public partial class AdnCeibaContext: DbContext
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=adn_ceiba;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Capacidad>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__Capacida__06370DADDBB27605");

            });

            modelBuilder.Entity<DetallePicoPlaca>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__DetalleP__06370DAD42C8A42E");

                entity.Property(e => e.DiaSemana).HasComment("1: Lunes ... 7: Domingo");

                entity.Property(e => e.Digito)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('1')");

                entity.Property(e => e.DigitoInicioFinal)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('I')")
                    .HasComment("Que la placa I: Inicie, F: Finalice");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.HoraFin).IsUnicode(false);

                entity.Property(e => e.HoraInicio).IsUnicode(false);

                entity.Property(e => e.Mes).HasComment("1: Enero ... 12: Diciembre");
            });

            modelBuilder.Entity<MovimientoParqueo>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__Movimien__06370DAD8FDEB7C5");


                entity.Property(e => e.Codigo).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Cilindraje).HasComment("Las motos con un cilindraje mayor a 500cc tienen un sobrecargo de $2000");

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.HoraEntrada).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Placa).IsUnicode(false);

                entity.Property(e => e.TotalPagar).HasDefaultValueSql("((0.0))");
            });

            modelBuilder.Entity<Parqueo>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__Parqueo__06370DADB4218A90");

                entity.Property(e => e.Direccion).IsUnicode(false);

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre).IsUnicode(false);
            });

            modelBuilder.Entity<PicoPlaca>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__PicoPlac__06370DADE47F30DD");

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TipoTransporte>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__TipoTran__06370DAD366F3F2C");

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Tipo).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
