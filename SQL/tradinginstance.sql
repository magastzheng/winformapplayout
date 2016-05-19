use tradingsystem

if object_id('tradinginstance') is not null
drop table tradinginstance

create table tradinginstance(
	InstanceId			int identity(1, 1) primary key
	,InstanceCode		varchar(20)
	,MonitorUnitId		int
	,StockDirection		int --1 - 买入， 2 - 卖出， 3 - 调整到[买卖]， 4 - 调整到[只买]， 5 - 调整到[只卖] 
	,FuturesContract	varchar(10)
	,FuturesDirection	int --1 -- 多头, 2 - 空头
	,OperationCopies	int
	,StockPriceType		int -- 0 - 不限价，1 - 最新价，A-J盘1至盘10
	,FuturesPriceType	int -- 0 - 不限价，1 - 最新价， A-E盘1到盘5
	,Status				int -- 0 - 无效， 1 - 有效
	,Owner				varchar(10)
	,CreatedDate		datetime
	,ModifiedDate		datetime
)

go
if exists (select name from sysobjects where name='procTradingInstanceInsert')
drop proc procTradingInstanceInsert

go
create proc procTradingInstanceInsert(
	@InstanceCode		varchar(20)
	,@MonitorUnitId		int
	,@StockDirection	int
	,@FuturesContract	varchar(10)
	,@FuturesDirection	int
	,@OperationCopies	int
	,@StockPriceType	int
	,@FuturesPriceType	int
	,@Status				int
	,@Owner				varchar(10)
	,@CreatedDate		datetime
)
as
begin
	declare @newid int
	insert into tradinginstance(
		InstanceCode		
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
	,@Owner				varchar(10)
	,@ModifiedDate		datetime
)
as
begin
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
			,b.MonitorUnitName	
			,c.TemplateName
		from tradinginstance a
		inner join monitorunit b
		on a.MonitorUnitId = b.MonitorUnitId
		inner join stocktemplate c
		on b.StockTemplateId = c.TemplateId
		where a.InstanceId=@InstanceId
	end
	else
	begin
		select
			a.InstanceId			
			,a.InstanceCode		
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
			,b.MonitorUnitName	
			,c.TemplateName
		from tradinginstance a
		inner join monitorunit b
		on a.MonitorUnitId = b.MonitorUnitId
		inner join stocktemplate c
		on b.StockTemplateId = c.TemplateId
	end
end