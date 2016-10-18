use tradingsystem

if object_id('tradinginstanceadjustment') is not null
drop table tradinginstanceadjustment

create table tradinginstanceadjustment(
	Id int identity(1, 1) primary key	--序号
	,CreateDate datetime
	,SourceInstanceId	int
	,SourceFundCode	varchar(20)
	,SourcePortfolioCode varchar(20)
	,DestinationInstanceId int
	,DestinationFundCode varchar(20)
	,DestinationPortfolioCode varchar(20)
	,SecuCode varchar(10)
	,SecuType int
	,PositionType int
	,Price	decimal(20, 4)
	,Amount int
	,AdjustType int
	,Operator varchar(20)
	,StockHolderId varchar(20)
	,SeatNo	varchar(20)
	,Notes varchar(100)
)

go
if exists (select name from sysobjects where name='procTradingInstanceAdjustmentInsert')
drop proc procTradingInstanceAdjustmentInsert

go
create proc procTradingInstanceAdjustmentInsert(
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

	insert into tradinginstanceadjustment(
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
if exists (select name from sysobjects where name='procTradingInstanceAdjustmentSelect')
drop proc procTradingInstanceAdjustmentSelect

go
create proc procTradingInstanceAdjustmentSelect
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
	from tradinginstanceadjustment
end

go
create proc procTradingInstanceAdjustmentDelete(
	@Id int
)
as
begin
	if @Id is not null and @Id > 0
	begin
		delete from tradinginstanceadjustment
		where Id=@Id
	end
	else
	begin
		truncate table tradinginstanceadjustment
	end
end