import { Injectable } from '@angular/core';
import { VehiculosDisponiblesParqueo } from '../models/VehiculosDisponiblesParqueo';

//Para hacer la peticion http
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Global } from './global';

import { MovimientosVehiculosDisponibles } from '../models/MovimientosVehiculosDisponibles';
import { RespuestaPicoPlaca } from '../models/RespuestaPicoPlaca';
import { MovimientoVehiculoPost } from '../models/MovimientoVehiculoPost';
import { MovimientoVehiculoPut } from '../models/MovimientoVehiculoPut';

@Injectable() export class VehiculosDisponiblesParqueoService {

  constructor(private _httpClient: HttpClient) { }

  getCapacidad(): Observable<Array<VehiculosDisponiblesParqueo>> {
    return this._httpClient.get<Array<VehiculosDisponiblesParqueo>>(`${Global.url_api}/Capacidad/${Global.codpar}`);
  }

  getParqueos(): Observable<Array<MovimientosVehiculosDisponibles>> {
    return this._httpClient.get<Array<MovimientosVehiculosDisponibles>>(`${Global.url_api}/MovimientosParqueo/codpar/1`);
  }

  consultarPicoPlaca(TipoVehiculo: number, Placa: string): Observable<RespuestaPicoPlaca> {
    return this._httpClient.get<RespuestaPicoPlaca>(`${Global.url_api}/PicoPlaca/consultarPicoPlaca/${TipoVehiculo}/${Placa}`);
  }

  postMovimientoParqueo(Movp: MovimientoVehiculoPost): Observable<MovimientoVehiculoPost> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    let params = JSON.stringify(Movp);
    return this._httpClient.post<MovimientoVehiculoPost>(`${Global.url_api}/MovimientosParqueo/GuardarMovimientoVehiculo`, params, { headers: headers });
  }

  putMovimientoParqueo(Movp: MovimientoVehiculoPut): Observable<MovimientoVehiculoPut> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    let params = JSON.stringify(Movp);
    return this._httpClient.put<MovimientoVehiculoPut>(`${Global.url_api}/MovimientosParqueo/GenerarTicket/${Movp.CodigoMovimiento}`, params, { headers: headers });
  }
}
