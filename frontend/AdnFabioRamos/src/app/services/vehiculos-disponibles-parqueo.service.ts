import { Injectable } from '@angular/core';
import { VehiculosDisponiblesParqueo } from '../models/VehiculosDisponiblesParqueo';

//Para hacer la peticion http
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { Global } from './global';

import { MovimientosVehiculosDisponibles } from '../models/MovimientosVehiculosDisponibles';
import { RespuestaPicoPlaca } from '../models/RespuestaPicoPlaca';
import { MovimientoVehiculoPost } from '../models/MovimientoVehiculoPost';
import { MovimientoVehiculoPut } from '../models/MovimientoVehiculoPut';

@Injectable() export class VehiculosDisponiblesParqueoService {

  constructor(private _httpClient: HttpClient) { }

  getCapacidad(): Observable<Array<VehiculosDisponiblesParqueo>> {
    return this._httpClient.get<Array<VehiculosDisponiblesParqueo>>(Global.url_api + "/cap_capacidad/1");
  }

  getParqueos(): Observable<Array<MovimientosVehiculosDisponibles>> {
    return this._httpClient.get<Array<MovimientosVehiculosDisponibles>>(Global.url_api + "/movp_movimiento_parqueo/codpar/1");
  }

  consultarPicoPlaca(tipo_vehiculo: number, placa: string): Observable<RespuestaPicoPlaca> {
    return this._httpClient.get<RespuestaPicoPlaca>(Global.url_api + "/dpp_detalle_pico_placa/consultarPicoPlaca/" + tipo_vehiculo + "/" + placa);
  }

  postMovimientoParqueo(movp: MovimientoVehiculoPost): Observable<MovimientoVehiculoPost> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    let params = JSON.stringify(movp);
    return this._httpClient.post<MovimientoVehiculoPost>(Global.url_api + "/movp_movimiento_parqueo/GuardarMovimientoVehiculo", params, { headers: headers });
  }

  putMovimientoParqueo(movp: MovimientoVehiculoPut): Observable<MovimientoVehiculoPut> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    let params = JSON.stringify(movp);
    return this._httpClient.put<MovimientoVehiculoPut>(Global.url_api + "/movp_movimiento_parqueo/GenerarTicket/" + movp.movp_codigo, params, { headers: headers });
  }
}
