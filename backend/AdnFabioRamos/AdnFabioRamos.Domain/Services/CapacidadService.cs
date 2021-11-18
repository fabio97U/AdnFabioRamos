using AdnFabioRamos.Domain.Ports;
using estacionamiento_adn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdnFabioRamos.Domain.Services
{
    public class CapacidadService
    {
        readonly ICapacidadRepository _repository;
        public CapacidadService(ICapacidadRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<VehiculosDisponiblesParqueoDto>> GetCapacidadxPorParqueo(int codigo_parqueo)
        {
            return await _repository.GetCapacidadxPorParqueo(codigo_parqueo);
        }
    }
}
