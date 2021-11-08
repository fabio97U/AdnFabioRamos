import { Component, OnInit, ViewChild } from '@angular/core';
import { MovimientosVehiculosDisponibles } from 'src/app/models/MovimientosVehiculosDisponibles';

import { NgbModalConfig, NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

import { VehiculosDisponiblesParqueo } from 'src/app/models/VehiculosDisponiblesParqueo';
import { VehiculosDisponiblesParqueoService } from '../../services/vehiculos-disponibles-parqueo.service';
import { MovimientoVehiculoPost } from 'src/app/models/MovimientoVehiculoPost';
import { MovimientoVehiculoPut } from 'src/app/models/MovimientoVehiculoPut';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-parqueo',
  templateUrl: './parqueo.component.html',
  styleUrls: ['./parqueo.component.css'],
  providers: [VehiculosDisponiblesParqueoService],

})
export class ParqueoComponent implements OnInit {

  closeResult = '';

  public lista: Array<VehiculosDisponiblesParqueo> = [];
  public lista_movp: Array<MovimientosVehiculosDisponibles> = [];
  public movp_seleccionado: MovimientosVehiculosDisponibles;

  constructor(
    private _VehiculosDisponiblesParqueoService: VehiculosDisponiblesParqueoService,
    private modalService: NgbModal,
    config: NgbModalConfig) {
    _VehiculosDisponiblesParqueoService.getCapacidad().subscribe(
      respon => {
        this.lista = respon;
        console.log(respon);
        
      },
      error => {
        console.log(error);
      }
    );

    this.cargarCeldas();

    this.movp_seleccionado = this.lista_movp[0];

    config.backdrop = 'static';
    config.keyboard = false;
  }

  ngOnInit(): void {

  }

  async guardar(modal: any) {
    if (confirm(`¿Asignar el parqueo de “${this.movp_seleccionado.TipoTransporte}” numero “${this.movp_seleccionado.Numero}” al vehículo con placa “${this.movp_seleccionado.Placa}”?`)) {

      await this._VehiculosDisponiblesParqueoService.consultarPicoPlaca(this.movp_seleccionado.CodigoTipoTransporte, this.movp_seleccionado.Placa).subscribe(
        respon => {
          if (!respon.PermitirSalirAhora) {
            alert(`El vehiculo con placa “${this.movp_seleccionado.Placa}” no esta en Pico Placa, los dias permitidos son: “${respon.DiasPermitidosSalir}”`);
          } else {
            this._VehiculosDisponiblesParqueoService.postMovimientoParqueo(new MovimientoVehiculoPost(
              this.movp_seleccionado.CodigoParqueo, this.movp_seleccionado.Placa, this.movp_seleccionado.CodigoTipoTransporte, ((this.movp_seleccionado.Cilindraje) ? this.movp_seleccionado.Cilindraje : 0), this.movp_seleccionado.Numero
            )).subscribe(
              respon => {
                alert("Parqueo reservado con éxito");
                this.cargarCeldas();
                modal.close('Save click');
              },
              error => {
                console.log(error);
              }
            );
          }
        },
        error => {
          console.log(error);
        }
      );
    }
  }

  generarTicket(modal: any) {
    if (confirm(`¿Generar el ticket de pago parqueadero número “${this.movp_seleccionado.Numero}” (${this.movp_seleccionado.TipoTransporte})?`)) {
      this._VehiculosDisponiblesParqueoService.putMovimientoParqueo(new MovimientoVehiculoPut(
        this.movp_seleccionado.CodigoTipoTransporte, this.movp_seleccionado.ValorHora, this.movp_seleccionado.ValorDia, this.movp_seleccionado.CodigoMovimientoParqueo,
        this.movp_seleccionado.Placa, this.movp_seleccionado.Cilindraje, this.movp_seleccionado.HoraEntrada
      )).subscribe(
        respon => {
          alert(`
                          *Ticket generado con éxito*
          Hora de salida: ${respon.HoraSalida.toLocaleString()}
          Valor hora: ${respon.ValorHora}
          Valor dia: ${respon.ValorDia}
          TOTAL DE HORAS: ${respon.CantidadHoras}
          ----------*----------
          Dias 9h: ${respon.Dias9h}
          Total a pagar: ${respon.TotalPagarDias9h}
          ----------*----------
          Horas restantes: ${respon.HorasRestantes}
          Total a pagar: ${respon.TotalPagarHorasRestantes}
          ----------*----------
          TOTAL A PAGAR (dias + horas): ${respon.CantidadPagar}`);
          this.cargarCeldas();
          modal.close('Save click');
        },
        error => {
          console.log(error);
        }
      );

      modal.dismiss('cancel click');
    }

  }

  onSumbit(): void {
    console.log(this.movp_seleccionado);
  }

  cargarCeldas(): void {
    this._VehiculosDisponiblesParqueoService.getParqueos().subscribe(
      respon => {
        this.lista_movp = respon;
        console.log(respon);
      },
      error => {
        console.log(error);
      }
    );
  }
  open(content: any, movp: MovimientosVehiculosDisponibles): void {
    this.movp_seleccionado = movp;
    console.log(this.movp_seleccionado);
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
