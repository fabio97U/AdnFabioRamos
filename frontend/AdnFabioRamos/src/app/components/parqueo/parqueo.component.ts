import { Component, OnInit } from '@angular/core';
import { MovimientosVehiculosDisponibles } from 'src/app/models/MovimientosVehiculosDisponibles';

import { NgbModalConfig, NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

import { VehiculosDisponiblesParqueo } from 'src/app/models/VehiculosDisponiblesParqueo';
import { VehiculosDisponiblesParqueoService } from '../../services/vehiculos-disponibles-parqueo.service';
import { MovimientoVehiculoPost } from 'src/app/models/MovimientoVehiculoPost';
import { MovimientoVehiculoPut } from 'src/app/models/MovimientoVehiculoPut';

@Component({
  selector: 'app-parqueo',
  templateUrl: './parqueo.component.html',
  providers: [VehiculosDisponiblesParqueoService],

})
export class ParqueoComponent implements OnInit {

  closeResult = '';

  public Lista: Array<VehiculosDisponiblesParqueo> = [];
  public ListaMovp: Array<MovimientosVehiculosDisponibles> = [];
  public Movp: MovimientosVehiculosDisponibles;

  constructor(
    private _VehiculosDisponiblesParqueoService: VehiculosDisponiblesParqueoService,
    private modalService: NgbModal,
    config: NgbModalConfig) {
    _VehiculosDisponiblesParqueoService.getCapacidad().subscribe(
      respon => {
        this.Lista = respon;
      },
      error => {

      }
    );

    this.cargarCeldas();

    this.Movp = this.ListaMovp[0];

    config.backdrop = 'static';
    config.keyboard = false;
  }

  ngOnInit(): void {

  }

  async guardar(modal: any) {
    if (confirm(`¿Asignar el parqueo de “${this.Movp.TipoTransporte}” numero “${this.Movp.Numero}” al vehículo con placa “${this.Movp.Placa}”?`)) {
      this._VehiculosDisponiblesParqueoService.postMovimientoParqueo(new MovimientoVehiculoPost(
        this.Movp.CodigoParqueo, this.Movp.Placa, this.Movp.CodigoTipoTransporte, ((this.Movp.Cilindraje) ? this.Movp.Cilindraje : 0), this.Movp.Numero
        , false, ""
      )).subscribe(
        responParqueo => {
          if (!responParqueo.PermitirSalirAhora) {
            alert(`El vehiculo con placa “${this.Movp.Placa}” no esta en Pico Placa, los dias permitidos son: “${responParqueo.DiasPermitidosSalir}”`);
          } else {
            alert('Parqueo reservado con éxito');
            this.cargarCeldas();
            modal.close('Save click');
          }
        },
        errorParqueo => {

        }
      );
    }
  }

  generarTicket(modal: any) {
    if (confirm(`¿Generar el ticket de pago parqueadero número “${this.Movp.Numero}” (${this.Movp.TipoTransporte})?`)) {
      this._VehiculosDisponiblesParqueoService.putMovimientoParqueo(new MovimientoVehiculoPut(
        this.Movp.CodigoTipoTransporte, this.Movp.ValorHora, this.Movp.ValorDia, this.Movp.CodigoMovimientoParqueo,
        this.Movp.Placa, this.Movp.Cilindraje, this.Movp.HoraEntrada
      )).subscribe(
        respon => {
          alert(`
                          *Ticket generado con éxito*
          Hora de salida: ${respon.HoraSalida.toLocaleString()}
          Valor hora: ${respon.ValorHora}
          Valor dia: ${respon.ValorDia}
          TOTAL DE HORAS: ${respon.CantidadHoras}
          ----------*----------
          Dias 9h: ${respon.Dias9H}
          Total a pagar: ${respon.TotalPagarDias9H}
          ----------*----------
          Horas restantes: ${respon.HorasRestantes}
          Total a pagar: ${respon.TotalPagarHorasRestantes}
          ----------*----------
          TOTAL A PAGAR (dias + horas): ${respon.CantidadPagar}`);
          this.cargarCeldas();
          modal.close('Save click');
        },
        error => {

        }
      );

      modal.dismiss('cancel click');
    }

  }

  onSumbit(): void {

  }

  cargarCeldas(): void {
    this._VehiculosDisponiblesParqueoService.getParqueos().subscribe(
      respon => {
        this.ListaMovp = respon;

      },
      error => {

      }
    );
  }
  open(content: any, movp: MovimientosVehiculosDisponibles): void {
    this.Movp = movp;

    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }

}
