using AdnFabioRamos.Domain.Ports;
using AdnFabioRamos.Infrastructure.Persistence;
using AutoMapper;
using estacionamiento_adn.Models;
using estacionamiento_adn.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdnFabioRamos.Infrastructure.Adapters
{
    public class MovimientoParqueo : IMovimientoParqueo
    {
        readonly AdnCeibaContext _context;

        private readonly IMapper _mapper;

        public MovimientoParqueo(AdnCeibaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public static int[] LimiteParqueosVirtuales()
        {
            const int Limite = 50;

            var Marray = new int[Limite];
            for (int i = 0; i < Limite; i++)
            {
                Marray[i] = i + 1;
            }
            return Marray;
        }

        public async Task<IEnumerable<SpMovimientosParqueoResult>> Getmovp_movimiento_x_parqueo(int id)
        {
            List<SpMovimientosParqueoResult> lst_model = new List<SpMovimientosParqueoResult>();
            var model = new SpMovimientosParqueoResult();

            int[] cont = LimiteParqueosVirtuales();

            var datos_select =
                 from cap in await _context.Capacidad.ToListAsync()
                 join par in await _context.Parqueo.ToListAsync() on cap.CodigoParqueo equals par.Codigo
                 join tt in await _context.TipoTransporte.ToListAsync() on cap.CodigoTipoTransporte equals tt.Codigo
                 from num in cont.AsEnumerable().Where(t => t <= cap.Capacidad1)

                 join _movp in _context.MovimientoParqueo
                 on new
                 {
                     codpar = cap.CodigoParqueo,
                     movp_numero = num,
                     codtt = tt.Codigo,
                     hora_salida = true
                 }
                 equals new
                 {
                     codpar = _movp.CodigoParqueo,
                     movp_numero = (int)_movp.ParqueoNumero,
                     codtt = _movp.CodigoTipoTransporte,
                     hora_salida = (_movp.HoraSalida == null)
                 }
                 into movp_
                 from movp in movp_.DefaultIfEmpty()

                 where par.Codigo == id
                 orderby tt.Tipo, num
                 select new
                 { cap, par, tt, num, movp };

            foreach (var item in datos_select)
            {
                model.CodigoParqueo = item.par.Codigo;
                model.NombreParqueo = item.par.Nombre;
                model.CodigoTipoTransporte = item.tt.Codigo;
                model.TipoTransporte = item.tt.Tipo;
                model.Capacidad = item.cap.Capacidad1;
                model.ValorHora = item.cap.ValorHora;
                model.ValorDia = item.cap.ValorDia;
                model.Numero = item.num;
                if (item.movp != null)
                {
                    model.CodigoMovimientoParqueo = item.movp.Codigo;
                    model.MovimientoCodigoParqueo = item.movp.CodigoParqueo;
                    model.Placa = item.movp.Placa;
                    model.MovimientoCodigoTipoTransporte = item.movp.CodigoTipoTransporte;
                    model.Cilindraje = item.movp.Cilindraje;
                    model.ParqueoNumero = item.movp.ParqueoNumero;
                    model.HoraEntrada = item.movp.HoraEntrada;
                    model.HoraSalida = item.movp.HoraSalida;
                    model.TotalPagar = item.movp.TotalPagar;
                    model.FechaCreacion = item.movp.FechaCreacion;
                }
                lst_model.Add(model);
                model = new SpMovimientosParqueoResult();
            }
            return lst_model;
        }

        public async Task<MovimientoVehiculoPostDto> Post_GuardarMovimientoVehiculo(MovimientoVehiculoPostDto _movp)
        {
            var movp = new estacionamiento_adn.Models.MovimientoParqueo();

            _mapper.Map(_movp, movp);

            _context.MovimientoParqueo.Add(movp);

            await _context.SaveChangesAsync();

            if (_movp != null)
            {
                if (_movp.Codigo != Guid.Empty)
                {
                    _movp.Codigo = movp.Codigo;
                }
                return _movp;
            }

            return new MovimientoVehiculoPostDto();
        }

        public async Task<MovimientoVehiculoPutDto> PutGenerarTicket(Guid id, MovimientoVehiculoPutDto movp)
        {
            var movp_movimiento_parqueo = await _context.MovimientoParqueo.FindAsync(id);

            if (movp != null)
            {
                movp_movimiento_parqueo.TotalPagar = movp.CantidadPagar;
                movp_movimiento_parqueo.HoraSalida = movp.HoraSalida;

                _context.Entry(movp_movimiento_parqueo).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return movp;
            }
            else
            {
                throw new System.ArgumentNullException(nameof(movp), "No se envio el MovimientoVehiculoPutDto");
            }

        }
    }
}
