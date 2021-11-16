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
  public Seleccionado: MovimientosVehiculosDisponibles;

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

    this.Seleccionado = this.ListaMovp[0];

    config.backdrop = 'static';
    config.keyboard = false;
  }

  ngOnInit(): void {

  }

  async guardar(modal: any) {
    if (confirm(`¿Asignar el parqueo de “${this.Seleccionado.TipoTransporte}” numero “${this.Seleccionado.Numero}” al vehículo con placa “${this.Seleccionado.Placa}”?`)) {

      await this._VehiculosDisponiblesParqueoService.consultarPicoPlaca(this.Seleccionado.CodigoTipoTransporte, this.Seleccionado.Placa).subscribe(
        respon => {
          if (!respon.PermitirSalirAhora) {
            alert(`El vehiculo con placa “${this.Seleccionado.Placa}” no esta en Pico Placa, los dias permitidos son: “${respon.DiasPermitidosSalir}”`);
          } else {
            this._VehiculosDisponiblesParqueoService.postMovimientoParqueo(new MovimientoVehiculoPost(
              this.Seleccionado.CodigoParqueo, this.Seleccionado.Placa, this.Seleccionado.CodigoTipoTransporte, ((this.Seleccionado.Cilindraje) ? this.Seleccionado.Cilindraje : 0), this.Seleccionado.Numero
            )).subscribe(
              responParqueo => {
                alert('Parqueo reservado con éxito');
                this.cargarCeldas();
                modal.close('Save click');
              },
              errorParqueo => {

              }
            );
          }
        },
        error => {

        }
      );
    }
  }

  generarTicket(modal: any) {
    if (confirm(`¿Generar el ticket de pago parqueadero número “${this.Seleccionado.Numero}” (${this.Seleccionado.TipoTransporte})?`)) {
      this._VehiculosDisponiblesParqueoService.putMovimientoParqueo(new MovimientoVehiculoPut(
        this.Seleccionado.CodigoTipoTransporte, this.Seleccionado.ValorHora, this.Seleccionado.ValorDia, this.Seleccionado.CodigoMovimientoParqueo,
        this.Seleccionado.Placa, this.Seleccionado.Cilindraje, this.Seleccionado.HoraEntrada
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
    this.Seleccionado = movp;

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
