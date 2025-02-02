create database EventoDB
go

use EventoDB

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
	[NombreComprador] [nvarchar](300) not null,
	[FechaCompra] [datetime] null,
	[Vendido] [bit] null,
	[Canjeado] [bit] null,
	[IdEvento] [int]
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