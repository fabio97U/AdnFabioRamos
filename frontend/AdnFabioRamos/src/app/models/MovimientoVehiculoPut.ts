import { Guid } from 'guid-typescript';

export class MovimientoVehiculoPut {
    constructor(
        public CodigoTipoTransporte: number,
        public ValorHora: number,
        public ValorDia: number,
        public CodigoMovimiento: Guid,
        public Placa: string,

        public Cilindraje: number,
        public HoraEntrada: Date,

        public HoraSalida: Date = new Date(),
        public CantidadHoras: number = 0.0,
        public Dias9H: number = 0.0,
        public HorasRestantes: number = 0.0,
        public TotalPagarDias9H: number = 0.0,
        public TotalPagarHorasRestantes: number = 0.0,
        public CantidadPagar: number = 0.0,
    ) { }
}
