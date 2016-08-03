use tradingsystem


if object_id('tradingcommandsecurity') is not null
drop table tradingcommandsecurity

create table tradingcommandsecurity(
	CommandId			int not null
	,SecuCode			varchar(10) not null
	,SecuType			int
	,CommandAmount		int
	,CommandDirection	int			 --10 - 买入股票， 11 - 卖出股票， 12 - 卖出开仓， 13 - 买入平仓
	,CommandPrice		numeric(20, 4) --如果不限价，则价格设置为0
	,EntrustStatus		int			 --1 - 未执行
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
