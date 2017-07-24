use tradingsystem

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