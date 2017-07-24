use tradingsystem

--====================================
--tradecommand table
--====================================
go
if exists (select name from sysobjects where name='procTradeCommandInsert')
drop proc procTradeCommandInsert

go
create proc procTradeCommandInsert(
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
	insert into tradecommand(
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
if exists (select name from sysobjects where name='procTradeCommandUpdate')
drop proc procTradeCommandUpdate

go
create proc procTradeCommandUpdate(
	@CommandId			int
	,@CommandStatus		int	
	,@ModifiedDate		datetime
	,@StartDate			datetime
	,@EndDate			datetime
	,@Notes				varchar(100)
	,@ModifiedCause		varchar(100) = NULL
	,@CancelCause		varchar(100) = NULL
)
as
begin

	declare @ModifiedTimes int
	set @ModifiedTimes = (select ModifiedTimes 
						from tradecommand
						where CommandId=@CommandId)
	if @ModifiedTimes is not null
	begin
		set @ModifiedTimes = @ModifiedTimes + 1
	end
	else
	begin
		set @ModifiedTimes = 1
	end

	if @CommandStatus = 3
	begin
		update tradecommand
		set			
			CommandStatus		= @CommandStatus
			,ModifiedTimes		= @ModifiedTimes
			,ModifiedDate		= @ModifiedDate
			,CancelDate			= @ModifiedDate
			,StartDate			= @StartDate
			,EndDate			= @EndDate
			,Notes				= @Notes
			,CancelCause		= @CancelCause
		where CommandId=@CommandId
	end
	else
	begin
		update tradecommand
		set			
			CommandStatus		= @CommandStatus
			,ModifiedTimes		= @ModifiedTimes
			,ModifiedDate		= @ModifiedDate
			,StartDate			= @StartDate
			,EndDate			= @EndDate
			,Notes				= @Notes
			,ModifiedCause		= @ModifiedCause
		where CommandId=@CommandId
	end
end

go
if exists (select name from sysobjects where name='procTradeCommandUpdateStatus')
drop proc procTradeCommandUpdateStatus

go
create proc procTradeCommandUpdateStatus(
	@CommandId			int
	,@EntrustStatus		int
	,@DealStatus		int	
	,@ModifiedDate		datetime
)
as
begin
	update tradecommand
	set			
		EntrustStatus		= @EntrustStatus
		,DealStatus			= @DealStatus
		,ModifiedDate		= @ModifiedDate
	where CommandId=@CommandId
end

go
if exists (select name from sysobjects where name='procTradeCommandDelete')
drop proc procTradeCommandDelete

go
create proc procTradeCommandDelete(
	@CommandId			int
)
as
begin
	--TODO:delete the tradecommandsecurity
	delete from tradecommandsecurity where CommandId=@CommandId

	delete from tradecommand where CommandId=@CommandId
end

go
if exists (select name from sysobjects where name='procTradeCommandSelectCombine')
drop proc procTradeCommandSelectCombine

go
create proc procTradeCommandSelectCombine(
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
			,a.CancelDate			
			,a.StartDate			
			,a.EndDate	
			,a.Notes
			,a.ModifiedCause
			,a.CancelCause
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
		from tradecommand a
		inner join tradeinstance b
		on a.InstanceId = b.InstanceId
		inner join monitorunit c
		on b.MonitorUnitId = c.MonitorUnitId
		inner join ufxportfolio d
		on c.PortfolioId=d.PortfolioId
		inner join stocktemplate e
		on b.TemplateId = e.TemplateId
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
			,a.CancelDate			
			,a.StartDate			
			,a.EndDate	
			,a.Notes
			,a.ModifiedCause
			,a.CancelCause	
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
		from tradecommand a
		inner join tradeinstance b
		on a.InstanceId = b.InstanceId
		inner join monitorunit c
		on b.MonitorUnitId = c.MonitorUnitId
		inner join ufxportfolio d
		on c.PortfolioId=d.PortfolioId
		inner join stocktemplate e
		on b.TemplateId = e.TemplateId
	end
end