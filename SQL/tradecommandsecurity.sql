use tradingsystem

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
	,@CurrentPrice		numeric(20, 4) --最新价
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
		,CurrentPrice	
	)values(
		@CommandId			
		,@SecuCode			
		,@SecuType	
		,@CommandAmount	
		,@CommandDirection
		,@CommandPrice		
		,1		
		,@CurrentPrice
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
	,@CurrentPrice		numeric(20, 4) --最新价格
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
			,CurrentPrice		
		)values(
			@CommandId			
			,@SecuCode			
			,@SecuType	
			,@CommandAmount	
			,@CommandDirection
			,@CommandPrice		
			,1	
			,@CurrentPrice	
		)
	end
	else
	begin
		update tradecommandsecurity
		set CommandAmount = @CommandAmount
			,CommandDirection = @CommandDirection
			,CommandPrice = @CommandPrice
			,CurrentPrice = @CurrentPrice
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
		,CurrentPrice	
	from tradecommandsecurity 
	where CommandId=@CommandId
end
