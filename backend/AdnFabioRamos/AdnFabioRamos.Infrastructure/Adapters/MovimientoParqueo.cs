using AdnFabioRamos.Domain.Ports;
using AdnFabioRamos.Infrastructure.Persistence;
using AutoMapper;
using estacionamiento_adn.Models;
using estacionamiento_adn.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdnFabioRamos.Infrastructure.Adapters
{
    public class MovimientoParqueo : IMovimientoParqueo
    {
        readonly Adn_CeibaContext _context;
        private readonly Adn_CeibaContextProcedures _contextProcedures;
        private readonly IMapper _mapper;

        public MovimientoParqueo(Adn_CeibaContext context, IMapper mapper)
        {
            _context = context;
            _contextProcedures = new Adn_CeibaContextProcedures(context);
            _mapper = mapper;
        }

        public IEnumerable<sp_movimientos_parqueoResult> Getmovp_movimiento_x_parqueo(int id)
        {
            var movp_movimiento_parqueo = _contextProcedures.sp_movimientos_parqueoAsync(id).Result.ToList();

            return movp_movimiento_parqueo;
        }

        public async Task<MovimientoVehiculoPostDTO> Post_GuardarMovimientoVehiculo(MovimientoVehiculoPostDTO _movp)
        {
            var movp = new movp_movimiento_parqueo();

            _mapper.Map(_movp, movp);

            _context.movp_movimiento_parqueo.Add(movp);
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

        public async Task<MovimientoVehiculoPutDTO> PutGenerarTicket(Guid id, MovimientoVehiculoPutDTO movp)
        {
            var movp_movimiento_parqueo = await _context.movp_movimiento_parqueo.FindAsync(id);
            movp_movimiento_parqueo.movp_total_pagar = movp.cantidad_pagar;
            movp_movimiento_parqueo.movp_hora_salida = movp.hora_salida;

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
