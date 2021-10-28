export class MovimientoVehiculoPost {
    constructor(
        public movp_codpar: number, 
        public movp_placa: string, 
        public movp_codtipt: number, 
        public movp_cilindraje: number, 
        public movp_parqueo_numero: number,
    ) { }
}