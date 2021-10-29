import { Component, OnInit, ViewChild } from '@angular/core';
import { MovimientosVehiculosDisponibles } from 'src/app/models/MovimientosVehiculosDisponibles';

import { NgbModalConfig, NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

import { VehiculosDisponiblesParqueo } from 'src/app/models/VehiculosDisponiblesParqueo';
import { VehiculosDisponiblesParqueoService } from '../../services/vehiculos-disponibles-parqueo.service';
import { MovimientoVehiculoPost } from 'src/app/models/MovimientoVehiculoPost';
import { MovimientoVehiculoPut } from 'src/app/models/MovimientoVehiculoPut';

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
    if (confirm(`¿Asignar el parqueo de “${this.movp_seleccionado.tipt_tipo}” numero “${this.movp_seleccionado.numero}” al vehículo con placa “${this.movp_seleccionado.movp_placa}”?`)) {

      await this._VehiculosDisponiblesParqueoService.consultarPicoPlaca(this.movp_seleccionado.tipt_codigo, this.movp_seleccionado.movp_placa).subscribe(
        respon => {
          if (!respon.permitir_salir_ahora) {
            alert(`El vehiculo con placa “${this.movp_seleccionado.movp_placa}” no esta en Pico Placa, los dias permitidos son: “${respon.dias_permitidos_salir}”`);
          } else {
            this._VehiculosDisponiblesParqueoService.postMovimientoParqueo(new MovimientoVehiculoPost(
              this.movp_seleccionado.par_codigo, this.movp_seleccionado.movp_placa, this.movp_seleccionado.tipt_codigo, ((this.movp_seleccionado.movp_cilindraje) ? this.movp_seleccionado.movp_cilindraje : 0), this.movp_seleccionado.numero
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
    if (confirm(`¿Generar el ticket de pago parqueadero número “${this.movp_seleccionado.numero}” (${this.movp_seleccionado.tipt_tipo})?`)) {
      this._VehiculosDisponiblesParqueoService.putMovimientoParqueo(new MovimientoVehiculoPut(
        this.movp_seleccionado.tipt_codigo, this.movp_seleccionado.cap_valor_hora, this.movp_seleccionado.cap_valor_dia, this.movp_seleccionado.movp_codigo,
        this.movp_seleccionado.movp_placa, 0
      )).subscribe(
        respon => {
          alert("Ticket generado con éxito, TOTAL A PAGAR: $ " + respon.movp_total_pagar);
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

      },
      error => {
        console.log(error);
      }
    );
  }
  open(content: any, movp: MovimientosVehiculosDisponibles): void {

    this.movp_seleccionado = movp;
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
