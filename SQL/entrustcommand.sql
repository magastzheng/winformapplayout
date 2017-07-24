use tradingsystem


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

go
if exists (select name from sysobjects where name='procEntrustCommandSelectBySubmitId')
drop proc procEntrustCommandSelectBySubmitId

go
create proc procEntrustCommandSelectBySubmitId(
	@SubmitId		int
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
	where SubmitId=@SubmitId
end

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
