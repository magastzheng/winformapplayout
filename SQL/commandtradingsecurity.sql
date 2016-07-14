use tradingsystem

if object_id('tradingcommand') is not null
drop table tradingcommand

create table tradingcommand(
	CommandId			int identity(1, 1) primary key
	,InstanceId			int not null
	,CommandNum			int	--指令份数
	,TargetNum			int	--目标份数
	--,DealNum			int --成交份数
	,ModifiedTimes		int	--修改次数
	,CommandType		int -- 1 - 期现套利
	,ExecuteType		int -- 1 开仓， 2 - 平仓
	,StockDirection		int --10 -- 买入现货，11--卖出现货
	,FuturesDirection	int --12-卖出开仓，13 -买入平仓
	,EntrustStatus		int	-- 1 - 未执行， 2 - 部分执行， 3- 已完成
	,DealStatus			int	-- 1 - 未成交， 2 - 部分成交， 3 - 已完成
	,SubmitPerson		varchar(10) --下达人
	,CreatedDate		datetime -- 下达指令时间
	,ModifiedDate		datetime -- 修改时间
	,StartDate			datetime
	,EndDate			datetime
)

if object_id('tradingcommandsecurity') is not null
drop table tradingcommandsecurity

create table tradingcommandsecurity(
	CommandId			int not null
	,SecuCode			varchar(10) not null
	,SecuType			int
	,WeightAmount		int
	,CommandAmount		int
	,CommandDirection	int			 --10 - 买入股票， 11 - 卖出股票， 12 - 卖出开仓， 13 - 买入平仓
	,EntrustedAmount	int
	,CommandPrice		numeric(20, 4) --如果不限价，则价格设置为0
	,EntrustStatus		int			 --1 - 未执行
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
	,@CommandNum	int
	,@CommandType		int	
	,@ExecuteType		int	
	,@StockDirection	int	
	,@FuturesDirection	int
	,@EntrustStatus		int
	,@DealStatus		int	
	,@SubmitPerson		varchar(10)
	,@CreatedDate		datetime
	,@StartDate			datetime
	,@EndDate			datetime
)
as
begin
	declare @newid int
	insert into tradingcommand(
		InstanceId	
		,CommandNum	
		,TargetNum	
		,ModifiedTimes		
		,CommandType		
		,ExecuteType		
		,StockDirection		
		,FuturesDirection	
		,EntrustStatus		
		,DealStatus	
		,SubmitPerson
		,CreatedDate	
		,StartDate			
		,EndDate			
	)
	values(
		@InstanceId
		,@CommandNum
		,0			
		,1		
		,@CommandType		
		,@ExecuteType		
		,@StockDirection	
		,@FuturesDirection	
		,@EntrustStatus		
		,@DealStatus
		,@SubmitPerson
		,@CreatedDate	
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
	,@ModifiedDate		datetime
)
as
begin
	update tradingcommand
	set			
		EntrustStatus		= @EntrustStatus
		,DealStatus			= @DealStatus
		,ModifiedDate		= @ModifiedDate
	where CommandId=@CommandId
end

go
if exists (select name from sysobjects where name='procTradingCommandUpdateTargetNum')
drop proc procTradingCommandUpdateTargetNum

go
create proc procTradingCommandUpdateTargetNum(
	@CommandId			int
	,@TargetNum			int
	,@ModifiedDate		datetime
)
as
begin
	update tradingcommand
	set	TargetNum	= @TargetNum
		,ModifiedDate = @ModifiedDate
	where CommandId=@CommandId
end

go
if exists (select name from sysobjects where name='procTradingCommandUpdateTargetNumBySubmitId')
drop proc procTradingCommandUpdateTargetNumBySubmitId

go
create proc procTradingCommandUpdateTargetNumBySubmitId(
	@CommandId			int
	,@SubmitId			int
	,@ModifiedDate		datetime
)
as
begin
	declare @TotalTargetNum int
	declare @ThisTargetNum int
	--
	set @TotalTargetNum=(select TargetNum from tradingcommand where CommandId=@CommandId)
	--从entrustcommand中获取本次委托份数
	set @ThisTargetNum=(select Copies from entrustcommand where SubmitId=@SubmitId and EntrustStatus=12)

	set @TotalTargetNum=@TotalTargetNum-@ThisTargetNum

	if @TotalTargetNum < 0
	begin
		set @TotalTargetNum=0
	end

	update tradingcommand
	set	TargetNum	= @TotalTargetNum
		,ModifiedDate = @ModifiedDate
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
	--TODO:delete the tradingcommandsecurity
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
			,CommandNum
			,TargetNum		
			,ModifiedTimes		
			,CommandType		
			,ExecuteType		
			,StockDirection		
			,FuturesDirection	
			,EntrustStatus		
			,DealStatus	
			,SubmitPerson
			,CreatedDate
			,ModifiedDate		
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
			,CommandNum	
			,TargetNum		
			,ModifiedTimes		
			,CommandType		
			,ExecuteType		
			,StockDirection		
			,FuturesDirection	
			,EntrustStatus		
			,DealStatus	
			,SubmitPerson
			,CreatedDate
			,ModifiedDate			
			,StartDate			
			,EndDate			
		from tradingcommand
	end
end

go
if exists (select name from sysobjects where name='procTradingCommandSelectCombine')
drop proc procTradingCommandSelectCombine

go
create proc procTradingCommandSelectCombine(
	@CommandId			int
)
as
begin
	if @CommandId is not null and @CommandId > 0
	begin
		select 
			a.CommandId			
			,a.InstanceId	
			,a.CommandNum
			,a.TargetNum		
			,a.ModifiedTimes		
			,a.CommandType		
			,a.ExecuteType		
			,a.StockDirection		
			,a.FuturesDirection	
			,a.EntrustStatus		
			,a.DealStatus
			,a.SubmitPerson
			,a.CreatedDate
			,a.ModifiedDate				
			,a.StartDate			
			,a.EndDate	
			,b.MonitorUnitId
			,b.InstanceCode
			,c.PortfolioId
			,c.MonitorUnitName	
			,d.PortfolioCode
			,d.PortfolioName
			,d.AccountCode
			,d.AccountName
		from tradingcommand a
		inner join tradinginstance b
		on a.InstanceId = b.InstanceId
		inner join monitorunit c
		on b.MonitorUnitId = c.MonitorUnitId
		inner join ufxportfolio d
		on c.PortfolioId=d.PortfolioId
		where CommandId=@CommandId
	end
	else
	begin
		select 
			a.CommandId			
			,a.InstanceId	
			,a.CommandNum
			,a.TargetNum		
			,a.ModifiedTimes		
			,a.CommandType		
			,a.ExecuteType		
			,a.StockDirection		
			,a.FuturesDirection	
			,a.EntrustStatus		
			,a.DealStatus
			,a.SubmitPerson
			,a.CreatedDate
			,a.ModifiedDate				
			,a.StartDate			
			,a.EndDate				
			,b.MonitorUnitId
			,b.InstanceCode
			,c.PortfolioId
			,c.MonitorUnitName	
			,d.PortfolioCode
			,d.PortfolioName
			,d.AccountCode
			,d.AccountName
		from tradingcommand a
		inner join tradinginstance b
		on a.InstanceId = b.InstanceId
		inner join monitorunit c
		on b.MonitorUnitId = c.MonitorUnitId
		inner join ufxportfolio d
		on c.PortfolioId=d.PortfolioId
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
	,@WeightAmount		int
	,@CommandAmount		int
	,@CommandDirection	int
	,@CommandPrice		numeric(20, 4) --如果不限价，则价格设置为0
)
as
begin
	insert into tradingcommandsecurity(
		CommandId			
		,SecuCode			
		,SecuType
		,WeightAmount			
		,CommandAmount	
		,CommandDirection	
		,EntrustedAmount	
		,CommandPrice		
		,EntrustStatus		
	)values(
		@CommandId			
		,@SecuCode			
		,@SecuType
		,@WeightAmount		
		,@CommandAmount	
		,@CommandDirection	
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
if exists (select name from sysobjects where name='procTradingCommandSecurityDelete')
drop proc procTradingCommandSecurityDelete

go
create proc procTradingCommandSecurityDelete(
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
		,WeightAmount	
		,CommandAmount
		,CommandDirection		
		,EntrustedAmount
		,CommandPrice	
		,EntrustStatus	
	from tradingcommandsecurity 
	where CommandId=@CommandId
end
