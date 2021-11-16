import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'filtrar'
})
export class FiltrarPipe implements PipeTransform {

    transform(items: any[], filter: { columna_filtrar: string, columna_items: string }): any {
        if (!items || !filter) {
            return items;
        }
        return items.filter(item => item[filter.columna_items].toString().indexOf(filter.columna_filtrar.toString()) !== -1);
    }
}
