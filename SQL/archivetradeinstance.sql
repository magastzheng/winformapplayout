use tradingsystem

go
if exists (select name from sysobjects where name='procArchiveTradeInstanceInsert')
drop proc procArchiveTradeInstanceInsert

go
create proc procArchiveTradeInstanceInsert(
	@InstanceId			int
	,@InstanceCode		varchar(20)
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
	,@ModifiedDate		datetime
	,@ArchiveDate		datetime
)
as
begin
	declare @newid int
	insert into archivetradeinstance(
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
		,ArchiveDate			
	)
	values(
		@InstanceId
		,@InstanceCode	
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
		,@ModifiedDate
		,@ArchiveDate
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end

go
if exists (select name from sysobjects where name='procArchiveTradeInstanceDeleteByArchiveId')
drop proc procArchiveTradeInstanceDeleteByArchiveId

go
create proc procArchiveTradeInstanceDeleteByArchiveId(
	@ArchiveId	int
)
as
begin
	delete from archivetradeinstance
	where ArchiveId=@ArchiveId
end

go
if exists (select name from sysobjects where name='procArchiveTradeInstanceDeleteByInstanceId')
drop proc procArchiveTradeInstanceDeleteByInstanceId

go
create proc procArchiveTradeInstanceDeleteByInstanceId(
	@InstanceId	int
)
as
begin
	delete from archivetradeinstance
	where InstanceId=@InstanceId
end

go
if exists (select name from sysobjects where name='procArchiveTradeInstanceSelect')
drop proc procArchiveTradeInstanceSelect

go
create proc procArchiveTradeInstanceSelect(
	@ArchiveId		int
)
as
begin
	select
		ArchiveId
		,InstanceId
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
		,ArchiveDate	
	from archivetradeinstance
	where ArchiveId=@ArchiveId
end