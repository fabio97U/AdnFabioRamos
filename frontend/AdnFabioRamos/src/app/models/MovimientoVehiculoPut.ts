import { Guid } from "guid-typescript";

export class MovimientoVehiculoPut {
    constructor(
        public tipt_codigo: number, 
        public cap_valor_hora : number, 
        public cap_valor_dia : number,
        public movp_codigo: Guid,
        public movp_placa: string,
        
        public movp_cilindraje: number,
        public movp_hora_entrada: Date,
        
        public hora_salida: Date = new Date(),
        public cantidad_horas : number = 0.0,
        public dias_9h : number = 0.0,
        public horas_restantes : number = 0.0,
        public total_pagar_dias_9h : number = 0.0,
        public total_pagar_horas_restantes : number = 0.0,
        public cantidad_pagar : number = 0.0,

    ) { }
}