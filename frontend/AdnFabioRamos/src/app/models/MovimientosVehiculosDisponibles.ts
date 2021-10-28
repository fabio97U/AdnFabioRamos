export class MovimientosVehiculosDisponibles {
    constructor(
        public par_codigo: number,
        public par_nombre: string,
        public tipt_codigo: number,
        public tipt_tipo: string,
        public cap_capacidad: number,
        public cap_valor_hora: number,
        public cap_valor_dia: number,
        
        public numero: number, 
        public movp_codigo: number, 
        public movp_codpar: number, 
        public movp_placa: string, 
        public movp_codtipt: number, 
        public movp_cilindraje: number, 
        public movp_parqueo_numero: number, 
        public movp_hora_entrada: Date, 
        public movp_hora_salida: Date, 
        public movp_total_pagar: number, 
        public movp_fecha_creacion: Date, 
    ) { }
}