use tradingsystem

if object_id('tradinginstancesecurity') is not null
drop table tradinginstancesecurity

--份数可以从模板中获取
create table tradinginstancesecurity(
	InstanceId			int not null	--实例Id
	--,InstanceCode		varchar(20)
	,SecuCode			varchar(10) not null		--证券代码
	,SecuType			int				--证券类型： 股票2， 期货3
	--,WeightAmount		int				--权重数量 直接从模板中获取
	,PositionType		int				--股票多头1，股票空头2，期货多头3， 期货空头4
	,PositionAmount		int				--持仓数量
	--,AvailableAmount	int				--可用数量
	,InstructionPreBuy	int				--指令预买数量
	,InstructionPreSell	int				--指令预卖数量
	,BuyBalance			numeric(20, 4)	--成本
	,SellBalance		numeric(20, 4)	--卖出金额
	,DealFee			numeric(20, 4)  --交易费用
	,BuyToday			int				--当日买量
	,SellToday			int				--当日卖量
	,CreatedDate		datetime		--创建时间
	,ModifiedDate		datetime		--修改时间
	,LastDate			datetime		--用于记录最近一天时间，该字段用于清算
	,constraint pk_TradingInstanceSecurity_IdSecuCode primary key(InstanceId, SecuCode)
)

go
if exists (select name from sysobjects where name='procTradingInstanceSecurityInsert')
drop proc procTradingInstanceSecurityInsert

go
create proc procTradingInstanceSecurityInsert(
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
	insert into tradinginstancesecurity(
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
if exists (select name from sysobjects where name='procTradingInstanceSecurityTransfer')
drop proc procTradingInstanceSecurityTransfer

go
create proc procTradingInstanceSecurityTransfer(
	@InstanceId				int
	,@SecuCode				varchar(10)
	,@SecuType				int
	,@PositionType			int
	,@PositionAmount		int
)
as
begin
	declare @Total int
	set @Total = (select count(SecuCode) from tradinginstancesecurity
				  where InstanceId=@InstanceId
				  and SecuCode = @SecuCode
				  and SecuType = @SecuType)
	
	if @Total = 1
	begin
		update tradinginstancesecurity
		set PositionAmount = @PositionAmount
			,ModifiedDate = getdate()
		where InstanceId=@InstanceId
			and SecuCode = @SecuCode
			and SecuType = @SecuType
	end
	else
	begin
		insert into tradinginstancesecurity(
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
if exists (select name from sysobjects where name='procTradingInstanceSecurityBuyToday')
drop proc procTradingInstanceSecurityBuyToday

go
create proc procTradingInstanceSecurityBuyToday(
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
	from tradinginstancesecurity
	where InstanceId=@InstanceId 
		and SecuCode=@SecuCode

	set @TotalPositionAmount = @TotalPositionAmount+@BuyAmount
	set @TotalBuyToday = @TotalBuyToday+@BuyAmount
	set @TotalBuyBalance = @TotalBuyBalance+@BuyBalance
	set @TotalDealFee = @TotalDealFee+@DealFee

	update tradinginstancesecurity
	set PositionAmount	= @TotalPositionAmount
		,BuyToday		= @TotalBuyToday
		,BuyBalance		= @TotalBuyBalance
		,DealFee		= @TotalDealFee
		,ModifiedDate	= getdate()
	where InstanceId=@InstanceId 
		and SecuCode=@SecuCode
end

go
if exists (select name from sysobjects where name='procTradingInstanceSecuritySellToday')
drop proc procTradingInstanceSecuritySellToday

go
create proc procTradingInstanceSecuritySellToday(
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
	from tradinginstancesecurity
	where InstanceId=@InstanceId 
		and SecuCode=@SecuCode

	set @TotalPositionAmount = @TotalPositionAmount-@SellAmount
	set @TotalSellToday = @TotalSellToday+@SellAmount
	set @TotalSellBalance = @TotalSellBalance+@SellBalance
	set @TotalDealFee = @TotalDealFee+@DealFee

	update tradinginstancesecurity
	set PositionAmount	= @TotalPositionAmount
		,SellToday		= @TotalSellToday
		,SellBalance	= @TotalSellBalance
		,DealFee		= @TotalDealFee
		,ModifiedDate	= getdate()
	where InstanceId=@InstanceId 
		and SecuCode=@SecuCode
end

go
if exists (select name from sysobjects where name='procTradingInstanceSecurityInstructionPreTrade')
drop proc procTradingInstanceSecurityInstructionPreTrade

go
create proc procTradingInstanceSecurityInstructionPreTrade(
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
	from tradinginstancesecurity
	where InstanceId=@InstanceId
		and SecuCode=@SecuCode

	set @TotalPreBuy = @TotalPreBuy+@InstructionPreBuy
	set @TotalPreSell = @TotalPreSell+@InstructionPreSell

	update tradinginstancesecurity
	set InstructionPreBuy	= @TotalPreBuy
		,InstructionPreSell = @TotalPreSell
		,ModifiedDate		= getdate()
	where InstanceId=@InstanceId
		and SecuCode=@SecuCode
end

go
if exists (select name from sysobjects where name='procTradingInstanceSecuritySettle')
drop proc procTradingInstanceSecuritySettle

go
create proc procTradingInstanceSecuritySettle
as
begin
	--根据LastDate结算每支证券，统计持仓量，清空当天买量和卖量，并重新设置LastDate
	update t
	set t.PositionAmount = d.PositionAmount+d.BuyToday-d.SellToday
		,t.BuyToday = 0.0
		,t.SellToday = 0.0
		,t.LastDate = getdate()
		,t.ModifiedDate = getdate()
	from tradinginstancesecurity t, tradinginstancesecurity d
	where t.InstanceId=d.InstanceId
		and t.SecuCode = d.SecuCode
		and t.LastDate < convert(varchar, getdate(), 112)
end

--go
--if exists (select name from sysobjects where name='procTradingInstanceSecurityUpdatePosition')
--drop proc procTradingInstanceSecurityUpdatePosition

--go
--create proc procTradingInstanceSecurityUpdatePosition(
--	@InstanceId			int
--	,@SecuCode			varchar(10)
--	,@PositionAmount	int
--	,@AvailableAmount	int
--)
--as
--begin
--	--declare @newid varchar(20)
--	update tradinginstancesecurity
--	set
--		PositionAmount = @PositionAmount
--		,AvailableAmount = @AvailableAmount
--	where InstanceId=@InstanceId and SecuCode=@SecuCode
--end

--go
--if exists (select name from sysobjects where name='procTradingInstanceSecurityUpdatePreTrading')
--drop proc procTradingInstanceSecurityUpdatePreTrading

--go
--create proc procTradingInstanceSecurityUpdatePreTrading(
--	@InstanceId				int
--	,@SecuCode				varchar(10)
--	,@InstructionPreBuy		int
--	,@InstructionPreSell	int
--)
--as
--begin
--	update tradinginstancesecurity
--	set
--		InstructionPreBuy = @InstructionPreBuy
--		,InstructionPreSell = @InstructionPreSell
--	where InstanceId=@InstanceId and SecuCode=@SecuCode
--end

go
if exists (select name from sysobjects where name='procTradingInstanceSecurityDelete')
drop proc procTradingInstanceSecurityDelete

go
create proc procTradingInstanceSecurityDelete(
	@InstanceId int
	,@SecuCode	varchar(10)
)
as
begin
	delete from tradinginstancesecurity
	where InstanceId=@InstanceId and SecuCode=@SecuCode
end

go
if exists (select name from sysobjects where name='procTradingInstanceSecuritySelect')
drop proc procTradingInstanceSecuritySelect

go
create proc procTradingInstanceSecuritySelect(
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
	from tradinginstancesecurity
	where InstanceId=@InstanceId
end

