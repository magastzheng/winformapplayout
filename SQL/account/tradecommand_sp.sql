use tradingsystem

--==trade instance/trade command/trade security/entruct command/entrust security begin===

--++tradecommand begin++++
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
--++tradecommand end++++

--++tradecommandsecurity begin++++
go
if exists (select name from sysobjects where name='procTradeCommandSecurityInsert')
drop proc procTradeCommandSecurityInsert

go
create proc procTradeCommandSecurityInsert(
	@CommandId			int 
	,@SecuCode			varchar(10) 
	,@SecuType			int
	,@CommandAmount		int
	,@CommandDirection	int
	,@CommandPrice		numeric(20, 4) --如果不限价，则价格设置为0
)
as
begin
	insert into tradecommandsecurity(
		CommandId			
		,SecuCode			
		,SecuType		
		,CommandAmount	
		,CommandDirection	
		,CommandPrice		
		,EntrustStatus		
	)values(
		@CommandId			
		,@SecuCode			
		,@SecuType	
		,@CommandAmount	
		,@CommandDirection
		,@CommandPrice		
		,1		
	)
end

go
if exists (select name from sysobjects where name='procTradeCommandSecurityInsertOrUpdate')
drop proc procTradeCommandSecurityInsertOrUpdate

go
create proc procTradeCommandSecurityInsertOrUpdate(
	@CommandId			int 
	,@SecuCode			varchar(10) 
	,@SecuType			int
	,@CommandAmount		int
	,@CommandDirection	int
	,@CommandPrice		numeric(20, 4) --如果不限价，则价格设置为0
)
as
begin

	declare @Total int

	set @Total=(select count(SecuCode) as Total 
		from tradecommandsecurity 
		where CommandId = @CommandId
		and SecuCode = @SecuCode
		and SecuType = @SecuType)
	
	if @Total = 0
	begin
		insert into tradecommandsecurity(
			CommandId			
			,SecuCode			
			,SecuType		
			,CommandAmount	
			,CommandDirection	
			,CommandPrice		
			,EntrustStatus		
		)values(
			@CommandId			
			,@SecuCode			
			,@SecuType	
			,@CommandAmount	
			,@CommandDirection
			,@CommandPrice		
			,1		
		)
	end
	else
	begin
		update tradecommandsecurity
		set CommandAmount = @CommandAmount
			,CommandDirection = @CommandDirection
			,CommandPrice = @CommandPrice
		where CommandId = @CommandId
			and SecuCode = @SecuCode
			and SecuType = @SecuType
	end
end
--go
--if exists (select name from sysobjects where name='procTradeCommandSecurityUpdateEntrustAmount')
--drop proc procTradeCommandSecurityUpdateEntrustAmount

--go
--create proc procTradeCommandSecurityUpdateEntrustAmount(
--	@CommandId			int 
--	,@SecuCode			varchar(10) 
--	,@EntrustedAmount	int
--)
--as
--begin
--	update tradingcommandsecurity
--	set EntrustedAmount=@EntrustedAmount,
--		EntrustStatus=
--		case 
--			when CommandAmount=@EntrustedAmount then 3 -- 已完成
--			else 2 -- 部分执行
--		end
--	where CommandId=@CommandId and SecuCode=@SecuCode
--end

go
if exists (select name from sysobjects where name='procTradeCommandSecurityDelete')
drop proc procTradeCommandSecurityDelete

go
create proc procTradeCommandSecurityDelete(
	@CommandId			int 
	,@SecuCode			varchar(10) 
	,@SecuType			int
)
as
begin
	delete from tradecommandsecurity 
	where CommandId=@CommandId 
	and SecuCode=@SecuCode
	and SecuType=@SecuType
end

go
if exists (select name from sysobjects where name='procTradeCommandSecuritySelect')
drop proc procTradeCommandSecuritySelect

go
create proc procTradeCommandSecuritySelect(
	@CommandId	int 
)
as
begin
	select
		CommandId		
		,SecuCode		
		,SecuType	
		,CommandAmount
		,CommandDirection		
		,CommandPrice	
		,EntrustStatus	
	from tradecommandsecurity 
	where CommandId=@CommandId
end

--++tradecommandsecurity end++++

--++tradeinstance begin++++
go
if exists (select name from sysobjects where name='procTradeInstanceInsert')
drop proc procTradeInstanceInsert

go
create proc procTradeInstanceInsert(
	@InstanceCode		varchar(20)
	,@PortfolioId		int
	,@MonitorUnitId		int
	,@TemplateId		int
	,@StockDirection	int
	,@FuturesContract	varchar(10)
	,@FuturesDirection	int
	,@OperationCopies	int
	,@StockPriceType	int
	,@FuturesPriceType	int
	,@Status			int
	,@Owner				int
	,@CreatedDate		datetime
	,@Notes				varchar(100)
)
as
begin
	declare @newid int
	insert into tradeinstance(
		InstanceCode	
		,PortfolioId	
		,MonitorUnitId	
		,TemplateId	
		,StockDirection		
		,FuturesContract	
		,FuturesDirection	
		,OperationCopies	
		,StockPriceType		
		,FuturesPriceType
		,Status	
		,Owner				
		,CreatedDate
		,Notes			
	)
	values(
		@InstanceCode	
		,@PortfolioId	
		,@MonitorUnitId		
		,@TemplateId
		,@StockDirection	
		,@FuturesContract	
		,@FuturesDirection	
		,@OperationCopies	
		,@StockPriceType	
		,@FuturesPriceType
		,@Status	
		,@Owner				
		,@CreatedDate
		,@Notes		
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end

go
if exists (select name from sysobjects where name='procTradeInstanceUpdate')
drop proc procTradeInstanceUpdate

go
create proc procTradeInstanceUpdate(
	@InstanceId			int
	,@InstanceCode		varchar(20)
	,@MonitorUnitId		int
	,@TemplateId		int
	,@ModifiedDate		datetime
	,@Notes				varchar(100)
)
as
begin
	
	--不可修改PortfolioId
	update tradeinstance
	set			
		InstanceCode		= @InstanceCode
		,MonitorUnitId		= @MonitorUnitId
		,TemplateId			= @TemplateId	
		,ModifiedDate		= @ModifiedDate	
		,Notes				= @Notes
	where InstanceId=@InstanceId
end

go
if exists (select name from sysobjects where name='procTradeInstanceDelete')
drop proc procTradeInstanceDelete

go
create proc procTradeInstanceDelete(
	@InstanceId	int = NULL
)
as
begin
	delete from tradeinstance where InstanceId=@InstanceId
end

go
if exists (select name from sysobjects where name='procTradeInstanceExist')
drop proc procTradeInstanceExist

go
create proc procTradeInstanceExist(
	@InstanceCode varchar(20)
)
as
begin
	declare @total int
	set @total = (select count(InstanceId)		
					from tradeinstance
					where InstanceCode=@InstanceCode)
	return @total
end

go
if exists (select name from sysobjects where name='procTradeInstanceSelectCombine')
drop proc procTradeInstanceSelectCombine

go
create proc procTradeInstanceSelectCombine(
	@InstanceId	int = NULL
)
as
begin
	if @InstanceId is not null or @InstanceId > 0
	begin
		select
			a.InstanceId			
			,a.InstanceCode	
			,a.PortfolioId	
			,a.MonitorUnitId
			,a.TemplateId		
			,a.StockDirection		
			,a.FuturesContract	
			,a.FuturesDirection	
			,a.OperationCopies	
			,a.StockPriceType		
			,a.FuturesPriceType	
			,a.Status
			,a.Owner				
			,a.CreatedDate		
			,a.ModifiedDate	
			,a.Notes
			,b.PortfolioCode
			,b.PortfolioName
			,b.AccountCode
			,b.AccountName
			,b.AssetNo
			,b.AssetName
			,c.MonitorUnitName	
			,d.TemplateName
		from tradeinstance a
		inner join ufxportfolio b
		on a.PortfolioId=b.PortfolioId
		inner join monitorunit c
		on a.MonitorUnitId = c.MonitorUnitId
		left join stocktemplate d
		on a.TemplateId = d.TemplateId
		where a.InstanceId=@InstanceId
	end
	else
	begin
		select
			a.InstanceId			
			,a.InstanceCode	
			,a.PortfolioId	
			,a.MonitorUnitId
			,a.TemplateId		
			,a.StockDirection		
			,a.FuturesContract	
			,a.FuturesDirection	
			,a.OperationCopies	
			,a.StockPriceType		
			,a.FuturesPriceType	
			,a.Status
			,a.Owner				
			,a.CreatedDate		
			,a.ModifiedDate	
			,a.Notes
			,b.PortfolioCode
			,b.PortfolioName
			,b.AccountCode
			,b.AccountName
			,b.AssetNo
			,b.AssetName
			,c.MonitorUnitName	
			,d.TemplateName
		from tradeinstance a
		inner join ufxportfolio b
		on a.PortfolioId=b.PortfolioId
		inner join monitorunit c
		on a.MonitorUnitId = c.MonitorUnitId
		left join stocktemplate d
		on a.TemplateId = d.TemplateId
	end
end

go
if exists (select name from sysobjects where name='procTradeInstanceSelectCombineByCode')
drop proc procTradeInstanceSelectCombineByCode

go
create proc procTradeInstanceSelectCombineByCode(
	@InstanceCode varchar(20)
)
as
begin
	select
		a.InstanceId			
		,a.InstanceCode	
		,a.PortfolioId	
		,a.MonitorUnitId	
		,a.TemplateId	
		,a.StockDirection		
		,a.FuturesContract	
		,a.FuturesDirection	
		,a.OperationCopies	
		,a.StockPriceType		
		,a.FuturesPriceType	
		,a.Status
		,a.Owner				
		,a.CreatedDate		
		,a.ModifiedDate	
		,a.Notes
		,b.PortfolioCode
		,b.PortfolioName
		,b.AccountCode
		,b.AccountName
		,b.AssetNo
		,b.AssetName
		,c.MonitorUnitName	
		,d.TemplateName
	from tradeinstance a
	inner join ufxportfolio b
	on a.PortfolioId=b.PortfolioId
	inner join monitorunit c
	on a.MonitorUnitId = c.MonitorUnitId
	left join stocktemplate d
	on a.TemplateId = d.TemplateId
	where a.InstanceCode=@InstanceCode
end
--++tradeinstance end++++

--++tradeinstancesecurity begin++++
go
if exists (select name from sysobjects where name='procTradeInstanceSecurityInsert')
drop proc procTradeInstanceSecurityInsert

go
create proc procTradeInstanceSecurityInsert(
	@InstanceId				int
	,@SecuCode				varchar(10)
	,@SecuType				int
	,@PositionType			int
	,@InstructionPreBuy		int
	,@InstructionPreSell	int
	,@RowId	varchar(20) output
)
as
begin
	insert into tradeinstancesecurity(
		InstanceId
		,SecuCode
		,SecuType
		,PositionType
		,InstructionPreBuy
		,InstructionPreSell
		,PositionAmount
		,BuyBalance
		,SellBalance
		,DealFee
		,BuyToday
		,SellToday
		,CreatedDate
		,ModifiedDate
		,LastDate
	)
	values(@InstanceId
			,@SecuCode
			,@SecuType
			,@PositionType
			,@InstructionPreBuy
			,@InstructionPreSell
			,0
			,0.0
			,0.0
			,0.0
			,0
			,0
			,getdate()
			,NULL
			,getdate()
		)

	set @RowId=@SecuCode+';'+cast(@InstanceId as varchar)
end

go
if exists (select name from sysobjects where name='procTradeInstanceSecurityInsertOrUpdate')
drop proc procTradeInstanceSecurityInsertOrUpdate

go
create proc procTradeInstanceSecurityInsertOrUpdate(
	@InstanceId				int
	,@SecuCode				varchar(10)
	,@SecuType				int
	,@PositionType			int
	,@InstructionPreBuy		int
	,@InstructionPreSell	int
	,@RowId	varchar(20) output
)
as
begin
	declare @Total int

	set @Total=(select count(SecuCode) as Total from tradeinstancesecurity 
		where InstanceId=@InstanceId and SecuCode=@SecuCode)
	
	if @Total = 0
	begin
		insert into tradeinstancesecurity(
			InstanceId
			,SecuCode
			,SecuType
			,PositionType
			,InstructionPreBuy
			,InstructionPreSell
			,PositionAmount
			,BuyBalance
			,SellBalance
			,DealFee
			,BuyToday
			,SellToday
			,CreatedDate
			,ModifiedDate
			,LastDate
		)
		values(@InstanceId
				,@SecuCode
				,@SecuType
				,@PositionType
				,@InstructionPreBuy
				,@InstructionPreSell
				,0
				,0.0
				,0.0
				,0.0
				,0
				,0
				,getdate()
				,NULL
				,getdate()
			)
	end
	else
	begin
		declare @PreBuy int
		declare @PreSell int

		select @PreBuy = InstructionPreBuy, @PreSell = InstructionPreSell
		from tradeinstancesecurity
		where InstanceId=@InstanceId and SecuCode=@SecuCode
		--更新预买和预卖数量
		set @PreBuy = @PreBuy+@InstructionPreBuy
		set @PreSell = @PreSell+@InstructionPreSell

		update tradeinstancesecurity
		set InstructionPreBuy = @PreBuy
			,InstructionPreSell = @PreSell
			,ModifiedDate = getdate()
			,LastDate = getdate()
		where InstanceId=@InstanceId and SecuCode=@SecuCode
	end

	set @RowId=@SecuCode+';'+cast(@InstanceId as varchar)
end

go
if exists (select name from sysobjects where name='procTradeInstanceSecurityTransfer')
drop proc procTradeInstanceSecurityTransfer

go
create proc procTradeInstanceSecurityTransfer(
	@InstanceId				int
	,@SecuCode				varchar(10)
	,@SecuType				int
	,@PositionType			int
	,@PositionAmount		int
)
as
begin
	declare @Total int
	set @Total = (select count(SecuCode) from tradeinstancesecurity
				  where InstanceId=@InstanceId
				  and SecuCode = @SecuCode
				  and SecuType = @SecuType)
	
	if @Total = 1
	begin
		update tradeinstancesecurity
		set PositionAmount = @PositionAmount
			,ModifiedDate = getdate()
			,LastDate = getdate()
		where InstanceId=@InstanceId
			and SecuCode = @SecuCode
			and SecuType = @SecuType
	end
	else
	begin
		insert into tradeinstancesecurity(
			InstanceId
			,SecuCode
			,SecuType
			,PositionType
			,InstructionPreBuy
			,InstructionPreSell
			,PositionAmount
			,BuyBalance
			,SellBalance
			,DealFee
			,BuyToday
			,SellToday
			,CreatedDate
			,ModifiedDate
			,LastDate
		)
		values(@InstanceId
			,@SecuCode
			,@SecuType
			,@PositionType
			,0
			,0
			,@PositionAmount
			,0.0
			,0.0
			,0.0
			,0
			,0
			,getdate()
			,NULL
			,getdate()
		)
	end
	

	--set @RowId=@SecuCode+';'+cast(@InstanceId as varchar)
end

go
if exists (select name from sysobjects where name='procTradeInstanceSecurityBuyToday')
drop proc procTradeInstanceSecurityBuyToday

go
create proc procTradeInstanceSecurityBuyToday(
	@InstanceId			int
	,@SecuCode			varchar(10)
	,@BuyAmount			int
	,@BuyBalance		numeric(20, 4)
	,@DealFee			numeric(20, 4)
)
as
begin
	declare @TotalPositionAmount int
	declare @TotalBuyToday int
	declare @TotalBuyBalance numeric(20, 4)
	declare @TotalDealFee numeric(20, 4)

	select @TotalPositionAmount	= PositionAmount
		,@TotalBuyToday		= BuyToday
		,@TotalBuyBalance	= BuyBalance
		,@TotalDealFee		= DealFee
	from tradeinstancesecurity
	where InstanceId=@InstanceId 
		and SecuCode=@SecuCode

	set @TotalPositionAmount = @TotalPositionAmount+@BuyAmount
	set @TotalBuyToday = @TotalBuyToday+@BuyAmount
	set @TotalBuyBalance = @TotalBuyBalance+@BuyBalance
	set @TotalDealFee = @TotalDealFee+@DealFee

	update tradeinstancesecurity
	set PositionAmount	= @TotalPositionAmount
		,BuyToday		= @TotalBuyToday
		,BuyBalance		= @TotalBuyBalance
		,DealFee		= @TotalDealFee
		,ModifiedDate	= getdate()
	where InstanceId=@InstanceId 
		and SecuCode=@SecuCode
end

go
if exists (select name from sysobjects where name='procTradeInstanceSecuritySellToday')
drop proc procTradeInstanceSecuritySellToday

go
create proc procTradeInstanceSecuritySellToday(
	@InstanceId			int
	,@SecuCode			varchar(10)
	,@SellAmount		int
	,@SellBalance		numeric(20, 4)
	,@DealFee			numeric(20, 4)
)
as
begin
	declare @TotalPositionAmount int
	declare @TotalSellToday int
	declare @TotalSellBalance numeric(20, 4)
	declare @TotalDealFee numeric(20, 4)

	select @TotalPositionAmount	= PositionAmount
		,@TotalSellToday		= SellToday
		,@TotalSellBalance	= SellBalance
		,@TotalDealFee		= DealFee
	from tradeinstancesecurity
	where InstanceId=@InstanceId 
		and SecuCode=@SecuCode

	set @TotalPositionAmount = @TotalPositionAmount-@SellAmount
	set @TotalSellToday = @TotalSellToday+@SellAmount
	set @TotalSellBalance = @TotalSellBalance+@SellBalance
	set @TotalDealFee = @TotalDealFee+@DealFee

	update tradeinstancesecurity
	set PositionAmount	= @TotalPositionAmount
		,SellToday		= @TotalSellToday
		,SellBalance	= @TotalSellBalance
		,DealFee		= @TotalDealFee
		,ModifiedDate	= getdate()
	where InstanceId=@InstanceId 
		and SecuCode=@SecuCode
end

go
if exists (select name from sysobjects where name='procTradeInstanceSecurityInstructionPreTrade')
drop proc procTradeInstanceSecurityInstructionPreTrade

go
create proc procTradeInstanceSecurityInstructionPreTrade(
	@InstanceId				int
	,@SecuCode				varchar(10)
	,@InstructionPreBuy		int
	,@InstructionPreSell	int
)
as
begin
	
	declare @TotalPreBuy int
	declare @TotalPreSell int

	select @TotalPreBuy=@InstructionPreBuy
		,@TotalPreSell=@InstructionPreSell
	from tradeinstancesecurity
	where InstanceId=@InstanceId
		and SecuCode=@SecuCode

	set @TotalPreBuy = @TotalPreBuy+@InstructionPreBuy
	set @TotalPreSell = @TotalPreSell+@InstructionPreSell

	update tradeinstancesecurity
	set InstructionPreBuy	= @TotalPreBuy
		,InstructionPreSell = @TotalPreSell
		,ModifiedDate		= getdate()
	where InstanceId=@InstanceId
		and SecuCode=@SecuCode
end

go
if exists (select name from sysobjects where name='procTradeInstanceSecuritySettle')
drop proc procTradeInstanceSecuritySettle

go
create proc procTradeInstanceSecuritySettle
as
begin
	--每次成交都会把增减持仓，所有不需要再把当天买量和卖量再加一次
	--根据LastDate结算每支证券，清空当天买量和卖量，并重新设置LastDate
	update tradeinstancesecurity
	set BuyToday = 0.0
		,SellToday = 0.0
		,LastDate = getdate()
		,ModifiedDate = getdate()
	from tradeinstancesecurity
	where LastDate < convert(varchar, getdate(), 112)
end

--go
--if exists (select name from sysobjects where name='procTradeInstanceSecurityUpdatePosition')
--drop proc procTradeInstanceSecurityUpdatePosition

--go
--create proc procTradeInstanceSecurityUpdatePosition(
--	@InstanceId			int
--	,@SecuCode			varchar(10)
--	,@PositionAmount	int
--	,@AvailableAmount	int
--)
--as
--begin
--	--declare @newid varchar(20)
--	update tradeinstancesecurity
--	set
--		PositionAmount = @PositionAmount
--		,AvailableAmount = @AvailableAmount
--	where InstanceId=@InstanceId and SecuCode=@SecuCode
--end

--go
--if exists (select name from sysobjects where name='procTradeInstanceSecurityUpdatePreTrading')
--drop proc procTradeInstanceSecurityUpdatePreTrading

--go
--create proc procTradeInstanceSecurityUpdatePreTrading(
--	@InstanceId				int
--	,@SecuCode				varchar(10)
--	,@InstructionPreBuy		int
--	,@InstructionPreSell	int
--)
--as
--begin
--	update tradeinstancesecurity
--	set
--		InstructionPreBuy = @InstructionPreBuy
--		,InstructionPreSell = @InstructionPreSell
--	where InstanceId=@InstanceId and SecuCode=@SecuCode
--end

go
if exists (select name from sysobjects where name='procTradeInstanceSecurityValidDelete')
drop proc procTradeInstanceSecurityValidDelete

go
create proc procTradeInstanceSecurityValidDelete(
	@InstanceId int
	,@SecuCode	varchar(10)
	,@SecuType				int
	,@InstructionPreBuy		int
	,@InstructionPreSell	int
)
as
begin
	--只有在持仓为0，没有预买和预卖情况下，才能删除该股票
	declare @PositionAmount int
	declare @PreBuy int
	declare @PreSell int
	declare @Result int

	select @PositionAmount = PositionAmount
		,@PreBuy = InstructionPreBuy
		,@PreSell = InstructionPreSell
	from tradeinstancesecurity
	where InstanceId=@InstanceId 
	and SecuCode=@SecuCode
	and SecuType=@SecuType

	set @PreBuy = @PreBuy - @InstructionPreBuy
	set @PreSell = @PreSell - @InstructionPreSell

	if (@PositionAmount is null or @PositionAmount = 0) 
	and (@PreBuy is null or @PreBuy = 0)
	and (@PreSell is null or @PreSell = 0)
	begin 
		delete from tradeinstancesecurity
		where InstanceId=@InstanceId 
		and SecuCode=@SecuCode 
		and SecuType=@SecuType

		set @Result = 1
	end
	else
	begin
		set @Result = 0
	end

	return @Result
end

go
if exists (select name from sysobjects where name='procTradeInstanceSecurityDelete')
drop proc procTradeInstanceSecurityDelete

go
create proc procTradeInstanceSecurityDelete(
	@InstanceId int
	,@SecuCode	varchar(10)
)
as
begin
	delete from tradeinstancesecurity
	where InstanceId=@InstanceId and SecuCode=@SecuCode
end

go
if exists (select name from sysobjects where name='procTradeInstanceSecuritySelect')
drop proc procTradeInstanceSecuritySelect

go
create proc procTradeInstanceSecuritySelect(
	@InstanceId int
)
as
begin
	select InstanceId
		   ,SecuCode		
		   ,SecuType		
		   ,PositionType	
		   ,PositionAmount	
		   ,InstructionPreBuy
		   ,InstructionPreSell
		   ,BuyBalance	
		   ,SellBalance
		   ,DealFee	
		   ,BuyToday	
		   ,SellToday
		   ,CreatedDate
		   ,ModifiedDate
		   ,LastDate	
	from tradeinstancesecurity
	where InstanceId=@InstanceId
end
--++tradeinstancesecurity end++++

--++tradeinstanceadjustment begin++++
go
if exists (select name from sysobjects where name='procTradeInstanceAdjustmentInsert')
drop proc procTradeInstanceAdjustmentInsert

go
create proc procTradeInstanceAdjustmentInsert(
	@SourceInstanceId int
	,@SourceFundCode	varchar(20)
	,@SourcePortfolioCode varchar(20)
	,@DestinationInstanceId int
	,@DestinationFundCode varchar(20)
	,@DestinationPortfolioCode varchar(20)
	,@SecuCode varchar(10)
	,@SecuType int
	,@PositionType int
	,@Price	decimal(20, 4)
	,@Amount int
	,@AdjustType int
	,@Operator varchar(20)
	,@StockHolderId varchar(20)
	,@SeatNo	varchar(20)
	,@Notes varchar(100)
)
as
begin
	declare @newid int

	insert into tradeinstanceadjustment(
		CreateDate
		,SourceInstanceId
		,SourceFundCode
		,SourcePortfolioCode
		,DestinationInstanceId
		,DestinationFundCode
		,DestinationPortfolioCode
		,SecuCode
		,SecuType
		,PositionType
		,Price
		,Amount
		,AdjustType
		,Operator
		,StockHolderId
		,SeatNo
		,Notes
	)values(
		getdate()
		,@SourceInstanceId
		,@SourceFundCode
		,@SourcePortfolioCode
		,@DestinationInstanceId
		,@DestinationFundCode
		,@DestinationPortfolioCode
		,@SecuCode
		,@SecuType
		,@PositionType
		,@Price
		,@Amount
		,@AdjustType
		,@Operator
		,@StockHolderId
		,@SeatNo
		,@Notes
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end

go
if exists (select name from sysobjects where name='procTradeInstanceAdjustmentSelect')
drop proc procTradeInstanceAdjustmentSelect

go
create proc procTradeInstanceAdjustmentSelect
as
begin
	select
		Id
		,CreateDate
		,SourceInstanceId
		,SourceFundCode
		,SourcePortfolioCode
		,DestinationInstanceId
		,DestinationFundCode
		,DestinationPortfolioCode
		,SecuCode
		,SecuType
		,PositionType
		,Price
		,Amount
		,AdjustType
		,Operator
		,StockHolderId
		,SeatNo
		,Notes
	from tradeinstanceadjustment
end

go
create proc procTradeInstanceAdjustmentDelete(
	@Id int
)
as
begin
	if @Id is not null and @Id > 0
	begin
		delete from tradeinstanceadjustment
		where Id=@Id
	end
	else
	begin
		truncate table tradeinstanceadjustment
	end
end
--++tradeinstanceadjustment end++++

--==trade instance/trade command/trade security/entruct command/entrust security end===