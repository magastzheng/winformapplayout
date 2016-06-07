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
	,AvailableAmount	int				--可用数量
	,InstructionPreBuy	int				--指令预买
	,InstructionPreSell	int				--指令预卖
	,Cost				numeric(20, 4)	--成本
	,BuyToday			int				--当日买量
	,constraint pk_TradingInstanceSecurity_IdSecuCode primary key(InstanceId, SecuCode)
)

go
if exists (select name from sysobjects where name='procTradingInstanceSecurityInsert')
drop proc procTradingInstanceSecurityInsert

go
create proc procTradingInstanceSecurityInsert(
	@InstanceId			int
	,@SecuCode			varchar(10)
	,@SecuType			int
	,@PositionType		int
	--,@PositionAmount	int
	--,@AvailableAmount	int
	,@InstructionPreBuy	int
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
	)
	values(@InstanceId
			,@SecuCode
			,@SecuType
			,@PositionType
			,@InstructionPreBuy
			,@InstructionPreSell
		)

	set @RowId=@SecuCode+';'+cast(@InstanceId as varchar)
end

go
if exists (select name from sysobjects where name='procTradingInstanceSecurityUpdatePosition')
drop proc procTradingInstanceSecurityUpdatePosition

go
create proc procTradingInstanceSecurityUpdatePosition(
	@InstanceId			int
	,@SecuCode			varchar(10)
	,@PositionAmount	int
	,@AvailableAmount	int
)
as
begin
	--declare @newid varchar(20)
	update tradinginstancesecurity
	set
		PositionAmount = @PositionAmount
		,AvailableAmount = @AvailableAmount
	where InstanceId=@InstanceId and SecuCode=@SecuCode
end

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
		   ,AvailableAmount
		   ,InstructionPreBuy
		   ,InstructionPreSell
	from tradinginstancesecurity
	where InstanceId=@InstanceId
end

