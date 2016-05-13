use tradingsystem

if object_id('futurescontract') is not null
drop table futurescontract

create table futurescontract(
	Code varchar(10) not null
	,Name varchar(50) not null
	,Exchange varchar(10) not null
	,PriceLimits numeric(8, 2)
	,Deposit numeric(8, 2)
	,ListedDate datetime
	,LastTradingDay datetime
	,LastDeliveryDay datetime
	,Status int   -- 1 正常交易, -1 过期
	,constraint pk_FuturesContract_Id primary key(Code)
)

if exists (select name from sysobjects where name='procFuturesContractInsert')
drop proc procFuturesContractInsert

go

create proc procFuturesContractInsert(
	@Code varchar(10)
	,@Name varchar(50)
	,@Exchange varchar(10)
	,@PriceLimits numeric(8, 2)
	,@Deposit numeric(8, 2)
	,@ListedDate datetime
	,@LastTradingDay datetime
	,@LastDeliveryDay datetime
)
as
begin
	insert into futurescontract(
		Code
		,Name
		,Exchange
		,PriceLimits
		,Deposit
		,ListedDate
		,LastTradingDay
		,LastDeliveryDay
	)
	values
	(
		@Code
		,@Name
		,@Exchange
		,@PriceLimits
		,@Deposit
		,@ListedDate
		,@LastTradingDay
		,@LastDeliveryDay
	)

	--return @Code
end

if exists (select name from sysobjects where name='procFuturesContractSelect')
drop proc procFuturesContractSelect

go

create proc procFuturesContractSelect(
	@Code varchar(10) = NULL
)
as
begin
	declare @today varchar(10)
	set @today = convert(varchar(10), getdate(), 112)
	if @Code is not null
	begin
		select
			Code
			,Name
			,Exchange
			,PriceLimits
			,Deposit
			,ListedDate
			,LastTradingDay
			,LastDeliveryDay
		from futurescontract
		where Code=@Code
	end
	else
	begin
		
		select
			Code
			,Name
			,Exchange
			,PriceLimits
			,Deposit
			,ListedDate
			,LastTradingDay
			,LastDeliveryDay
		from futurescontract
		where convert(varchar, LastTradingDay, 112) >= @today
	end
end