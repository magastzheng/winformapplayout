use tradingsystem

if object_id('futurescontract') is not null
drop table futurescontract

create table futurescontract(
	Code varchar(10) not null			--合约代码
	,Name varchar(50) not null			--合约名称
	,Exchange varchar(10) not null		--交易所代码
	,PriceLimits numeric(8, 2)			--涨跌幅限制(%)
	,Deposit numeric(8, 2)				--交易保证金(%)
	,ListedDate datetime				--合约上市日
	,LastTradingDay datetime			--最后交易日期
	,LastDeliveryDay datetime			--最后交割日
	,Status int							-- 1 正常交易, -1 过期
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