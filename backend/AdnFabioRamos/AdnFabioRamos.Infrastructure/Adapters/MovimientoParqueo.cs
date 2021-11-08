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
        private readonly AdnCeibaContextProcedures _contextProcedures;
        private readonly IMapper _mapper;

        public MovimientoParqueo(AdnCeibaContext context, IMapper mapper)
        {
            _context = context;
            _contextProcedures = new AdnCeibaContextProcedures(context);
            _mapper = mapper;
        }

        public IEnumerable<SpMovimientosParqueoResult> Getmovp_movimiento_x_parqueo(int id)
        {
            var movp_movimiento_parqueo = _contextProcedures.SpMovimientosParqueoAsync(id).Result.ToList();

            return movp_movimiento_parqueo;
        }

        public async Task<MovimientoVehiculoPostDto> Post_GuardarMovimientoVehiculo(MovimientoVehiculoPostDto _movp)
        {
            var movp = new estacionamiento_adn.Models.MovimientoParqueo();

            _mapper.Map(_movp, movp);
            
            _context.MovimientoParqueo.Add(movp);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return _movp;
        }

        public async Task<MovimientoVehiculoPutDto> PutGenerarTicket(Guid id, MovimientoVehiculoPutDto movp)
        {
            var movp_movimiento_parqueo = await _context.MovimientoParqueo.FindAsync(id);

            movp_movimiento_parqueo.TotalPagar = movp.CantidadPagar;
            movp_movimiento_parqueo.HoraSalida = movp.HoraSalida;

            _context.Entry(movp_movimiento_parqueo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return movp;
        }
    }
}
