create database EventosDB
go

use EventosDB

go

/****** Object:  Table [dbo].[Evento]   Script Date: 02/02/2025 01:36:10 p. m.  ******/
if  exists (select * from sys.objects where object_id = object_id('[dbo].[Evento]') and type in ('u'))
drop table [dbo].[Evento]
go
/****** object:  table [dbo].[Evento]   script date: 02/02/2025 01:36:10 p. m. ******/
set ansi_nulls on
go

set quoted_identifier on
go

create table [dbo].[Evento](
	[IdEvento] [int] identity(1,1) not null,
	[Nombre] [nvarchar](300) not null,
	[FechaInicio] [datetime] not null,
	[FechaFin] [datetime] not null,
	[NoBoletos] [int] null,
 constraint [Pk_IdEvento] primary key clustered 
(
	[IdEvento] asc
)with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on, optimize_for_sequential_key = off) on [PRIMARY]
) on [PRIMARY]
go




/****** Object:  Table [dbo].[Boleto]   Script Date: 02/02/2025 01:36:10 p. m.  ******/
if  exists (select * from sys.objects where object_id = object_id('[dbo].[Boleto]') and type in ('u'))
drop table [dbo].[Boleto]
go
/****** object:  table [dbo].[Boleto]   script date: 02/02/2025 01:36:10 p. m. ******/
set ansi_nulls on
go

set quoted_identifier on
go

create table [dbo].[Boleto](
	[IdBoleto] [int] identity(1,1) not null,
	[NombreComprador] [nvarchar](300) null,
	[FechaCompra] [datetime] null,
	[Vendido] [bit] null,
	[Canjeado] [bit] null,
	[IdEvento] [int] not null
 constraint [Pk_IdBoleto] primary key clustered 
(
	[IdBoleto] asc
)with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on, optimize_for_sequential_key = off) on [PRIMARY]
) on [PRIMARY]
go

alter table [dbo].[Boleto]  with check add  constraint [FkIdEvento] foreign key([IdEvento])
references [dbo].[Evento] ([IdEvento])
go

alter table [dbo].[Boleto] add  constraint [DfBoletoVendido]  default ((0)) for [Vendido]
go

alter table [dbo].[Boleto] add  constraint [DfBoletoCanjeado]  default ((0)) for [Canjeado]
go

/****** Object:  StoredProcedure [dbo].[SpeEventoInsertar]   Script Date: 02/01/2025 02:07:15 p. m. ******/
drop procedure [dbo].[SpeEventoObtener]
go

set ansi_nulls on
go

set quoted_identifier on
go


create procedure [dbo].[SpeEventoObtener]
	-- Add the parameters for the stored procedure here
	@IdEvento			as int = null
	
as
begin

	declare @trancount bit = 0

	begin try

		
		select
			Ev.IdEvento,
			Ev.Nombre, 
			Ev.FechaInicio,
			Ev.FechaFin,
			Ev.NoBoletos,
			[NoBoletosDisponibles] = (select count(*) from [dbo].[Boleto] Bol where Bol.Vendido=0),
			[NoBoletosVendidos] = (select count(*) from [dbo].[Boleto] Bol where Bol.Vendido=1),
			[NoBoletosCanjeados] = (select count(*) from [dbo].[Boleto] Bol where Bol.Canjeado=1)
		from
			[dbo].[Evento] Ev
		where
			(@IdEvento is null or (@IdEvento is not null and Ev.IdEvento = @IdEvento))
				
		
	end try
	begin catch

      if @@trancount > 0  and @trancount = 1 begin
		print 'rollback global'
         rollback
	  end

	end catch

end
go


/****** Object:  StoredProcedure [dbo].[SpeEventoInsertar]   Script Date: 02/01/2025 02:07:15 p. m. ******/
drop procedure [dbo].[SpeEventoInsertar]
go

/****** Object:  StoredProcedure [dbo].[SpeEventoInsertar]   Script Date: 02/01/2025 02:07:15 p. m. ******/
set ansi_nulls on
go

set quoted_identifier on
go

create procedure [dbo].[SpeEventoInsertar]
	-- Add the parameters for the stored procedure here
	@OutErrorNumber		int out,
	@OutErrorMessage	nvarchar(max) out,

	@Nombre				as nvarchar(300),
	@FechaInicio		as datetime,
	@FechaFin			as datetime,
	@NoBoletos			as int

as
begin

	declare @trancount bit = 0
	declare @contador int = 1
	declare @IdEvento int = 0

	begin try

		if @@trancount = 0
		begin
			print 'inicia transaccion'
			begin transaction
			set @trancount = 1;
		end	


		insert into [dbo].[Evento]
				   (
					Nombre, 
					FechaInicio,
					FechaFin,
					NoBoletos
				   )
		values
				   (
					@Nombre,
					@FechaInicio,
					@FechaFin,
					@NoBoletos
				    )
		
		set @IdEvento = SCOPE_IDENTITY()

		while (@contador<@NoBoletos)
		begin
			insert into [dbo].[Boleto]
				   (
					IdEvento
				   )
				values
				   (
					@IdEvento
				    )
			set @contador = @contador + 1
		end

		set @OutErrorNumber = 0;
		set @OutErrorMessage = '';
		
		if @@trancount > 0  and @trancount = 1 begin
			print 'commit'
			commit
		end

	end try
	begin catch

      if @@trancount > 0  and @trancount = 1 begin
		print 'rollback global'
         rollback
	  end

		set @OutErrorNumber = error_number();
		set @OutErrorMessage = error_message();

	end catch

end
go



/****** Object:  StoredProcedure [dbo].[SpeEventoActualizar]    Script Date: 02/01/2025 02:07:15 p. m. ******/
drop procedure [dbo].[SpeEventoActualizar] 
go


/****** Object:  StoredProcedure [dbo].[SpeEventoActualizar]    Script Date: 04/02/2025 03:13:40 p. m. ******/
set ansi_nulls on
go
set quoted_identifier on
go

create procedure [dbo].[SpeEventoActualizar]
	-- Add the parameters for the stored procedure here
	@OutErrorNumber		int out,
	@OutErrorMessage	nvarchar(max) out,

	@IdEvento			as int,
	@Nombre				as nvarchar(300),
	@FechaInicio		as datetime,
	@FechaFin			as datetime,
	@NoBoletos			as int,
	@Nofilas			as int,
	@Insertar			as bit

as
begin

	declare @trancount bit = 0
	declare @contador int = 1

	begin try

		if @@trancount = 0
		begin
			print 'inicia transaccion'
			begin transaction
			set @trancount = 1;
		end	


		update [dbo].[Evento]
			set
				Nombre = @Nombre, 
				FechaInicio = @FechaInicio,
				FechaFin = @FechaFin,
				NoBoletos = @NoBoletos
		where
			IdEvento = @IdEvento
		
		if (@Insertar = 1)
		begin
			while (@contador<=@Nofilas)
			begin
				insert into [dbo].[Boleto]
					   (
						IdEvento
					   )
					values
					   (
						@IdEvento
						)
				set @contador = @contador + 1
			end
		end

		else
		begin
			while (@contador<@Nofilas)
			begin

				delete from 
					[dbo].[Boleto] 
				where 
					IdBoleto=(select max(IdBoleto) from [dbo].[Boleto])
					and IdEvento = @IdEvento
					and Vendido = 0
					and Canjeado = 0

				set @contador = @contador + 1
			end
		end


		set @OutErrorNumber = 0;
		set @OutErrorMessage = '';
		
		if @@trancount > 0  and @trancount = 1 begin
			print 'commit'
			commit
		end

	end try
	begin catch

      if @@trancount > 0  and @trancount = 1 begin
		print 'rollback global'
         rollback
	  end

		set @OutErrorNumber = error_number();
		set @OutErrorMessage = error_message();

	end catch

end



/****** Object:  StoredProcedure [dbo].[SpeEventoEliminar]  Script Date: 02/01/2025 02:07:15 p. m. ******/
drop procedure [dbo].[SpeEventoEliminar]
go

/****** Object:  StoredProcedure [dbo].[SpeEventoEliminar]   Script Date: 02/01/2025 02:07:15 p. m. ******/
set ansi_nulls on
go

set quoted_identifier on
go

create procedure [dbo].[SpeEventoEliminar]
	-- Add the parameters for the stored procedure here
	@OutErrorNumber		int out,
	@OutErrorMessage	nvarchar(max) out,

	@IdEvento				as int

as
begin

	declare @trancount bit = 0
	
	begin try

		if @@trancount = 0
		begin
			print 'inicia transaccion'
			begin transaction
			set @trancount = 1;
		end	


		delete 
		from 
			[dbo].[Boleto]
		where
			IdEvento = @IdEvento

		delete 
		from 
			[dbo].[Evento]
		where
			IdEvento = @IdEvento

		
		set @OutErrorNumber = 0;
		set @OutErrorMessage = '';
		
		if @@trancount > 0  and @trancount = 1 begin
			print 'commit'
			commit
		end

	end try
	begin catch

      if @@trancount > 0  and @trancount = 1 begin
		print 'rollback global'
         rollback
	  end

		set @OutErrorNumber = error_number();
		set @OutErrorMessage = error_message();

	end catch

end
go




/****** Object:  StoredProcedure [dbo].[SpeBoletoObtener]   Script Date: 02/01/2025 02:07:15 p. m. ******/
drop procedure [dbo].[SpeBoletoObtener]
go

set ansi_nulls on
go

set quoted_identifier on
go


create procedure [dbo].[SpeBoletoObtener]
	-- Add the parameters for the stored procedure here
	@IdBoleto			as int = null
	
as
begin

	declare @trancount bit = 0

	begin try

		
		select
			Bo.IdBoleto,
			Bo.NombreComprador, 
			Bo.FechaCompra,
			Bo.Vendido,
			Bo.Canjeado,
			Bo.IdEvento
		from
			[dbo].[Boleto] Bo
		where
			(@IdBoleto is null or (@IdBoleto is not null and Bo.IdBoleto = @IdBoleto))
				
		
	end try
	begin catch

      if @@trancount > 0  and @trancount = 1 begin
		print 'rollback global'
         rollback
	  end

	end catch

end
go



/****** Object:  StoredProcedure [dbo].[SpeBoletoActualizar]  Script Date: 02/01/2025 02:07:15 p. m. ******/
drop procedure [dbo].[SpeBoletoActualizar]
go

/****** Object:  StoredProcedure [dbo].[SpeBoletoActualizar]    Script Date: 04/02/2025 09:03:24 a. m. ******/
set ansi_nulls on
go
set quoted_identifier on
go

create procedure [dbo].[SpeBoletoActualizar]
	-- Add the parameters for the stored procedure here
	@OutErrorNumber		int out,
	@OutErrorMessage	nvarchar(max) out,

	@IdBoleto			as int,
	@NombreComprador	nvarchar(300),
	@Vendido			bit,
	@Canjeado			bit

as
begin

	declare @trancount bit = 0
	declare @contador int = 0
	
	begin try

		if @@trancount = 0
		begin
			print 'inicia transaccion'
			begin transaction
			set @trancount = 1;
		end	


		update [dbo].[Boleto]
			set
				NombreComprador = @NombreComprador, 
				FechaCompra = getdate(),
				Vendido = @Vendido,
				Canjeado = @Canjeado
		where
			IdBoleto = @IdBoleto
		


		set @OutErrorNumber = 0;
		set @OutErrorMessage = '';
		
		if @@trancount > 0  and @trancount = 1 begin
			print 'commit'
			commit
		end

	end try
	begin catch

      if @@trancount > 0  and @trancount = 1 begin
		print 'rollback global'
         rollback
	  end

		set @OutErrorNumber = error_number();
		set @OutErrorMessage = error_message();

	end catch

end