use tradingsystem

if object_id('tradinginstanceadjustment') is not null
drop table tradinginstanceadjustment

create table tradinginstanceadjustment(
	Id int identity(1, 1) primary key	--序号
	,Date datetime
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