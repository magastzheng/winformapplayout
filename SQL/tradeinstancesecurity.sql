use tradingsystem

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

