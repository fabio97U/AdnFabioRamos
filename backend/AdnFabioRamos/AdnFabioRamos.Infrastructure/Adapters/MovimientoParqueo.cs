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

        public async Task<IEnumerable<SpMovimientosParqueoResult>> Getmovp_movimiento_x_parqueo(int id)
        {
            //var movp_movimiento_parqueo = _contextProcedures.SpMovimientosParqueoAsync(id).Result.ToList();
            List<SpMovimientosParqueoResult> lst_model = new List<SpMovimientosParqueoResult>();
            var model = new SpMovimientosParqueoResult();

            int[] cont = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50 };

            var datos_select =
                 from cap in await _context.Capacidad.ToListAsync()
                 join par in await _context.Parqueo.ToListAsync() on cap.CodigoParqueo equals par.Codigo
                 join tt in await _context.TipoTransporte.ToListAsync() on cap.CodigoTipoTransporte equals tt.Codigo
                 from num in cont.ToList().Where(t => t <= cap.Capacidad1)

                 join _movp in _context.MovimientoParqueo
                 on new
                 {
                     codpar = (int)(cap.CodigoParqueo),
                     movp_numero = (int)num,
                     codtt = tt.Codigo,
                     hora_salida = true
                 }
                 equals new
                 {
                     codpar = (int)(_movp.CodigoParqueo),
                     movp_numero = (int)_movp.ParqueoNumero,
                     codtt = (int)_movp.CodigoTipoTransporte,
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
            //return movp_movimiento_parqueo;
            return lst_model;
        }

        public async Task<MovimientoVehiculoPostDto> Post_GuardarMovimientoVehiculo(MovimientoVehiculoPostDto _movp)
        {
            var movp = new estacionamiento_adn.Models.MovimientoParqueo();

            _mapper.Map(_movp, movp);

            _context.MovimientoParqueo.Add(movp);

            await _context.SaveChangesAsync();

            _movp.Codigo = movp.Codigo;

            return _movp;
        }

        public async Task<MovimientoVehiculoPutDto> PutGenerarTicket(Guid id, MovimientoVehiculoPutDto movp)
        {
            var movp_movimiento_parqueo = await _context.MovimientoParqueo.FindAsync(id);

            movp_movimiento_parqueo.TotalPagar = movp.CantidadPagar;
            movp_movimiento_parqueo.HoraSalida = movp.HoraSalida;

            _context.Entry(movp_movimiento_parqueo).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return movp;
        }
    }
}
