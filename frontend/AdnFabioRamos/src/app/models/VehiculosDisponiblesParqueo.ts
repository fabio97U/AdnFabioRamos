export class VehiculosDisponiblesParqueo {
    constructor(
        public par_codigo: number,
        public par_nombre: string,
        public tipt_codigo: number,
        public tipt_tipo: string,
        public cap_capacidad: number,
        public cap_valor_hora: number,
        public cap_valor_dia: number
    ) { }
}