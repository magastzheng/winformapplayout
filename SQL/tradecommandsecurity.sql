use tradingsystem


if object_id('tradecommandsecurity') is not null
drop table tradecommandsecurity

create table tradecommandsecurity(
	CommandId			int not null			--指令序号
	,SecuCode			varchar(10) not null	--证券交易所代码
	,SecuType			int						--证券类型 1 - 指数， 2 - 股票， 3 - 股指期货
	,CommandAmount		int						--指令数量
	,CommandDirection	int						--指令方向：10 - 买入股票， 11 - 卖出股票， 12 - 卖出开仓股指期货， 13 - 买入平仓股指期货
	,CommandPrice		numeric(20, 4)			--如果不限价，则价格设置为0
	,EntrustStatus		int						--委托状态：0 - 提交到数据库， 1 - 提交到UFX，2 - 未执行，3 - 部分执行，4 - 已完成， 10 - 撤单， 11 - 撤单到UFX, 12 - 撤单成功， -4 - 委托失败，-12 - 撤单失败
)

--====================================
--tradecommandsecurity table
--====================================
go
if exists (select name from sysobjects where name='procTradeCommandSecurityInsert')
drop proc procTradeCommandSecurityInsert

go
create proc procTradeCommandSecurityInsert(
	@CommandId			int 
	,@SecuCode			varchar(10) 
	,@SecuType			int
	,@CommandAmount		int
	,@CommandDirection	int
	,@CommandPrice		numeric(20, 4) --如果不限价，则价格设置为0
)
as
begin
	insert into tradecommandsecurity(
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
if exists (select name from sysobjects where name='procTradeCommandSecurityInsertOrUpdate')
drop proc procTradeCommandSecurityInsertOrUpdate

go
create proc procTradeCommandSecurityInsertOrUpdate(
	@CommandId			int 
	,@SecuCode			varchar(10) 
	,@SecuType			int
	,@CommandAmount		int
	,@CommandDirection	int
	,@CommandPrice		numeric(20, 4) --如果不限价，则价格设置为0
)
as
begin

	declare @Total int

	set @Total=(select count(SecuCode) as Total 
		from tradecommandsecurity 
		where CommandId = @CommandId
		and SecuCode = @SecuCode
		and SecuType = @SecuType)
	
	if @Total = 0
	begin
		insert into tradecommandsecurity(
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
	else
	begin
		update tradecommandsecurity
		set CommandAmount = @CommandAmount
			,CommandDirection = @CommandDirection
			,CommandPrice = @CommandPrice
		where CommandId = @CommandId
			and SecuCode = @SecuCode
			and SecuType = @SecuType
	end
end
--go
--if exists (select name from sysobjects where name='procTradeCommandSecurityUpdateEntrustAmount')
--drop proc procTradeCommandSecurityUpdateEntrustAmount

--go
--create proc procTradeCommandSecurityUpdateEntrustAmount(
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
if exists (select name from sysobjects where name='procTradeCommandSecurityDelete')
drop proc procTradeCommandSecurityDelete

go
create proc procTradeCommandSecurityDelete(
	@CommandId			int 
	,@SecuCode			varchar(10) 
	,@SecuType			int
)
as
begin
	delete from tradecommandsecurity 
	where CommandId=@CommandId 
	and SecuCode=@SecuCode
	and SecuType=@SecuType
end

go
if exists (select name from sysobjects where name='procTradeCommandSecuritySelect')
drop proc procTradeCommandSecuritySelect

go
create proc procTradeCommandSecuritySelect(
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
	from tradecommandsecurity 
	where CommandId=@CommandId
end
