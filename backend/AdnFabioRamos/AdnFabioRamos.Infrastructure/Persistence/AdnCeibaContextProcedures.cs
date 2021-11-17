using estacionamiento_adn.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Threading;

namespace AdnFabioRamos.Infrastructure.Persistence
{
    public partial class AdnCeibaContext
    {
        private AdnCeibaContextProcedures _procedures;

        public AdnCeibaContextProcedures Procedures
        {
            get
            {
                if (_procedures is null)
                {
                    _procedures = new AdnCeibaContextProcedures(this);
                }
                return _procedures;
            }
            set
            {
                _procedures = value;
            }
        }

        public AdnCeibaContextProcedures GetProcedures()
        {
            return Procedures;
        }
    }

    public class AdnCeibaContextProcedures
    {
        private readonly AdnCeibaContext _context;

        public AdnCeibaContextProcedures(AdnCeibaContext context)
        {
            _context = context;
        }

        public virtual async Task<List<SpMovimientosParqueoResult>> SpMovimientosParqueoAsync(int? codpar, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new[]
            {
                new SqlParameter
                {
                    ParameterName = "codpar",
                    Value = codpar ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<SpMovimientosParqueoResult>("EXEC @returnValue = [par].[SpMovimientosParqueo] @codpar", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        public virtual async Task<List<SpValidarPicoPlacaResult>> SpValidarPicoPlacaAsync(int? tipo_vehiculo, string placa, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new[]
            {
                new SqlParameter
                {
                    ParameterName = "tipo_vehiculo",
                    Value = tipo_vehiculo ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                new SqlParameter
                {
                    ParameterName = "placa",
                    Size = 25,
                    Value = placa ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<SpValidarPicoPlacaResult>("EXEC @returnValue = [pp].[SpValidarPicoPlaca] @tipo_vehiculo, @placa", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }
    }
}
