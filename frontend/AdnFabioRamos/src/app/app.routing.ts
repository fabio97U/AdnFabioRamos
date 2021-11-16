//Importar los modulos del router de angular
import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

//Importar los componenetes que tendran una pagina exclusiva
import { ParqueoComponent } from './components/parqueo/parqueo.component';
import { ErrorComponent } from './components/error/error.component';

//Array de rutas
const appRoutes: Routes = [
    { path: '', component: ParqueoComponent },
    { path: 'parqueo', component: ParqueoComponent },
    
    { path: '**', component: ErrorComponent }
]

// Exportar el modulo de rutas
export const appRoutingProvider: any[] = [];
export const routing: ModuleWithProviders<any> = RouterModule.forRoot(appRoutes);
