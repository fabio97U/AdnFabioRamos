create database adn_ceiba;
go
use adn_ceiba
go
create schema pp;--pico_placa
go
create schema par;--parqueo
go

-- drop table par.tipt_tipo_transporte
create table par.tipt_tipo_transporte (
	tipt_codigo int primary key identity(1, 1),
	tipt_tipo varchar(50) not null,
	tipt_descripcion varchar(75) not null,
	tipt_fecha_creacion datetime default getdate()
)
-- select * from par.tipt_tipo_transporte
insert into par.tipt_tipo_transporte (tipt_tipo, tipt_descripcion)
values ('Moto', 'Toda tipo de moto'), ('Carro', 'Toda tipo de carro')

-- drop table pp.pp_pico_placa
create table pp.pp_pico_placa (
	pp_codigo int primary key identity(1, 1),
	pp_anio int not null,
	pp_descripcion varchar(255) not null, 
	pp_fecha_creacion datetime default getdate()
)
-- select * from pp.pp_pico_placa
insert into pp.pp_pico_placa (pp_anio, pp_descripcion)
values (2021, 'Pico placa para el año 2021 en Colombia');

-- drop table pp.dpp_detalle_pico_placa
create table pp.dpp_detalle_pico_placa (
	dpp_codigo int primary key identity(1, 1),
	dpp_codpp int foreign key references pp.pp_pico_placa not null,
	dpp_codtipt int foreign key references par.tipt_tipo_transporte not null,
	dpp_mes tinyint not null,
	dpp_hora_inicio varchar(5) not null,
	dpp_hora_fin varchar(5) not null,

	dpp_dia_semana int not null check (dpp_dia_semana >= 1 and dpp_dia_semana <= 7),

	dpp_digito smallint not null default 1 check (dpp_digito >= 0 and dpp_digito <= 9), 

	dpp_digito_inicio_final varchar(2) not null default 'I' check (dpp_digito_inicio_final='I' or dpp_digito_inicio_final='F'),

	dpp_fecha_creacion datetime default getdate()
)
-- select * from pp.dpp_detalle_pico_placa
insert into pp.dpp_detalle_pico_placa (dpp_codpp, dpp_codtipt, dpp_mes, dpp_hora_inicio, dpp_hora_fin, dpp_digito, dpp_dia_semana)
values (1, 2, 10, '00:00', '23:59', 6, 5), (1, 2, 10, '00:00', '23:59', 6, 4), (1, 2, 10, '00:00', '23:59', 8, 3), (1, 2, 10, '00:00', '23:59', 9, 4)
insert into pp.dpp_detalle_pico_placa (dpp_codpp, dpp_codtipt, dpp_mes, dpp_hora_inicio, dpp_hora_fin, dpp_digito, dpp_dia_semana)
values (1, 1, 10, '00:00', '23:59', 4, 1), (1, 1, 10, '00:00', '23:59', 5, 7)

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1: Enero ... 12: Diciembre' , 
	@level0type=N'SCHEMA',@level0name=N'pp', @level1type=N'TABLE',@level1name=N'dpp_detalle_pico_placa', 
	@level2type=N'COLUMN',@level2name=N'dpp_mes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Que la placa I: Inicie, F: Finalice' , 
	@level0type=N'SCHEMA',@level0name=N'pp', @level1type=N'TABLE',@level1name=N'dpp_detalle_pico_placa', 
	@level2type=N'COLUMN',@level2name=N'dpp_digito_inicio_final'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1: Lunes ... 7: Domingo' , 
	@level0type=N'SCHEMA',@level0name=N'pp', @level1type=N'TABLE',@level1name=N'dpp_detalle_pico_placa', 
	@level2type=N'COLUMN',@level2name=N'dpp_dia_semana'
GO
create index index_detalle_pico_placa on pp.dpp_detalle_pico_placa (dpp_digito, dpp_dia_semana, dpp_hora_inicio, dpp_hora_fin);
go

-- drop table par.par_parqueo
create table par.par_parqueo (
	par_codigo int primary key identity(1, 1),
	par_nombre varchar(50) not null,
	par_direccion varchar(255) not null,
	par_fecha_creacion datetime default getdate()
)
-- select * from par.par_parqueo
insert into par.par_parqueo (par_nombre, par_direccion)
values ('Parqueo Ceiba', 'Cl. 8B ##65-191, Medellín, Antioquia, Colombia')

-- drop table par.cap_capacidad
create table par.cap_capacidad (
	cap_codigo int primary key identity(1, 1),
	cap_codpar int foreign key references par.par_parqueo not null,
	cap_codtipt int foreign key references par.tipt_tipo_transporte not null,
	cap_capacidad smallint not null,
	cap_valor_hora float not null,
	cap_valor_dia float not null,
	cap_fecha_creacion datetime default getdate()
)
-- select * from par.cap_capacidad
insert into par.cap_capacidad (cap_codpar, cap_codtipt, cap_capacidad, cap_valor_hora, cap_valor_dia)
values (1, 1, 10, 500, 4000), (1, 2, 20, 1000, 8000)

-- drop table par.movp_movimiento_parqueo
create table par.movp_movimiento_parqueo (
	movp_codigo uniqueidentifier primary key default newid(),
	movp_codpar int foreign key references par.par_parqueo not null,
	movp_placa varchar(50) not null,
	movp_codtipt int foreign key references par.tipt_tipo_transporte not null,
	movp_cilindraje int null,
	movp_parqueo_numero smallint not null, 

	movp_hora_entrada datetime default getdate(),
	movp_hora_salida datetime null,
	movp_total_pagar float null default 0.0,

	movp_fecha_creacion datetime default getdate()
)
-- select * from par.movp_movimiento_parqueo
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Las motos con un cilindraje mayor a 500cc tienen un sobrecargo de $2000' , 
	@level0type=N'SCHEMA',@level0name=N'par', @level1type=N'TABLE',@level1name=N'movp_movimiento_parqueo', 
	@level2type=N'COLUMN',@level2name=N'movp_cilindraje'
GO

create index index_busqueda_placa on par.movp_movimiento_parqueo (movp_codpar, movp_placa, movp_codtipt);
create index index_entrada_salida on par.movp_movimiento_parqueo (movp_hora_entrada, movp_hora_salida);
go

	-- exec par.sp_movimientos_parqueo 1
create or alter procedure par.sp_movimientos_parqueo
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

	select par_codigo, par_nombre, tipt_codigo, tipt_tipo, cap_capacidad, cap_valor_hora, cap_valor_dia, numero, 

	movp_codigo, movp_codpar, movp_placa, movp_codtipt, movp_cilindraje, movp_parqueo_numero, movp_hora_entrada, 
	movp_hora_salida, movp_total_pagar, movp_fecha_creacion
	from par.cap_capacidad
		inner join par.par_parqueo on cap_codpar = par_codigo
		inner join par.tipt_tipo_transporte on cap_codtipt = tipt_codigo
		inner join @tbl_contador on numero <= cap_capacidad
		left join par.movp_movimiento_parqueo on cap_codpar = movp_codpar and movp_parqueo_numero = numero and tipt_codigo = movp_codtipt
			and movp_hora_salida is null
	where par_codigo = @codpar
	order by tipt_tipo, numero

end
GO

	-- exec pp.sp_validar_pico_placa 2, '841265'
create or alter procedure pp.sp_validar_pico_placa
	@tipo_vehiculo int = 0, -- codtipt
	@placa varchar(25) = ''
as
begin
	set datefirst 1
	declare @tipt_codigo int = 1,
	@codpp int = 0, @fecha_actual datetime = getdate(), @dia_semana int = 0
	select @dia_semana = datepart(dw, @fecha_actual)
	declare @tbl_dias as table (dia_numero int, dia_nombre varchar(15)) 
	insert into @tbl_dias (dia_numero, dia_nombre) 
	values (1, 'lunes'), (2, 'martes'), (3, 'miercoles'), (4, 'jueves'), (5, 'viernes'), (6, 'sabado'), (7, 'domingo')

	select @codpp = pp_codigo from pp.pp_pico_placa where YEAR(GETDATE()) = pp_anio

	select dpp_codigo, dpp_codpp, dpp_codtipt, dpp_mes, dpp_hora_inicio, dpp_hora_fin, 
	dpp_dia_semana, dia_nombre, dpp_digito, dpp_digito_inicio_final, salida, tipo 
	from (
		--Devuelve >= 1 rows cuando puede salir el vehiculo @tipo_vehiculo con placas @placa
		select dpp_codigo, dpp_codpp, dpp_codtipt, dpp_mes, dpp_hora_inicio, dpp_hora_fin, dpp_dia_semana, dpp_digito, dpp_digito_inicio_final,
		'Puede salir el vehiculo este dia y hora' 'salida', 0 tipo
		from pp.dpp_detalle_pico_placa 
		where dpp_codpp = @codpp and MONTH(getdate()) = dpp_mes
		and dpp_codtipt = @tipo_vehiculo
		and CAST(@fecha_actual as time) between dpp_hora_inicio and dpp_hora_fin
		and dpp_digito in (
			case 
				when dpp_digito_inicio_final = 'I' then SUBSTRING(@placa, 1, 1) 
				when dpp_digito_inicio_final = 'F' then SUBSTRING(@placa, len(@placa), len(@placa)-1) 
				else '0' 
			end
		)
		and dpp_dia_semana in (@dia_semana)
	
			union all

		--Dias que puede salir
		select dpp_codigo, dpp_codpp, dpp_codtipt, dpp_mes, dpp_hora_inicio, dpp_hora_fin, dpp_dia_semana, dpp_digito, dpp_digito_inicio_final,
		'Dias y horas que puede salir el vehiculo' 'salida', 1 tipo
		from pp.dpp_detalle_pico_placa 
		where dpp_codpp = @codpp and MONTH(getdate()) = dpp_mes
		and dpp_codtipt = @tipo_vehiculo
		and dpp_digito in (
			case 
				when dpp_digito_inicio_final = 'I' then SUBSTRING(@placa, 1, 1) 
				when dpp_digito_inicio_final = 'F' then SUBSTRING(@placa, len(@placa), len(@placa)-1) 
				else '0' 
			end
		)
	) t
	inner join @tbl_dias on dia_numero = dpp_dia_semana
end