use tradingsystem

--==stock/benchmark/futures begin====

--++stock begin++++
go
if exists (select name from sysobjects where name='procSecurityInfoInsert')
drop proc procSecurityInfoInsert

go
create proc procSecurityInfoInsert(
	@SecuCode		varchar(10),
	@SecuName		varchar(50),
	@ExchangeCode	varchar(10),
	@SecuType		int,
	@ListDate		varchar(10)
)
as
begin
	insert into securityinfo(
		SecuCode
		,SecuName
		,ExchangeCode
		,SecuType
		,ListDate	
	)
	values(
		@SecuCode
		,@SecuName
		,@ExchangeCode
		,@SecuType
		,@ListDate	
	)
end

go
if exists (select name from sysobjects where name='procSecurityInfoUpdate')
drop proc procSecurityInfoUpdate

go
create proc procSecurityInfoUpdate(
	@SecuCode		varchar(10),
	@SecuName		varchar(50),
	@ExchangeCode	varchar(10),
	@SecuType		int
)
as
begin
	update securityinfo
	set SecuName = @SecuName
		,ExchangeCode = @ExchangeCode
	where SecuCode = @SecuCode

	if @SecuType is not null
	begin
		update securityinfo
		set SecuType = @SecuType
		where SecuCode = @SecuCode
	end
end

go
if exists (select name from sysobjects where name='procSecurityInfoDelete')
drop proc procSecurityInfoDelete

go
create proc procSecurityInfoDelete(
	@SecuCode varchar(10),
	@SecuType int = 2 -- default remove the stock
)
as
begin
	delete from securityinfo where SecuCode=@SecuCode and SecuType = @SecuType
end

go
if exists (select name from sysobjects where name='procSecurityInfoSelect')
drop proc procSecurityInfoSelect

go
create proc procSecurityInfoSelect(
	@SecuCode varchar(10) = NULL,
	@SecuType int = 2
)
as
begin
	if @SecuCode is not null and len(@SecuCode) > 0
	begin
		if @SecuType is not null
		begin
			select SecuCode
				,SecuName
				,ExchangeCode
				,SecuType
				,ListDate
				,DeListDate 
			from securityinfo
			where SecuCode = @SecuCode and SecuType = @SecuType
		end
		else
		begin
			select SecuCode
				,SecuName
				,ExchangeCode
				,SecuType
				,ListDate
				,DeListDate 
			from securityinfo
			where SecuCode = @SecuCode
		end
	end
	else
	begin
		if @SecuType is not null
		begin
			select SecuCode
				,SecuName
				,ExchangeCode
				,SecuType
				,ListDate
				,DeListDate 
			from securityinfo
		end
		else
		begin
			select SecuCode
				,SecuName
				,ExchangeCode
				,SecuType
				,ListDate
				,DeListDate 
			from securityinfo
			where SecuType = @SecuType
		end
	end
end
--++stock end++++

--++benchmark begin++++
go
if exists (select name from sysobjects where name='procBenchmarkInsert')
drop proc procBenchmarkInsert

go
create proc procBenchmarkInsert(
	@BenchmarkId varchar(10),
	@BenchmarkName varchar(50),
	@Exchange varchar(10),
	@ContractMultiple int
)
as
begin
	insert into benchmark(
		BenchmarkId		
		,BenchmarkName	
		,Exchange		
		,ContractMultiple
	)
	values(
		@BenchmarkId		
		,@BenchmarkName	
		,@Exchange		
		,@ContractMultiple
	)
end

go
if exists (select name from sysobjects where name='procBenchmarkUpdate')
drop proc procBenchmarkUpdate

go
create proc procBenchmarkUpdate(
	@BenchmarkId varchar(10),
	@BenchmarkName varchar(50),
	@Exchange varchar(10),
	@ContractMultiple int
)
as
begin
	update benchmark
	set	BenchmarkName		= @BenchmarkName
		,Exchange			= @Exchange
		,ContractMultiple	= @ContractMultiple
	where BenchmarkId=@BenchmarkId
end

go
if exists (select name from sysobjects where name='procBenchmarkDelete')
drop proc procBenchmarkDelete

go
create proc procBenchmarkDelete(
	@BenchmarkId varchar(10)
)
as
begin
	delete from benchmark
	where BenchmarkId=@BenchmarkId
end

go
if exists (select name from sysobjects where name='procBenchmarkSelect')
drop proc procBenchmarkSelect

go
create proc procBenchmarkSelect(
	@BenchmarkId varchar(10) = NULL
)
as
begin
	if @BenchmarkId is not null and len(@BenchmarkId) > 0
	begin
		select
			BenchmarkId
			,BenchmarkName
			,Exchange
			,ContractMultiple
		from benchmark
		where BenchmarkId=@BenchmarkId
	end
	else
	begin
		select
			BenchmarkId
			,BenchmarkName
			,Exchange
			,ContractMultiple
		from benchmark
	end
end
--++benchmark end++++

--++futurescontract begin++++
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
--++futurescontract end++++

--==stock/benchmark/futures end====