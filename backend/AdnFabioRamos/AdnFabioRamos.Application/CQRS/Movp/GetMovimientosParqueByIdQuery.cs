using AdnFabioRamos.Domain.Ports;
using estacionamiento_adn.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AdnFabioRamos.Application.CQRS.Movp
{
    public class GetMovimientosParqueByIdQuery : IRequest<IEnumerable<SpMovimientosParqueoResult>>
    {
        public int CodigoParqueo { get; set; }
        public class GetMovimientosParqueHandler : IRequestHandler<GetMovimientosParqueByIdQuery, IEnumerable<SpMovimientosParqueoResult>>
        {
            private readonly IMovimientoParqueo _repositorio;
            public GetMovimientosParqueHandler(IMovimientoParqueo repository)
            {
                _repositorio = repository;
            }

            public async Task<IEnumerable<SpMovimientosParqueoResult>> Handle(GetMovimientosParqueByIdQuery request, CancellationToken cancellationToken)
            {
                if (request != null)
                {
                    return _repositorio.Getmovp_movimiento_x_parqueo(request.CodigoParqueo);
                }
                else
                {
                    throw new System.ArgumentNullException(nameof(request), "No se envio el Codigo del Parqueo");
                }
            }
        }
    }
}
