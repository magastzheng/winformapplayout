use tradingsystem


--alter table tradeinstance
--add TemplateId int
--alter table tradeinstance
--add Notes varchar(100)

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
	,@StockDirection	int
	,@FuturesContract	varchar(10)
	,@FuturesDirection	int
	,@OperationCopies	int
	,@StockPriceType	int
	,@FuturesPriceType	int
	,@Status			int
	,@Owner				int
	,@ModifiedDate		datetime
	,@Notes				varchar(100)
)
as
begin
	declare @OldMonitorUnitId int
	declare @OldTemplateId int
	declare @OldStockDirection int
	declare @OldFuturesDirection int
	declare @OldOperationCopies int
	declare @OldNotes varchar
	--declare @OldStockPriceType int

	select @OldMonitorUnitId = MonitorUnitId
		,@OldTemplateId = TemplateId
		,@OldStockDirection = StockDirection
		,@OldFuturesDirection = FuturesDirection
		,@OldOperationCopies = OperationCopies
		,@OldNotes = Notes
		--,@OldStockPriceType = StockPriceType
	from tradeinstance
	where InstanceId=@InstanceId 

	if @MonitorUnitId = 0 or @MonitorUnitId < 0
	begin
		set @MonitorUnitId = @OldMonitorUnitId
	end

	if @TemplateId = 0 or @TemplateId < 0
	begin
		set @TemplateId = @OldTemplateId
	end

	if @OperationCopies = 0 or @OperationCopies < 0
	begin
		set @OperationCopies = @OldOperationCopies
	end

	if @StockDirection = 0 or @StockDirection < 0
	begin
		set @StockDirection = @OldStockDirection
	end

	if @FuturesDirection = 0 or @FuturesDirection < 0
	begin
		set @FuturesDirection = @OldFuturesDirection
	end

	if @Notes is null or len(@Notes) = 0
	begin
		set @Notes = @OldNotes
	end

	--不可修改PortfolioId
	update tradeinstance
	set			
		InstanceCode		= @InstanceCode
		,MonitorUnitId		= @MonitorUnitId
		,TemplateId			= @TemplateId
		,StockDirection		= @StockDirection	
		,FuturesContract	= @FuturesContract	
		,FuturesDirection	= @FuturesDirection
		,OperationCopies	= @OperationCopies
		,StockPriceType		= @StockPriceType
		,FuturesPriceType	= @FuturesPriceType
		,Status				= @Status
		,Owner				= @Owner	
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