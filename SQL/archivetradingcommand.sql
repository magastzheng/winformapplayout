use tradingsystem

if object_id('archivetradingcommand') is not null
drop table archivetradingcommand

create table archivetradingcommand(
	ArchiveId			int identity(1, 1) primary key
	,CommandId			int not null					--指令序号
	,InstanceId			int not null					--交易实例ID
	,CommandNum			int								--指令份数
	,ModifiedTimes		int								--修改次数
	,CommandType		int								-- 1 - 期现套利
	,ExecuteType		int								-- 1 开仓， 2 - 平仓
	,StockDirection		int								--10 -- 买入现货，11--卖出现货
	,FuturesDirection	int								--12-卖出开仓，13 -买入平仓
	,CommandStatus		int not null					--指令状态：1 - 有效指令，2 - 已修改，3 - 已撤销
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
	,StartDate			datetime						-- 指令有效开始时间
	,EndDate			datetime						-- 指令有效结束时间
	,ModifiedCause		varchar(100)					-- 修改指令原因
	,CancelCause		varchar(100)					-- 撤销指令原因
	,ApprovalCause		varchar(100)					-- 审批原因
	,DispatchRejectCause	varchar(100)				-- 分发拒绝原因
	,CommandNotes		varchar(100)					-- 指令备注
)

go

if exists (select name from sysobjects where name='procArchiveTradingCommandInsert')
drop proc procArchiveTradingCommandInsert

go
create proc procArchiveTradingCommandInsert(
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
	,@StartDate			datetime		
	,@EndDate			datetime		
	,@ModifiedCause		varchar(100)	
	,@CancelCause		varchar(100)	
	,@ApprovalCause		varchar(100)	
	,@DispatchRejectCause	varchar(100)
	,@CommandNotes		varchar(100)	
)
as
begin
	
	declare @newid int

	insert into archivetradingcommand(
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
		,StartDate			
		,EndDate			
		,ModifiedCause		
		,CancelCause		
		,ApprovalCause		
		,DispatchRejectCause
		,CommandNotes		
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
		,@StartDate			
		,@EndDate			
		,@ModifiedCause		
		,@CancelCause		
		,@ApprovalCause		
		,@DispatchRejectCause
		,@CommandNotes		
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end


go

if exists (select name from sysobjects where name='procArchiveTradingCommandSelect')
drop proc procArchiveTradingCommandSelect

go
create proc procArchiveTradingCommandSelect
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
		,StartDate			
		,EndDate			
		,ModifiedCause		
		,CancelCause		
		,ApprovalCause		
		,DispatchRejectCause
		,CommandNotes	
	from archivetradingcommand
end