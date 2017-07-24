use tradingsystem


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
