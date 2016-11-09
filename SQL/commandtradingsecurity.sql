use tradingsystem


if object_id('tradingcommandsecurity') is not null
drop table tradingcommandsecurity

create table tradingcommandsecurity(
	CommandId			int not null			--指令序号
	,SecuCode			varchar(10) not null	--证券交易所代码
	,SecuType			int						--证券类型 1 - 指数， 2 - 股票， 3 - 股指期货
	,CommandAmount		int						--指令数量
	,CommandDirection	int						--指令方向：10 - 买入股票， 11 - 卖出股票， 12 - 卖出开仓股指期货， 13 - 买入平仓股指期货
	,CommandPrice		numeric(20, 4)			--如果不限价，则价格设置为0
	,EntrustStatus		int						--委托状态：0 - 提交到数据库， 1 - 提交到UFX，2 - 未执行，3 - 部分执行，4 - 已完成， 10 - 撤单， 11 - 撤单到UFX, 12 - 撤单成功， -4 - 委托失败，-12 - 撤单失败
)

--====================================
--tradingcommandsecurity table
--====================================
go
if exists (select name from sysobjects where name='procTradingCommandSecurityInsert')
drop proc procTradingCommandSecurityInsert

go
create proc procTradingCommandSecurityInsert(
	@CommandId			int 
	,@SecuCode			varchar(10) 
	,@SecuType			int
	,@CommandAmount		int
	,@CommandDirection	int
	,@CommandPrice		numeric(20, 4) --如果不限价，则价格设置为0
)
as
begin
	insert into tradingcommandsecurity(
		CommandId			
		,SecuCode			
		,SecuType		
		,CommandAmount	
		,CommandDirection	
		,CommandPrice		
		,EntrustStatus		
	)values(
		@CommandId			
		,@SecuCode			
		,@SecuType	
		,@CommandAmount	
		,@CommandDirection
		,@CommandPrice		
		,1		
	)
end

go
if exists (select name from sysobjects where name='procTradingCommandSecurityUpdate')
drop proc procTradingCommandSecurityUpdate

go
create proc procTradingCommandSecurityUpdate(
	@CommandId			int 
	,@SecuCode			varchar(10) 
	,@SecuType			int
	,@CommandAmount		int
	,@CommandDirection	int
	,@CommandPrice		numeric(20, 4) --如果不限价，则价格设置为0
)
as
begin
	update tradingcommandsecurity
	set CommandAmount = @CommandAmount
		,CommandDirection = @CommandDirection
		,CommandPrice = @CommandPrice
	where CommandId = @CommandId
		and SecuCode = @SecuCode
		and SecuType = @SecuType
end
--go
--if exists (select name from sysobjects where name='procTradingCommandSecurityUpdateEntrustAmount')
--drop proc procTradingCommandSecurityUpdateEntrustAmount

--go
--create proc procTradingCommandSecurityUpdateEntrustAmount(
--	@CommandId			int 
--	,@SecuCode			varchar(10) 
--	,@EntrustedAmount	int
--)
--as
--begin
--	update tradingcommandsecurity
--	set EntrustedAmount=@EntrustedAmount,
--		EntrustStatus=
--		case 
--			when CommandAmount=@EntrustedAmount then 3 -- 已完成
--			else 2 -- 部分执行
--		end
--	where CommandId=@CommandId and SecuCode=@SecuCode
--end

go
if exists (select name from sysobjects where name='procTradingCommandSecurityDelete')
drop proc procTradingCommandSecurityDelete

go
create proc procTradingCommandSecurityDelete(
	@CommandId			int 
	,@SecuCode			varchar(10) 
)
as
begin
	delete from tradingcommandsecurity where CommandId=@CommandId and SecuCode=@SecuCode
end

go
if exists (select name from sysobjects where name='procTradingCommandSecuritySelect')
drop proc procTradingCommandSecuritySelect

go
create proc procTradingCommandSecuritySelect(
	@CommandId	int 
)
as
begin
	select
		CommandId		
		,SecuCode		
		,SecuType	
		,CommandAmount
		,CommandDirection		
		,CommandPrice	
		,EntrustStatus	
	from tradingcommandsecurity 
	where CommandId=@CommandId
end
