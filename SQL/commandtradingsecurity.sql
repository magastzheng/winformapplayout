use tradingsystem

if object_id('commandtrading') is not null
drop table commandtrading

create table commandtrading(
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

if object_id('commandtradingsecurity') is not null
drop table commandtradingsecurity

create table commandtradingsecurity(
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
	,EntrustStatus		int			 --1 - 未执行
)


--====================================
--commandtrading table
--====================================
go
if exists (select name from sysobjects where name='procCommandTradingInsert')
drop proc procCommandTradingInsert

go
create proc procCommandTradingInsert(
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
	insert into commandtrading(
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
if exists (select name from sysobjects where name='procCommandTradingUpdateStatus')
drop proc procCommandTradingUpdateStatus

go
create proc procCommandTradingUpdateStatus(
	@CommandId			int
	,@EntrustStatus		int
	,@DealStatus		int	
)
as
begin
	update commandtrading
	set			
		EntrustStatus		= @EntrustStatus
		,DealStatus			= @DealStatus
	where CommandId=@CommandId
end
--====================================
--commandtradingsecurity table
--====================================