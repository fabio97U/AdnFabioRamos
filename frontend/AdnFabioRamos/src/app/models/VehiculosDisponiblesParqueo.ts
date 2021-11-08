export class VehiculosDisponiblesParqueo {
    constructor(
        public CodigoParqueo: number,
        public ParqueoNombre: string,
        public CodigoTipoTransporte: number,
        public TipoTransporte: string,
        public Capacidad: number,
        public ValorHora: number,
        public ValorDia: number
    ) { }
}