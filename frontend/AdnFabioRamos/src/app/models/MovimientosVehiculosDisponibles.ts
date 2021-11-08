import { Guid } from "guid-typescript";

export class MovimientosVehiculosDisponibles {
    constructor(
        public CodigoParqueo: number,
        public NombreParqueo: string,
        public CodigoTipoTransporte: number,
        public TipoTransporte: string,
        public Capacidad: number,
        public ValorHora: number,
        public ValorDia: number,
        
        public Numero: number, 
        public CodigoMovimientoParqueo: Guid, 
        public MovimientoCodigoParqueo: number, 
        public Placa: string, 
        public MovimientoCodigoTipoTransporte: number, 
        public Cilindraje: number, 
        public MovimientoParqueoNumero: number, 
        public HoraEntrada: Date, 
        public HoraSalida: Date, 
        public TotalPagar: number, 
        public MovimientoFechaCreacion: Date, 
    ) { }
}