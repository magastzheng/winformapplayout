use tradingsystem

--==通过交易系统委托之后，将委托指令添加到本表，由于可以分多次进行委托
if object_id('entrustcommand') is not null
drop table entrustcommand

create table entrustcommand(
	SubmitId		int identity(1, 1) primary key	-- 指令提交ID,每次通过界面委托都会产生唯一的一个ID
	,CommandId		int not null					-- 指令ID
	,Copies			int								--指令份数
	,EntrustNo		int								--委托之后，服务器返回的委托号
	,BatchNo		int								--委托之后，服务器返回的批号
	,EntrustStatus	int								--委托状态	 4-已完成
	,DealStatus		int								--成交状态   1-未成交，2-部分成交，3-已完成
	,SubmitPerson	int								--提交人
	,CreatedDate	datetime						--提交时间	
	,ModifiedDate	datetime						--修改时间	
	,EntrustFailCode	int							--委托错误码
	,EntrustFailCause	varchar(128)				--委托失败原因
)

go
if exists (select name from sysobjects where name='procEntrustCommandInsert')
drop proc procEntrustCommandInsert

go
create proc procEntrustCommandInsert(
	@CommandId		int
	,@Copies		int
	,@SubmitPerson	int
	,@CreatedDate	datetime
)
as
begin
	declare @newid int
	insert into entrustcommand(
		CommandId
		,Copies
		,EntrustNo
		,BatchNo
		,EntrustStatus
		,DealStatus
		,SubmitPerson
		,CreatedDate
		,EntrustFailCode
		,EntrustFailCause
	)
	values(
		@CommandId
		,@Copies
		,-1
		,-1
		,0
		,1
		,@SubmitPerson
		,@CreatedDate
		,0
		,NULL
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end

--go
--if exists (select name from sysobjects where name='procEntrustCommandUpdate')
--drop proc procEntrustCommandUpdate

--go
--create proc procEntrustCommandUpdate(
--	@SubmitId		int
--	,@EntrustNo		int
--	,@BatchNo		int
--	,@EntrustStatus	int
--	,@DealStatus	int
--	,@ModifiedDate	datetime
--	,@EntrustFailCode int
--	,@EntrustFailCause varchar(128)
--)
--as
--begin
--	update entrustcommand
--	set EntrustNo		= @EntrustNo
--		,BatchNo		= @BatchNo
--		,EntrustStatus	= @EntrustStatus
--		,DealStatus		= @DealStatus
--		,ModifiedDate	= @ModifiedDate
--		,EntrustFailCode = @EntrustFailCode
--		,EntrustFailCause = @EntrustFailCause
--	where SubmitId=@SubmitId
--end

go
if exists (select name from sysobjects where name='procEntrustCommandUpdateBatchNo')
drop proc procEntrustCommandUpdateBatchNo

go
create proc procEntrustCommandUpdateBatchNo(
	@SubmitId		int
	,@BatchNo		int
	,@EntrustStatus	int
	,@ModifiedDate	datetime
	,@EntrustFailCode	int
	,@EntrustFailCause varchar(128)
)
as
begin
	update entrustcommand
	set BatchNo		= @BatchNo
		,EntrustStatus	= @EntrustStatus
		,ModifiedDate	= @ModifiedDate
		,EntrustFailCode = @EntrustFailCode
		,EntrustFailCause = @EntrustFailCause
	where SubmitId=@SubmitId
end

go
if exists (select name from sysobjects where name='procEntrustCommandUpdateDealStatus')
drop proc procEntrustCommandUpdateDealStatus

go
create proc procEntrustCommandUpdateDealStatus(
	@SubmitId			int
	,@DealStatus		int
	,@ModifiedDate		datetime
)
as
begin
	--declare @newid int
	update entrustcommand
	set DealStatus		= @DealStatus
		,ModifiedDate	= @ModifiedDate
	where SubmitId=@SubmitId
end


go
if exists (select name from sysobjects where name='procEntrustCommandUpdateEntrustStatus')
drop proc procEntrustCommandUpdateEntrustStatus

go
create proc procEntrustCommandUpdateEntrustStatus(
	@SubmitId			int
	,@EntrustStatus		int
	,@ModifiedDate		datetime
)
as
begin
	--declare @newid int
	update entrustcommand
	set EntrustStatus	= @EntrustStatus
		,ModifiedDate	= @ModifiedDate
	where SubmitId=@SubmitId
end

--go
--if exists (select name from sysobjects where name='procEntrustCommandUpdateCancel')
--drop proc procEntrustCommandUpdateCancel

--go
--create proc procEntrustCommandUpdateCancel(
--	@CommandId			int
--	,@ModifiedDate		datetime
--)
--as
--begin
--	--declare @newid int
--	update entrustcommand
--	set EntrustStatus	= 10
--		,ModifiedDate	= @ModifiedDate
--	where CommandId=@CommandId
--		and (DealStatus = 1 or DealStatus = 2)		--未成交
--		and EntrustStatus = 4	--已完成
--end

--go
--if exists (select name from sysobjects where name='procEntrustCommandDeleteBySubmitId')
--drop proc procEntrustCommandDeleteBySubmitId

--go
--create proc procEntrustCommandDeleteBySubmitId(
--	@SubmitId		int
--)
--as
--begin
--	delete from entrustcommand
--	where SubmitId=@SubmitId
--end

go
if exists (select name from sysobjects where name='procEntrustCommandDeleteByCommandId')
drop proc procEntrustCommandDeleteByCommandId

go
create proc procEntrustCommandDeleteByCommandId(
	@CommandId		int
)
as
begin
	delete from entrustcommand
	where CommandId=@CommandId
end

go
if exists (select name from sysobjects where name='procEntrustCommandDeleteBySubmitId')
drop proc procEntrustCommandDeleteBySubmitId

go
create proc procEntrustCommandDeleteBySubmitId(
	@SubmitId		int
)
as
begin
	delete from entrustcommand
	where SubmitId=@SubmitId
end

go
if exists (select name from sysobjects where name='procEntrustCommandDeleteByCommandIdEntrustStatus')
drop proc procEntrustCommandDeleteByCommandIdEntrustStatus

go
create proc procEntrustCommandDeleteByCommandIdEntrustStatus(
	@CommandId		int
	,@EntrustStatus	int
)
as
begin
	delete from entrustcommand
	where CommandId=@CommandId and EntrustStatus=@EntrustStatus
end

--go
--if exists (select name from sysobjects where name='procEntrustCommandSelectBySubmitId')
--drop proc procEntrustCommandSelectBySubmitId

--go
--create proc procEntrustCommandSelectBySubmitId(
--	@SubmitId		int
--)
--as
--begin
--	select SubmitId
--		  ,CommandId
--		  ,Copies
--		  ,EntrustNo
--		  ,BatchNo
--		  ,EntrustStatus
--		  ,DealStatus
--		  ,CreatedDate
--		  ,ModifiedDate
--		  ,EntrustFailCode
--		  ,EntrustFailCause
--	from entrustcommand
--	where SubmitId=@SubmitId
--end

--go
--if exists (select name from sysobjects where name='procEntrustCommandSelectByCommandId')
--drop proc procEntrustCommandSelectByCommandId

--go
--create proc procEntrustCommandSelectByCommandId(
--	@CommandId		int
--)
--as
--begin
--	select SubmitId
--		  ,CommandId
--		  ,Copies
--		  ,EntrustNo
--		  ,BatchNo
--		  ,EntrustStatus
--		  ,DealStatus
--		  ,CreatedDate
--		  ,ModifiedDate
--		  ,EntrustFailCode
--		  ,EntrustFailCause
--	from entrustcommand
--	where CommandId=@CommandId
--end


--go
--if exists (select name from sysobjects where name='procEntrustCommandSelectAll')
--drop proc procEntrustCommandSelectAll

--go
--create proc procEntrustCommandSelectAll
--as
--begin
--	select SubmitId
--		  ,CommandId
--		  ,Copies
--		  ,EntrustNo
--		  ,BatchNo
--		  ,EntrustStatus
--		  ,DealStatus
--		  ,CreatedDate
--		  ,ModifiedDate
--		  ,EntrustFailCode
--		  ,EntrustFailCause
--	from entrustcommand
--end


--go
--if exists (select name from sysobjects where name='procEntrustCommandSelectByCommandIdEntrustStatus')
--drop proc procEntrustCommandSelectByCommandIdEntrustStatus

--go
--create proc procEntrustCommandSelectByCommandIdEntrustStatus(
--	@CommandId		int
--	,@EntrustStatus	int
--)
--as
--begin
--	select SubmitId
--		  ,CommandId
--		  ,Copies
--		  ,EntrustNo
--		  ,BatchNo
--		  ,EntrustStatus
--		  ,DealStatus
--		  ,CreatedDate
--		  ,ModifiedDate
--		  ,EntrustFailCode
--		  ,EntrustFailCause
--	from entrustcommand
--	where CommandId = @CommandId 
--		and EntrustStatus=@EntrustStatus
--end

go
if exists (select name from sysobjects where name='procEntrustCommandSelectByCommandId')
drop proc procEntrustCommandSelectByCommandId

go
create proc procEntrustCommandSelectByCommandId(
	@CommandId int
)
as
begin
	select SubmitId
		,CommandId
		,Copies
		,EntrustNo
		,BatchNo
		,EntrustStatus
		,DealStatus
		,SubmitPerson
		,CreatedDate
		,ModifiedDate
		,EntrustFailCode
		,EntrustFailCause
	from entrustcommand
	where CommandId = @CommandId 
end

go
if exists (select name from sysobjects where name='procEntrustCommandSelectCancel')
drop proc procEntrustCommandSelectCancel

go
create proc procEntrustCommandSelectCancel(
	@CommandId int
)
as
begin
	select SubmitId
		,CommandId
		,Copies
		,EntrustNo
		,BatchNo
		,EntrustStatus
		,DealStatus
		,SubmitPerson
		,CreatedDate
		,ModifiedDate
		,EntrustFailCode
		,EntrustFailCause
	from entrustcommand
	where CommandId = @CommandId 
		and (DealStatus=1 or DealStatus=2)		--未成交或部分成交
		and EntrustStatus=4		--仅对已委托完成的撤单
end

--go
--if exists (select name from sysobjects where name='procEntrustCommandSelectCancelCompletedRedo')
--drop proc procEntrustCommandSelectCancelCompletedRedo

--go
--create proc procEntrustCommandSelectCancelCompletedRedo(
--	@CommandId int
--)
--as
--begin
--	select SubmitId
--		  ,CommandId
--		  ,Copies
--		  ,EntrustNo
--		  ,BatchNo
--		  ,EntrustStatus
--		  ,DealStatus
--		  ,CreatedDate
--		  ,ModifiedDate
--		  ,EntrustFailCode
--		  ,EntrustFailCause
--	from entrustcommand
--	where CommandId = @CommandId 
--		and (DealStatus = 1 		--未成交
--		or DealStatus = 2)		--部分成交
--		and EntrustStatus = 12	--已完成撤销
--end