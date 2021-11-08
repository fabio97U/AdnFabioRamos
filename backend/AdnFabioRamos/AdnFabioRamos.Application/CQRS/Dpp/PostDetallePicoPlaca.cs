using AdnFabioRamos.Domain.Ports;
using estacionamiento_adn.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AdnFabioRamos.Application.CQRS.Dpp
{
    public class PostDetallePicoPlaca : IRequest<DetallePicoPlaca>
    {
        public DetallePicoPlaca DetallePicoPlaca { get; set; } = new DetallePicoPlaca();

        public class PostDetallePicoPlacaHandler : IRequestHandler<PostDetallePicoPlaca, DetallePicoPlaca>
        {
            private readonly IDetallePicoPlaca _DetallePicoPlacaRepository;
            public PostDetallePicoPlacaHandler(IDetallePicoPlaca repository)
            {
                _DetallePicoPlacaRepository = repository;
            }

            public async Task<DetallePicoPlaca> Handle(PostDetallePicoPlaca request, CancellationToken cancellationToken)
            {
                return await _DetallePicoPlacaRepository.PostDetallePicoPlaca(request.DetallePicoPlaca);
            }
        }
    }
}
