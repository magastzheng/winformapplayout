use tradingsystem

if object_id('tradingcommand') is not null
drop table tradingcommand

create table tradingcommand(
	CommandId			int identity(1, 1) primary key	--指令序号
	,InstanceId			int not null					--交易实例ID
	,CommandNum			int								--指令份数
	--,TargetNum			int	--目标份数
	--,DealNum			int --成交份数
	,CommandStatus		int								--指令状态： 1 - 有效指令，2 - 已修改，3 - 已撤销， 4 - 委托完成， 5 - 已完成成交
	,ModifiedTimes		int								--修改次数
	,CommandType		int								-- 1 - 期现套利
	,ExecuteType		int								-- 1 开仓， 2 - 平仓
	,StockDirection		int								--10 -- 买入现货，11--卖出现货
	,FuturesDirection	int								--12-卖出开仓，13 -买入平仓
	,EntrustStatus		int								-- 1 - 未执行， 2 - 部分执行， 3- 已完成
	,DealStatus			int								-- 1 - 未成交， 2 - 部分成交， 3 - 已完成
	,SubmitPerson		int								--下达人
	,CreatedDate		datetime						-- 下达指令时间
	,ModifiedDate		datetime						-- 修改时间
	,StartDate			datetime						-- 指令有效开始时间
	,EndDate			datetime						-- 指令有效结束时间
	,Notes				varchar(100)					-- 备注
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
	,@CommandNum		int
	,@CommandType		int	
	,@ExecuteType		int	
	,@StockDirection	int	
	,@FuturesDirection	int
	,@EntrustStatus		int
	,@DealStatus		int	
	,@SubmitPerson		int
	,@CreatedDate		datetime
	,@StartDate			datetime
	,@EndDate			datetime
	,@Notes				varchar(100)
)
as
begin
	declare @newid int
	insert into tradingcommand(
		InstanceId	
		,CommandNum	
		,CommandStatus
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
		,Notes			
	)
	values(
		@InstanceId
		,@CommandNum
		,1				--默认为有效指令		
		,1				--默认修改一次
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
		,@Notes
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end

go
if exists (select name from sysobjects where name='procTradingCommandUpdate')
drop proc procTradingCommandUpdate

go
create proc procTradingCommandUpdate(
	@CommandId			int
	,@CommandStatus		int	
	,@ModifiedDate		datetime
	,@StartDate			datetime
	,@EndDate			datetime
	,@Notes				varchar(100)
)
as
begin

	declare @ModifiedTimes int
	set @ModifiedTimes = (select ModifiedTimes 
						from tradingcommand
						where CommandId=@CommandId)
	if @ModifiedTimes is not null
	begin
		set @ModifiedTimes = @ModifiedTimes + 1
	end
	else
	begin
		set @ModifiedTimes = 1
	end

	update tradingcommand
	set			
		CommandStatus		= @CommandStatus
		,ModifiedTimes		= @ModifiedTimes
		,ModifiedDate		= @ModifiedDate
		,StartDate			= @StartDate
		,EndDate			= @EndDate
		,Notes				= @Notes
	where CommandId=@CommandId
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

--go
--if exists (select name from sysobjects where name='procTradingCommandUpdateTargetNum')
--drop proc procTradingCommandUpdateTargetNum

--go
--create proc procTradingCommandUpdateTargetNum(
--	@CommandId			int
--	,@Copies			int		-- positive integer means ADD; negative integer means reduce
--	,@ModifiedDate		datetime
--)
--as
--begin
	
--	declare @TargetNum int
--	set @TargetNum = (select TargetNum from tradingcommand where CommandId=@CommandId)

--	update tradingcommand
--	set	TargetNum	= @TargetNum+@Copies
--		,ModifiedDate = @ModifiedDate
--	where CommandId=@CommandId
--end

--go
--if exists (select name from sysobjects where name='procTradingCommandUpdateTargetNumBySubmitId')
--drop proc procTradingCommandUpdateTargetNumBySubmitId

--go
--create proc procTradingCommandUpdateTargetNumBySubmitId(
--	@CommandId			int
--	,@SubmitId			int
--	,@ModifiedDate		datetime
--)
--as
--begin
--	declare @TotalTargetNum int
--	declare @ThisTargetNum int
--	--
--	set @TotalTargetNum=(select TargetNum from tradingcommand where CommandId=@CommandId)
--	--从entrustcommand中获取本次委托份数
--	set @ThisTargetNum=(select Copies from entrustcommand where SubmitId=@SubmitId and EntrustStatus=12)

--	set @TotalTargetNum=@TotalTargetNum-@ThisTargetNum

--	if @TotalTargetNum < 0
--	begin
--		set @TotalTargetNum=0
--	end

--	update tradingcommand
--	set	TargetNum	= @TotalTargetNum
--		,ModifiedDate = @ModifiedDate
--	where CommandId=@CommandId
--end

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
	delete from tradingcommandsecurity where CommandId=@CommandId

	delete from tradingcommand where CommandId=@CommandId
end

--go
--if exists (select name from sysobjects where name='procTradingCommandSelect')
--drop proc procTradingCommandSelect

--go
--create proc procTradingCommandSelect(
--	@CommandId			int
--)
--as
--begin
--	if @CommandId is not null and @CommandId > 0
--	begin
--		select 
--			CommandId			
--			,InstanceId	
--			,CommandNum
--			,ModifiedTimes		
--			,CommandType		
--			,ExecuteType		
--			,StockDirection		
--			,FuturesDirection	
--			,EntrustStatus		
--			,DealStatus	
--			,SubmitPerson
--			,CreatedDate
--			,ModifiedDate		
--			,StartDate			
--			,EndDate			
--		from tradingcommand
--		where CommandId=@CommandId
--	end
--	else
--	begin
--		select 
--			CommandId			
--			,InstanceId	
--			,CommandNum		
--			,ModifiedTimes		
--			,CommandType		
--			,ExecuteType		
--			,StockDirection		
--			,FuturesDirection	
--			,EntrustStatus		
--			,DealStatus	
--			,SubmitPerson
--			,CreatedDate
--			,ModifiedDate			
--			,StartDate			
--			,EndDate			
--		from tradingcommand
--	end
--end

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
			,a.CommandStatus
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
			,a.Notes
			,b.MonitorUnitId
			,b.InstanceCode
			,c.PortfolioId
			,c.MonitorUnitName	
			,c.StockTemplateId
			,c.BearContract
			,d.PortfolioCode
			,d.PortfolioName
			,d.AccountCode
			,d.AccountName
			,e.TemplateName
		from tradingcommand a
		inner join tradinginstance b
		on a.InstanceId = b.InstanceId
		inner join monitorunit c
		on b.MonitorUnitId = c.MonitorUnitId
		inner join ufxportfolio d
		on c.PortfolioId=d.PortfolioId
		inner join stocktemplate e
		on c.StockTemplateId = e.TemplateId
		where CommandId=@CommandId
	end
	else
	begin
		select 
			a.CommandId			
			,a.InstanceId	
			,a.CommandNum	
			,a.CommandStatus
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
			,a.Notes		
			,b.MonitorUnitId
			,b.InstanceCode
			,c.PortfolioId
			,c.MonitorUnitName	
			,c.StockTemplateId
			,c.BearContract
			,d.PortfolioCode
			,d.PortfolioName
			,d.AccountCode
			,d.AccountName
			,e.TemplateName
		from tradingcommand a
		inner join tradinginstance b
		on a.InstanceId = b.InstanceId
		inner join monitorunit c
		on b.MonitorUnitId = c.MonitorUnitId
		inner join ufxportfolio d
		on c.PortfolioId=d.PortfolioId
		inner join stocktemplate e
		on c.StockTemplateId = e.TemplateId
	end
end