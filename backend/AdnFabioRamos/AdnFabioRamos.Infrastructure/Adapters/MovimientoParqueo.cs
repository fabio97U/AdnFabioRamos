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


        public async Task<IEnumerable<sp_movimientos_parqueoResult>> Getmovp_movimiento_x_parqueo(int id)
        {
            var movp_movimiento_parqueo = _contextProcedures.sp_movimientos_parqueoAsync(id).Result.ToList();

            //if (movp_movimiento_parqueo.Count() == 0)
            //{
            //    return NotFound(new { respuesta = "No existen celdas para el parqueo " + id });
            //}

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
            }

            return _movp;
        }

        public async Task<MovimientoVehiculoPutDTO> PutGenerarTicket(Guid id, MovimientoVehiculoPutDTO movp)
        {
            //if (id != movp.movp_codigo)
            //{
            //    return BadRequest();
            //}
            var movp_movimiento_parqueo = await _context.movp_movimiento_parqueo.FindAsync(id);

            //cap_valor_hora
            //cap_valor_dia

            var hora_entrada = movp_movimiento_parqueo.movp_hora_entrada;
            var hora_salida = DateTime.Now;
            var cantidad_horas = (hora_salida - hora_entrada).Value.Hours;

            var cantidad_pagar = 0;

            if (cantidad_horas <= 9 || cantidad_horas <= 24)
            {
                cantidad_pagar = cantidad_horas * movp.cap_valor_hora;
            }
            else
            {
                var cantidida_dias_24h = Math.Truncate(Convert.ToDecimal(cantidad_horas / 24));

                var dias_9_horas = cantidad_horas - (cantidida_dias_24h * 15);

                var cantidad_dias_9h = dias_9_horas / 9;
            }

            if (movp.tipt_codigo == 1 && movp_movimiento_parqueo.movp_cilindraje > 500)//Es moto
            {
                cantidad_pagar = cantidad_pagar + 2000;
            }

            movp.movp_total_pagar = cantidad_pagar;

            movp_movimiento_parqueo.movp_total_pagar = cantidad_pagar;
            movp_movimiento_parqueo.movp_hora_salida = hora_salida;

            _context.Entry(movp_movimiento_parqueo).State = EntityState.Modified;

            try
            {
                //await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return movp;
        }
    }
}
