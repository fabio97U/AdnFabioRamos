create database adn_ceiba;
go
use adn_ceiba
go
create schema pp;--pico_placa
go
create schema par;--parqueo
go

-- drop table par.TipoTransporte
create table par.TipoTransporte (
	Codigo int primary key identity(1, 1),
	Tipo varchar(50) not null,
	Descripcion varchar(75) not null,
	FechaCreacion datetime default getdate()
)
-- select * from par.TipoTransporte
insert into par.TipoTransporte (Tipo, Descripcion)
values ('Moto', 'Toda tipo de moto'), ('Carro', 'Toda tipo de carro')

-- drop table pp.PicoPlaca
create table pp.PicoPlaca (
	Codigo int primary key identity(1, 1),
	Anio int not null,
	Descripcion varchar(255) not null, 
	FechaCreacion datetime default getdate()
)
-- select * from pp.PicoPlaca
insert into pp.PicoPlaca (Anio, Descripcion)
values (2021, 'Pico placa para el año 2021 en Colombia');

-- drop table pp.DetallePicoPlaca
create table pp.DetallePicoPlaca (
	Codigo int primary key identity(1, 1),
	CodigoPicoPlaca int foreign key references pp.PicoPlaca not null,
	CodigoTipoTransporte int foreign key references par.TipoTransporte not null,
	Mes tinyint not null,
	HoraInicio varchar(5) not null,
	HoraFin varchar(5) not null,

	DiaSemana int not null check (DiaSemana >= 1 and DiaSemana <= 7),

	Digito CHAR(1) not null default '1', 

	DigitoInicioFinal varchar(2) not null default 'I' check (DigitoInicioFinal='I' or DigitoInicioFinal='F'),

	FechaCreacion datetime default getdate()
)
-- select * from pp.DetallePicoPlaca
insert into pp.DetallePicoPlaca (CodigoPicoPlaca, CodigoTipoTransporte, Mes, HoraInicio, HoraFin, Digito, DiaSemana)
values (1, 2, 11, '00:00', '23:59', 1, 7), (1, 2, 11, '00:00', '23:59', 1, 1), (1, 2, 11, '00:00', '23:59', 8, 3), (1, 2, 11, '00:00', '23:59', 9, 4)
insert into pp.DetallePicoPlaca (CodigoPicoPlaca, CodigoTipoTransporte, Mes, HoraInicio, HoraFin, Digito, DiaSemana)
values (1, 1, 11, '00:00', '23:59', 1, 7), (1, 1, 11, '00:00', '23:59', 1, 1)

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1: Enero ... 12: Diciembre' , 
	@level0type=N'SCHEMA',@level0name=N'pp', @level1type=N'TABLE',@level1name=N'DetallePicoPlaca', 
	@level2type=N'COLUMN',@level2name=N'Mes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Que la placa I: Inicie, F: Finalice' , 
	@level0type=N'SCHEMA',@level0name=N'pp', @level1type=N'TABLE',@level1name=N'DetallePicoPlaca', 
	@level2type=N'COLUMN',@level2name=N'DigitoInicioFinal'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1: Lunes ... 7: Domingo' , 
	@level0type=N'SCHEMA',@level0name=N'pp', @level1type=N'TABLE',@level1name=N'DetallePicoPlaca', 
	@level2type=N'COLUMN',@level2name=N'DiaSemana'
GO
create index index_detalle_pico_placa on pp.DetallePicoPlaca (Digito, DiaSemana, HoraInicio, HoraFin);
go

-- drop table par.Parqueo
create table par.Parqueo (
	Codigo int primary key identity(1, 1),
	Nombre varchar(50) not null,
	Direccion varchar(255) not null,
	FechaCreacion datetime default getdate()
)
-- select * from par.Parqueo
insert into par.Parqueo (Nombre, Direccion)
values ('Parqueo Ceiba', 'Cl. 8B ##65-191, Medellín, Antioquia, Colombia')

-- drop table par.Capacidad
create table par.Capacidad (
	Codigo int primary key identity(1, 1),
	CodigoParqueo int foreign key references par.Parqueo not null,
	CodigoTipoTransporte int foreign key references par.TipoTransporte not null,
	Capacidad smallint not null,
	ValorHora float not null,
	ValorDia float not null,
	FechaCreacion datetime default getdate()
)
-- select * from par.Capacidad
insert into par.Capacidad (CodigoParqueo, CodigoTipoTransporte, Capacidad, ValorHora, ValorDia)
values (1, 1, 10, 500, 4000), (1, 2, 20, 1000, 8000)

-- drop table par.MovimientoParqueo
create table par.MovimientoParqueo (
	Codigo uniqueidentifier primary key default newid(),
	CodigoParqueo int foreign key references par.Parqueo not null,
	Placa varchar(50) not null,
	CodigoTipoTransporte int foreign key references par.TipoTransporte not null,
	Cilindraje int null,
	ParqueoNumero smallint not null, 

	HoraEntrada datetime default getdate(),
	HoraSalida datetime null,
	TotalPagar float null default 0.0,

	FechaCreacion datetime default getdate()
)
-- select * from par.MovimientoParqueo
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Las motos con un cilindraje mayor a 500cc tienen un sobrecargo de $2000' , 
	@level0type=N'SCHEMA',@level0name=N'par', @level1type=N'TABLE',@level1name=N'MovimientoParqueo', 
	@level2type=N'COLUMN',@level2name=N'Cilindraje'
GO

create index index_busqueda_placa on par.MovimientoParqueo (CodigoParqueo, Placa, CodigoTipoTransporte);
create index index_entrada_salida on par.MovimientoParqueo (HoraEntrada, HoraSalida);
go

	-- exec par.SpMovimientosParqueo 1
create or alter procedure par.SpMovimientosParqueo
	@codpar int
as
begin
	declare @tbl_contador as table (numero int)
	insert into @tbl_contador (numero) 
	values (1), (2), (3), (4), (5), (6), (7), (8), (9), (10), 
	(11), (12), (13), (14), (15), (16), (17), (18), (19), (20), 
	(21), (22), (23), (24), (25), (26), (27), (28), (29), (30),
	(31), (32), (33), (34), (35), (36), (37), (38), (39), (40), 
	(41), (42), (43), (44), (45), (46), (47), (48), (49), (50)

	select Parqueo.Codigo 'CodigoParqueo', Nombre 'NombreParqueo', TipoTransporte.Codigo 'CodigoTipoTransporte', 
	Tipo 'TipoTransporte', Capacidad, ValorHora, ValorDia, t.numero 'Numero', 

	MovimientoParqueo.Codigo 'CodigoMovimientoParqueo', MovimientoParqueo.CodigoParqueo 'MovimientoCodigoParqueo', Placa, 
	MovimientoParqueo.CodigoTipoTransporte 'MovimientoCodigoTipoTransporte', Cilindraje, MovimientoParqueo.ParqueoNumero, HoraEntrada, 
	HoraSalida, TotalPagar, MovimientoParqueo.FechaCreacion
	from par.Capacidad
		inner join par.Parqueo on Capacidad.CodigoParqueo = Parqueo.Codigo
		inner join par.TipoTransporte on CodigoTipoTransporte = TipoTransporte.Codigo
		inner join @tbl_contador t on numero <= Capacidad
		left join par.MovimientoParqueo on Capacidad.CodigoParqueo = MovimientoParqueo.CodigoParqueo 
			and ParqueoNumero = numero and TipoTransporte.Codigo = MovimientoParqueo.CodigoTipoTransporte and HoraSalida is null
	where Parqueo.Codigo = @codpar
	order by Tipo, numero

end
GO

	-- exec pp.SpValidarPicoPlaca 2, 'A841265'
create or alter procedure pp.SpValidarPicoPlaca
	@tipo_vehiculo int = 0, -- codtipt
	@placa varchar(25) = ''
as
begin
	set datefirst 1
	declare @Codigo int = 1,
	@codpp int = 0, @fecha_actual datetime = getdate(), @dia_semana int = 0
	select @dia_semana = datepart(dw, @fecha_actual)
	declare @tbl_dias as table (dia_numero int, dia_nombre varchar(15)) 
	insert into @tbl_dias (dia_numero, dia_nombre) 
	values (1, 'lunes'), (2, 'martes'), (3, 'miercoles'), (4, 'jueves'), (5, 'viernes'), (6, 'sabado'), (7, 'domingo')

	select @codpp = Codigo from pp.PicoPlaca where YEAR(GETDATE()) = Anio

	select Codigo, CodigoPicoPlaca, CodigoTipoTransporte, Mes, HoraInicio, HoraFin, 
	DiaSemana, dia_nombre 'DiaNombre', Digito, DigitoInicioFinal, salida 'Salida', tipo 'Tipo'
	from (
		--Devuelve >= 1 rows cuando puede salir el vehiculo @tipo_vehiculo con placas @placa
		select Codigo, CodigoPicoPlaca, CodigoTipoTransporte, Mes, HoraInicio, HoraFin, DiaSemana, Digito, DigitoInicioFinal,
		'Puede salir el vehiculo este dia y hora' 'salida', 0 tipo
		from pp.DetallePicoPlaca 
		where CodigoPicoPlaca = @codpp and MONTH(getdate()) = Mes
		and CodigoTipoTransporte = @tipo_vehiculo
		and CAST(@fecha_actual as time) between HoraInicio and HoraFin
		and Digito in (
			case 
				when DigitoInicioFinal = 'I' then SUBSTRING(@placa, 1, 1) 
				when DigitoInicioFinal = 'F' then SUBSTRING(@placa, len(@placa), len(@placa)-1) 
				else '0' 
			end
		)
		and DiaSemana in (@dia_semana)
	
			union all

		--Dias que puede salir
		select Codigo, CodigoPicoPlaca, CodigoTipoTransporte, Mes, HoraInicio, HoraFin, DiaSemana, Digito, DigitoInicioFinal,
		'Dias y horas que puede salir el vehiculo' 'salida', 1 tipo
		from pp.DetallePicoPlaca 
		where CodigoPicoPlaca = @codpp and MONTH(getdate()) = Mes
		and CodigoTipoTransporte = @tipo_vehiculo
		and Digito in (
			case 
				when DigitoInicioFinal = 'I' then SUBSTRING(@placa, 1, 1) 
				when DigitoInicioFinal = 'F' then SUBSTRING(@placa, len(@placa), len(@placa)-1) 
				else '0' 
			end
		)
	) t
	inner join @tbl_dias on dia_numero = DiaSemana
end