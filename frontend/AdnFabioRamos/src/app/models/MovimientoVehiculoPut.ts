export class MovimientoVehiculoPut {
    constructor(
        public tipt_codigo: number, 
        public cap_valor_hora : number, 
        public cap_valor_dia : number,
        public movp_codigo: number,
        public movp_placa: string,
        public movp_total_pagar: number
    ) { }
}