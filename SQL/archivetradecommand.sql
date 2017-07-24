use tradingsystem

go

if exists (select name from sysobjects where name='procArchiveTradeCommandInsert')
drop proc procArchiveTradeCommandInsert

go
create proc procArchiveTradeCommandInsert(
	@CommandId			int
	,@InstanceId		int
	,@CommandNum		int				
	,@ModifiedTimes		int				
	,@CommandType		int				
	,@ExecuteType		int				
	,@StockDirection	int				
	,@FuturesDirection	int				
	,@CommandStatus		int
	,@DispatchStatus	int				
	,@EntrustStatus		int				
	,@DealStatus		int				
	,@SubmitPerson		int				
	,@ModifiedPerson	int				
	,@CancelPerson		int				
	,@ApprovalPerson	int				
	,@DispatchPerson	int				
	,@ExecutePerson		int				
	,@CreatedDate		datetime		
	,@ModifiedDate		datetime		
	,@ArchiveDate		datetime
	,@StartDate			datetime		
	,@EndDate			datetime		
	,@ModifiedCause		varchar(100)	
	,@CancelCause		varchar(100)	
	,@ApprovalCause		varchar(100)	
	,@DispatchRejectCause	varchar(100)
	,@Notes		varchar(100)	
)
as
begin
	declare @newid int

	insert into archivetradecommand(
		CommandId			
		,InstanceId		
		,CommandNum		
		,ModifiedTimes		
		,CommandType		
		,ExecuteType		
		,StockDirection	
		,FuturesDirection	
		,CommandStatus		
		,DispatchStatus	
		,EntrustStatus		
		,DealStatus		
		,SubmitPerson		
		,ModifiedPerson	
		,CancelPerson		
		,ApprovalPerson	
		,DispatchPerson	
		,ExecutePerson		
		,CreatedDate		
		,ModifiedDate	
		,ArchiveDate	
		,StartDate			
		,EndDate			
		,ModifiedCause		
		,CancelCause		
		,ApprovalCause		
		,DispatchRejectCause
		,Notes		
	)
	values(
		@CommandId			
		,@InstanceId		
		,@CommandNum		
		,@ModifiedTimes		
		,@CommandType		
		,@ExecuteType		
		,@StockDirection	
		,@FuturesDirection	
		,@CommandStatus		
		,@DispatchStatus	
		,@EntrustStatus		
		,@DealStatus		
		,@SubmitPerson		
		,@ModifiedPerson	
		,@CancelPerson		
		,@ApprovalPerson	
		,@DispatchPerson	
		,@ExecutePerson		
		,@CreatedDate		
		,@ModifiedDate	
		,@ArchiveDate	
		,@StartDate			
		,@EndDate			
		,@ModifiedCause		
		,@CancelCause		
		,@ApprovalCause		
		,@DispatchRejectCause
		,@Notes		
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end

go

if exists (select name from sysobjects where name='procArchiveTradeCommandDelete')
drop proc procArchiveTradeCommandDelete

go
create proc procArchiveTradeCommandDelete(
	@ArchiveId	int	= NULL	--两个参数必须传入一个
	,@CommandId	int = NULL
)
as
begin
	if @ArchiveId is not null
	begin
		delete from archivetradecommand
		where ArchiveId=@ArchiveId
	end
	else if @CommandId is not null
	begin
		delete from archivetradecommand
		where CommandId=@CommandId
	end
	else
	begin
		raiserror('The parameter ArchiveId and CommandId are not NULL. It needs to pass one.', 16, -1)
	end
end

go

if exists (select name from sysobjects where name='procArchiveTradeCommandSelect')
drop proc procArchiveTradeCommandSelect

go
create proc procArchiveTradeCommandSelect(
	@ArchiveId	int
)
as
begin
	select
		ArchiveId
		,CommandId			
		,InstanceId		
		,CommandNum		
		,ModifiedTimes		
		,CommandType		
		,ExecuteType		
		,StockDirection	
		,FuturesDirection	
		,CommandStatus		
		,DispatchStatus	
		,EntrustStatus		
		,DealStatus		
		,SubmitPerson		
		,ModifiedPerson	
		,CancelPerson		
		,ApprovalPerson	
		,DispatchPerson	
		,ExecutePerson		
		,CreatedDate		
		,ModifiedDate	
		,ArchiveDate	
		,StartDate			
		,EndDate			
		,ModifiedCause		
		,CancelCause		
		,ApprovalCause		
		,DispatchRejectCause
		,Notes	
	from archivetradecommand
	where ArchiveId=@ArchiveId
end