use tradingsystem

if object_id('tradinginstance') is not null
drop table tradinginstance

create table tradinginstance(
	InstanceId			int identity(1, 1) primary key	--交易实例ID
	,InstanceCode		varchar(20)						--交易实例代码
	,PortfolioId		int								--组合ID,唯一确定交易实例和组合之间的关系
	,MonitorUnitId		int								--监控单元ID，监控单元可以改变
	,StockDirection		int			--股票委托方向：1 - 买入， 2 - 卖出， 3 - 调整到[买卖]， 4 - 调整到[只买]， 5 - 调整到[只卖], 10 -- 买入现货，11--卖出现货，12-卖出开仓，13 -买入平仓
	,FuturesContract	varchar(10)	--股指期货合约代码
	,FuturesDirection	int			--股指期货委托方向：12-卖出开仓，13 -买入平仓
	,OperationCopies	int			--期货合约操作份数
	,StockPriceType		int			--股票价格类型： 0 - 不限价，1 - 最新价，A-J盘1至盘10
	,FuturesPriceType	int			--期货合约价格类型： 0 - 不限价，1 - 最新价， A-E盘1到盘5
	,Status				int			--交易实例状态 0 - 无效， 1 - 有效
	,Owner				int			--所有者
	,CreatedDate		datetime	--交易实例创建时间
	,ModifiedDate		datetime	--交易实例修改时间
	--,StartDate			datetime -- 指令开始时间
	--,EndDate			datetime -- 指令结束时间
	--,EntrustedAmount	int		 -- 已委托数量	
)

go
if exists (select name from sysobjects where name='procTradingInstanceInsert')
drop proc procTradingInstanceInsert

go
create proc procTradingInstanceInsert(
	@InstanceCode		varchar(20)
	,@PortfolioId		int
	,@MonitorUnitId		int
	,@StockDirection	int
	,@FuturesContract	varchar(10)
	,@FuturesDirection	int
	,@OperationCopies	int
	,@StockPriceType	int
	,@FuturesPriceType	int
	,@Status			int
	,@Owner				int
	,@CreatedDate		datetime
)
as
begin
	declare @newid int
	insert into tradinginstance(
		InstanceCode	
		,PortfolioId	
		,MonitorUnitId		
		,StockDirection		
		,FuturesContract	
		,FuturesDirection	
		,OperationCopies	
		,StockPriceType		
		,FuturesPriceType
		,Status	
		,Owner				
		,CreatedDate			
	)
	values(
		@InstanceCode	
		,@PortfolioId	
		,@MonitorUnitId		
		,@StockDirection	
		,@FuturesContract	
		,@FuturesDirection	
		,@OperationCopies	
		,@StockPriceType	
		,@FuturesPriceType
		,@Status	
		,@Owner				
		,@CreatedDate		
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end

go
if exists (select name from sysobjects where name='procTradingInstanceUpdate')
drop proc procTradingInstanceUpdate

go
create proc procTradingInstanceUpdate(
	@InstanceId			int
	,@InstanceCode		varchar(20)
	,@MonitorUnitId		int
	,@StockDirection	int
	,@FuturesContract	varchar(10)
	,@FuturesDirection	int
	,@OperationCopies	int
	,@StockPriceType	int
	,@FuturesPriceType	int
	,@Status			int
	,@Owner				int
	,@ModifiedDate		datetime
)
as
begin
	declare @OldMonitorUnitId int
	declare @OldStockDirection int
	declare @OldFuturesDirection int
	declare @OldOperationCopies int
	--declare @OldStockPriceType int

	select @OldMonitorUnitId = MonitorUnitId
		,@OldStockDirection = StockDirection
		,@OldFuturesDirection = FuturesDirection
		,@OldOperationCopies = OperationCopies
		--,@OldStockPriceType = StockPriceType
	from tradinginstance
	where InstanceId=@InstanceId 

	if @MonitorUnitId = 0 or @MonitorUnitId < 0
	begin
		set @MonitorUnitId = @OldMonitorUnitId
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

	--不可修改PortfolioId
	update tradinginstance
	set			
		InstanceCode		= @InstanceCode
		,MonitorUnitId		= @MonitorUnitId
		,StockDirection		= @StockDirection	
		,FuturesContract	= @FuturesContract	
		,FuturesDirection	= @FuturesDirection
		,OperationCopies	= @OperationCopies
		,StockPriceType		= @StockPriceType
		,FuturesPriceType	= @FuturesPriceType
		,Status				= @Status
		,Owner				= @Owner	
		,ModifiedDate		= @ModifiedDate	
	where InstanceId=@InstanceId
end

go
if exists (select name from sysobjects where name='procTradingInstanceSelect')
drop proc procTradingInstanceSelect

go
create proc procTradingInstanceSelect(
	@InstanceId	int = NULL
)
as
begin
	if @InstanceId is not null or @InstanceId > 0
	begin
		select
			InstanceId			
			,InstanceCode
			,PortfolioId		
			,MonitorUnitId		
			,StockDirection		
			,FuturesContract	
			,FuturesDirection	
			,OperationCopies	
			,StockPriceType		
			,FuturesPriceType	
			,Status
			,Owner				
			,CreatedDate		
			,ModifiedDate		
		from tradinginstance
		where InstanceId=@InstanceId
	end
	else
	begin
		select
			InstanceId			
			,InstanceCode	
			,PortfolioId	
			,MonitorUnitId		
			,StockDirection		
			,FuturesContract	
			,FuturesDirection	
			,OperationCopies	
			,StockPriceType		
			,FuturesPriceType
			,Status	
			,Owner				
			,CreatedDate		
			,ModifiedDate		
		from tradinginstance
	end
end

go
if exists (select name from sysobjects where name='procTradingInstanceDelete')
drop proc procTradingInstanceDelete

go
create proc procTradingInstanceDelete(
	@InstanceId	int = NULL
)
as
begin
	delete from tradinginstance where InstanceId=@InstanceId
end

go
if exists (select name from sysobjects where name='procTradingInstanceSelectByCode')
drop proc procTradingInstanceSelectByCode

go
create proc procTradingInstanceSelectByCode(
	@InstanceCode varchar(20)
)
as
begin
	select 
		InstanceId			
		,InstanceCode	
		,PortfolioId	
		,MonitorUnitId		
		,StockDirection		
		,FuturesContract	
		,FuturesDirection	
		,OperationCopies	
		,StockPriceType		
		,FuturesPriceType	
		,Status				
		,Owner				
		,CreatedDate		
		,ModifiedDate		
	from tradinginstance
	where InstanceCode=@InstanceCode
end

go
if exists (select name from sysobjects where name='procTradingInstanceExist')
drop proc procTradingInstanceExist

go
create proc procTradingInstanceExist(
	@InstanceCode varchar(20)
)
as
begin
	declare @total int
	set @total = (select count(InstanceId)		
					from tradinginstance
					where InstanceCode=@InstanceCode)
	return @total
end

go
if exists (select name from sysobjects where name='procTradingInstanceSelectCombine')
drop proc procTradingInstanceSelectCombine

go
create proc procTradingInstanceSelectCombine(
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
			,b.PortfolioCode
			,b.PortfolioName
			,b.AccountCode
			,b.AccountName
			,b.AssetNo
			,b.AssetName
			,c.MonitorUnitName	
			,d.TemplateId
			,d.TemplateName
		from tradinginstance a
		inner join ufxportfolio b
		on a.PortfolioId=b.PortfolioId
		inner join monitorunit c
		on a.MonitorUnitId = c.MonitorUnitId
		inner join stocktemplate d
		on c.StockTemplateId = d.TemplateId
		where a.InstanceId=@InstanceId
	end
	else
	begin
		select
			a.InstanceId			
			,a.InstanceCode	
			,a.PortfolioId	
			,a.MonitorUnitId		
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
			,b.PortfolioCode
			,b.PortfolioName
			,b.AccountCode
			,b.AccountName
			,b.AssetNo
			,b.AssetName
			,c.MonitorUnitName	
			,d.TemplateId
			,d.TemplateName
		from tradinginstance a
		inner join ufxportfolio b
		on a.PortfolioId=b.PortfolioId
		inner join monitorunit c
		on a.MonitorUnitId = c.MonitorUnitId
		inner join stocktemplate d
		on c.StockTemplateId = d.TemplateId
	end
end

go
if exists (select name from sysobjects where name='procTradingInstanceSelectCombineByCode')
drop proc procTradingInstanceSelectCombineByCode

go
create proc procTradingInstanceSelectCombineByCode(
	@InstanceCode varchar(20)
)
as
begin
	select
		a.InstanceId			
		,a.InstanceCode	
		,a.PortfolioId	
		,a.MonitorUnitId		
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
		,b.PortfolioCode
		,b.PortfolioName
		,b.AccountCode
		,b.AccountName
		,b.AssetNo
		,b.AssetName
		,c.MonitorUnitName	
		,d.TemplateId
		,d.TemplateName
	from tradinginstance a
	inner join ufxportfolio b
	on a.PortfolioId=b.PortfolioId
	inner join monitorunit c
	on a.MonitorUnitId = c.MonitorUnitId
	inner join stocktemplate d
	on c.StockTemplateId = d.TemplateId
	where a.InstanceCode=@InstanceCode
end