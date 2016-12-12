use tradingsystem


if object_id('archivetradecommandsecurity') is not null
drop table archivetradecommandsecurity

create table archivetradecommandsecurity(
	ArchiveId			int not null			--表archivetradecommand中的ArchiveId
	,CommandId			int not null			--指令序号
	,SecuCode			varchar(10) not null	--证券交易所代码
	,SecuType			int						--证券类型 1 - 指数， 2 - 股票， 3 - 股指期货
	,CommandAmount		int						--指令数量
	,CommandDirection	int						--指令方向：10 - 买入股票， 11 - 卖出股票， 12 - 卖出开仓股指期货， 13 - 买入平仓股指期货
	,CommandPrice		numeric(20, 4)			--如果不限价，则价格设置为0
	,EntrustStatus		int						--委托状态：0 - 提交到数据库， 1 - 提交到UFX，2 - 未执行，3 - 部分执行，4 - 已完成， 10 - 撤单， 11 - 撤单到UFX, 12 - 撤单成功， -4 - 委托失败，-12 - 撤单失败
)

--====================================
--archivetradecommandsecurity table
--====================================
go
if exists (select name from sysobjects where name='procArchiveTradeCommandSecurityInsert')
drop proc procArchiveTradeCommandSecurityInsert

go
create proc procArchiveTradeCommandSecurityInsert(
	@ArchiveId			int
	,@CommandId			int 
	,@SecuCode			varchar(10) 
	,@SecuType			int
	,@CommandAmount		int
	,@CommandDirection	int
	,@CommandPrice		numeric(20, 4) --如果不限价，则价格设置为0
	,@EntrustStatus		int
)
as
begin
	insert into archivetradecommandsecurity(
		ArchiveId
		,CommandId			
		,SecuCode			
		,SecuType		
		,CommandAmount	
		,CommandDirection	
		,CommandPrice		
		,EntrustStatus		
	)values(
		@ArchiveId
		,@CommandId			
		,@SecuCode			
		,@SecuType	
		,@CommandAmount	
		,@CommandDirection
		,@CommandPrice		
		,@EntrustStatus		
	)
end

go
if exists (select name from sysobjects where name='procArchiveTradeCommandSecurityDelete')
drop proc procArchiveTradeCommandSecurityDelete

go
create proc procArchiveTradeCommandSecurityDelete(
	@ArchiveId	int
	,@CommandId int			= NULL
	,@SecuCode	varchar(10)	= NULL
)
as
begin
	if @CommandId is not null and @SecuCode is not null
	begin
		delete from archivetradecommandsecurity
		where ArchiveId=@ArchiveId
			and CommandId=@CommandId
			and SecuCode=@SecuCode
	end
	else if @CommandId is not null
	begin
		delete from archivetradecommandsecurity
		where ArchiveId=@ArchiveId
			and CommandId=@CommandId
	end
	else if @SecuCode is not null
	begin
		delete from archivetradecommandsecurity
		where ArchiveId=@ArchiveId
			and SecuCode=@SecuCode
	end
	else
	begin
		delete from archivetradecommandsecurity
		where ArchiveId=@ArchiveId
	end
end

go
if exists (select name from sysobjects where name='procArchiveTradeCommandSecuritySelect')
drop proc procArchiveTradeCommandSecuritySelect

go
create proc procArchiveTradeCommandSecuritySelect(
	@ArchiveId	int
	,@CommandId	int			= NULL
	,@SecuCode	varchar(10) = NULL
)
as
begin
	if @CommandId is not null and @SecuCode is not null
	begin
		select
			ArchiveId
			,CommandId		
			,SecuCode		
			,SecuType	
			,CommandAmount
			,CommandDirection		
			,CommandPrice	
			,EntrustStatus	
		from archivetradecommandsecurity 
		where ArchiveId=@ArchiveId 
			and CommandId=@CommandId
			and SecuCode=@SecuCode
	end
	else if @CommandId is not null
	begin
		select
			ArchiveId
			,CommandId		
			,SecuCode		
			,SecuType	
			,CommandAmount
			,CommandDirection		
			,CommandPrice	
			,EntrustStatus	
		from archivetradecommandsecurity 
		where ArchiveId=@ArchiveId 
			and CommandId=@CommandId
	end
	else if @SecuCode is not null
	begin
		select
			ArchiveId
			,CommandId		
			,SecuCode		
			,SecuType	
			,CommandAmount
			,CommandDirection		
			,CommandPrice	
			,EntrustStatus	
		from archivetradecommandsecurity 
		where ArchiveId=@ArchiveId 
			and SecuCode=@SecuCode
	end
	else
	begin
		select
			ArchiveId
			,CommandId		
			,SecuCode		
			,SecuType	
			,CommandAmount
			,CommandDirection		
			,CommandPrice	
			,EntrustStatus	
		from archivetradecommandsecurity 
		where ArchiveId=@ArchiveId 
	end
end
