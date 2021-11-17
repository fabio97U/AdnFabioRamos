export class MovimientoVehiculoPost {
    constructor(
        public CodigoParqueo: number,
        public Placa: string,
        public CodigoTipoTransporte: number,
        public Cilindraje: number,
        public ParqueoNumero: number,
        
        public PermitirSalirAhora: boolean,
        public DiasPermitidosSalir: string,
    ) { }
}
