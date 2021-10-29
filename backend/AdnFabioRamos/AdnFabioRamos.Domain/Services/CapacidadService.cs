using AdnFabioRamos.Domain.Ports;
using estacionamiento_adn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdnFabioRamos.Domain.Services
{
    public class CapacidadService
    {
        readonly ICapacidadRepository _repository;
        public async Task<IEnumerable<VehiculosDisponiblesParqueoDTO>> GetCapacidadxPorParqueo(int codigo_parqueo)
        {
            return await _repository.GetCapacidadxPorParqueo(codigo_parqueo);
        }


    }
}
