use tradingsystem

if object_id('tradingcommand') is not null
drop table tradingcommand

create table tradingcommand(
	CommandId			int identity(1, 1) primary key
	,InstanceId			int not null
	,ModifiedTimes		int
	,CommandType		int -- 1 - 期现套利
	,ExecuteType		int -- 1 开仓， 2 - 平仓
	,StockDirection		int --10 -- 买入现货，11--卖出现货
	,FuturesDirection	int --12-卖出开仓，13 -买入平仓
	,EntrustStatus		int	-- 1 - 未执行， 2 - 部分执行， 3- 已完成
	,DealStatus			int		-- 1 - 未成交， 2 - 部分成交， 3 - 已完成
	,StartDate			datetime
	,EndDate			datetime
)

if object_id('tradingcommandsecurity') is not null
drop table tradingcommandsecurity

create table tradingcommandsecurity(
	CommandId			int not null
	,SecuCode			varchar(10) not null
	,SecuType			int
	,CommandAmount		int
	,EntrustedAmount	int
	,CommandPrice		numeric(20, 4) --如果不限价，则价格设置为0
	,EntrustStatus		int			 --1 - 未执行
)

if object_id('entrustsecurity') is not null
drop table entrustsecurity

create table entrustsecurity(
	CommandId			int not null
	,SecuCode			varchar(10) not null
	,SecuType			int
	,EntrustedAmount	int
	,EntrustPrice		numeric(20, 4) 
	,EntrustDirection	int			 --10 - 买入股票， 11 - 卖出股票， 12 - 卖出开仓， 13 - 买入平仓
	,EntrustStatus		int			 -- 1 - 未执行， 2 - 部分执行， 3- 已完成
)


--====================================
--tradingcommand table
--====================================
go
if exists (select name from sysobjects where name='procTradingCommandInsert')
drop proc procTradingCommandInsert

go
create proc procTradingCommandInsert(
	@InstanceId			int	
	,@CommandType		int	
	,@ExecuteType		int	
	,@StockDirection	int	
	,@FuturesDirection	int
	,@EntrustStatus		int
	,@DealStatus		int	
	,@StartDate			datetime
	,@EndDate			datetime
)
as
begin
	declare @newid int
	insert into tradingcommand(
		InstanceId			
		,ModifiedTimes		
		,CommandType		
		,ExecuteType		
		,StockDirection		
		,FuturesDirection	
		,EntrustStatus		
		,DealStatus			
		,StartDate			
		,EndDate			
	)
	values(
		@InstanceId			
		,1		
		,@CommandType		
		,@ExecuteType		
		,@StockDirection	
		,@FuturesDirection	
		,@EntrustStatus		
		,@DealStatus		
		,@StartDate			
		,@EndDate
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end

go
if exists (select name from sysobjects where name='procTradingCommandUpdateStatus')
drop proc procTradingCommandUpdateStatus

go
create proc procTradingCommandUpdateStatus(
	@CommandId			int
	,@EntrustStatus		int
	,@DealStatus		int	
)
as
begin
	update tradingcommand
	set			
		EntrustStatus		= @EntrustStatus
		,DealStatus			= @DealStatus
	where CommandId=@CommandId
end

go
if exists (select name from sysobjects where name='procTradingCommandDelete')
drop proc procTradingCommandDelete

go
create proc procTradingCommandDelete(
	@CommandId			int
)
as
begin
	delete from tradingcommand where CommandId=@CommandId
end

go
if exists (select name from sysobjects where name='procTradingCommandSelect')
drop proc procTradingCommandSelect

go
create proc procTradingCommandSelect(
	@CommandId			int
)
as
begin
	if @CommandId is not null and @CommandId > 0
	begin
		select 
			CommandId			
			,InstanceId			
			,ModifiedTimes		
			,CommandType		
			,ExecuteType		
			,StockDirection		
			,FuturesDirection	
			,EntrustStatus		
			,DealStatus			
			,StartDate			
			,EndDate			
		from tradingcommand
		where CommandId=@CommandId
	end
	else
	begin
		select 
			CommandId			
			,InstanceId			
			,ModifiedTimes		
			,CommandType		
			,ExecuteType		
			,StockDirection		
			,FuturesDirection	
			,EntrustStatus		
			,DealStatus			
			,StartDate			
			,EndDate			
		from tradingcommand
	end
end
--====================================
--tradingcommandsecurity table
--====================================
go
if exists (select name from sysobjects where name='procTradingCommandSecurityInsert')
drop proc procTradingCommandSecurityInsert

go
create proc procTradingCommandSecurityInsert(
	@CommandId			int 
	,@SecuCode			varchar(10) 
	,@SecuType			int
	,@CommandAmount		int
	,@CommandPrice		numeric(20, 4) --如果不限价，则价格设置为0
)
as
begin
	insert into tradingcommandsecurity(
		CommandId			
		,SecuCode			
		,SecuType			
		,CommandAmount		
		,EntrustedAmount	
		,CommandPrice		
		,EntrustStatus		
	)values(
		@CommandId			
		,@SecuCode			
		,@SecuType			
		,@CommandAmount		
		,0	
		,@CommandPrice		
		,1		
	)
end

go
if exists (select name from sysobjects where name='procTradingCommandSecurityUpdateEntrustAmount')
drop proc procTradingCommandSecurityUpdateEntrustAmount

go
create proc procTradingCommandSecurityUpdateEntrustAmount(
	@CommandId			int 
	,@SecuCode			varchar(10) 
	,@EntrustedAmount	int
)
as
begin
	update tradingcommandsecurity
	set EntrustedAmount=@EntrustedAmount,
		EntrustStatus=
		case 
			when CommandAmount=@EntrustedAmount then 3 -- 已完成
			else 2 -- 部分执行
		end
	where CommandId=@CommandId and SecuCode=@SecuCode
end

go
if exists (select name from sysobjects where name='procTradingCommandSecurityUpdateEntrustDelete')
drop proc procTradingCommandSecurityUpdateEntrustDelete

go
create proc procTradingCommandSecurityUpdateEntrustDelete(
	@CommandId			int 
	,@SecuCode			varchar(10) 
)
as
begin
	delete from tradingcommandsecurity where CommandId=@CommandId and SecuCode=@SecuCode
end

go
if exists (select name from sysobjects where name='procTradingCommandSecuritySelect')
drop proc procTradingCommandSecuritySelect

go
create proc procTradingCommandSecuritySelect(
	@CommandId	int 
)
as
begin
	select
		CommandId		
		,SecuCode		
		,SecuType		
		,CommandAmount	
		,EntrustedAmount
		,CommandPrice	
		,EntrustStatus	
	from tradingcommandsecurity 
	where CommandId=@CommandId
end
--====================================
--entrustsecurity table
--====================================
go
if exists (select name from sysobjects where name='procEntrustSecurityInsert')
drop proc procEntrustSecurityInsert

go
create proc procEntrustSecurityInsert(
	@CommandId			int
	,@SecuCode			varchar(10)
	,@SecuType			int
	,@EntrustedAmount	int
	,@EntrustPrice		numeric(20, 4)
	,@EntrustDirection	int
	,@EntrustStatus		int
)
as
begin
	insert into entrustsecurity(
		CommandId			
		,SecuCode			
		,SecuType			
		,EntrustedAmount	
		,EntrustPrice		
		,EntrustDirection	
		,EntrustStatus
	)values(
		@CommandId			
		,@SecuCode			
		,@SecuType			
		,@EntrustedAmount	
		,@EntrustPrice		
		,@EntrustDirection	
		,@EntrustStatus	
	)		
end

go
if exists (select name from sysobjects where name='procEntrustSecurityUpdate')
drop proc procEntrustSecurityUpdate

go
create proc procEntrustSecurityUpdate(
	@CommandId			int
	,@SecuCode			varchar(10)
	,@EntrustedAmount	int
	,@EntrustPrice		numeric(20, 4)
	,@EntrustDirection	int
	,@EntrustStatus		int
)
as
begin
	update entrustsecurity
	set EntrustedAmount	= @EntrustedAmount
		,EntrustPrice		= @EntrustPrice
		,EntrustDirection	= @EntrustDirection
		,EntrustStatus		= @EntrustStatus
	where CommandId=@CommandId and SecuCode=@SecuCode
end

go
if exists (select name from sysobjects where name='procEntrustSecurityDelete')
drop proc procEntrustSecurityDelete

go
create proc procEntrustSecurityDelete(
	@CommandId			int
	,@SecuCode			varchar(10)
)
as
begin
	delete from entrustsecurity where CommandId=@CommandId and SecuCode=@SecuCode
end

go
if exists (select name from sysobjects where name='procEntrustSecuritySelect')
drop proc procEntrustSecuritySelect

go
create proc procEntrustSecuritySelect(
	@CommandId			int
)
as
begin
	select CommandId			
		,SecuCode			
		,SecuType			
		,EntrustedAmount	
		,EntrustPrice		
		,EntrustDirection	
		,EntrustStatus
	from entrustsecurity
	where CommandId = @CommandId
end