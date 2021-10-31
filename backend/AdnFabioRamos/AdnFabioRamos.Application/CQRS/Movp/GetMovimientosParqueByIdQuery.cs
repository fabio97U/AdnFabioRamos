using AdnFabioRamos.Domain.Ports;
using AdnFabioRamos.Infrastructure.Persistence;
using estacionamiento_adn.Models;
using estacionamiento_adn.Models.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdnFabioRamos.Application.CQRS.Movp
{
    
    public class GetMovimientosParqueByIdQuery : IRequest<IEnumerable<sp_movimientos_parqueoResult>>
    {
        public int codigo_parqueo { get; set; }
        public class GetMovimientosParqueHandler : IRequestHandler<GetMovimientosParqueByIdQuery, IEnumerable<sp_movimientos_parqueoResult>>
        {
            private readonly IMovimientoParqueo _repositorio;
            public GetMovimientosParqueHandler(IMovimientoParqueo repository)
            {
                _repositorio = repository;
            }

            public async Task<IEnumerable<sp_movimientos_parqueoResult>> Handle(GetMovimientosParqueByIdQuery request, CancellationToken cancellationToken)
            {
                return _repositorio.Getmovp_movimiento_x_parqueo(request.codigo_parqueo);
            }

            private Task<IEnumerable<sp_movimientos_parqueoResult>> NotFound(object p)
            {
                throw new NotImplementedException();
            }
        }
    }
}
