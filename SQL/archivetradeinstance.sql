use tradingsystem

if object_id('archivetradeinstance') is not null
drop table archivetradeinstance

create table archivetradeinstance(
	ArchiveId			int identity(1, 1) primary key	--归档ID
	,InstanceId			int	not null					--交易实例ID
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
	,ArchiveDate		datetime	--归档时间
	,CreatedDate		datetime	--交易实例创建时间
	,ModifiedDate		datetime	--交易实例修改时间
)


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