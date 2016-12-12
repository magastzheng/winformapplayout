use tradingsystem

if object_id('archivetradecommand') is not null
drop table archivetradecommand

create table archivetradecommand(
	ArchiveId			int identity(1, 1) primary key
	,CommandId			int not null					--指令序号
	,InstanceId			int not null					--交易实例ID
	,CommandNum			int								--指令份数
	,ModifiedTimes		int								--修改次数
	,CommandType		int								-- 1 - 期现套利
	,ExecuteType		int								-- 1 开仓， 2 - 平仓
	,StockDirection		int								--10 -- 买入现货，11--卖出现货
	,FuturesDirection	int								--12-卖出开仓，13 -买入平仓
	,CommandStatus		int not null					--指令状态：1 - 有效指令，2 - 已修改，3 - 已撤销, 4 - 委托完成， 5 - 已完成成交
	,DispatchStatus		int								--分发状态：1 - 未分发，2 - 已分发
	,EntrustStatus		int								-- 1 - 未执行， 2 - 部分执行， 3- 已完成
	,DealStatus			int								-- 1 - 未成交， 2 - 部分成交， 3 - 已完成
	,SubmitPerson		int								--下达人
	,ModifiedPerson		int								--修改人
	,CancelPerson		int								--撤销人
	,ApprovalPerson		int								--审批人
	,DispatchPerson		int								--分发人
	,ExecutePerson		int								--执行人
	,CreatedDate		datetime						-- 下达指令时间
	,ModifiedDate		datetime						-- 修改时间
	,ArchiveDate		datetime						-- 归档时间
	,StartDate			datetime						-- 指令有效开始时间
	,EndDate			datetime						-- 指令有效结束时间
	,ModifiedCause		varchar(100)					-- 修改指令原因
	,CancelCause		varchar(100)					-- 撤销指令原因
	,ApprovalCause		varchar(100)					-- 审批原因
	,DispatchRejectCause	varchar(100)				-- 分发拒绝原因
	,Notes		varchar(100)					-- 指令备注
)

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