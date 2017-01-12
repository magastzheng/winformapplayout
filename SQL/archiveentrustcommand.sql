use tradingsystem

--==通过交易系统委托之后，将委托指令添加到本表，由于可以分多次进行委托
if object_id('archiveentrustcommand') is not null
drop table archiveentrustcommand

create table archiveentrustcommand(
	ArchiveId		int identity(1, 1) primary key
	,SubmitId		int not null	-- 指令提交ID,每次通过界面委托都会产生唯一的一个ID
	,CommandId		int not null					-- 指令ID
	,Copies			int								--指令份数
	,EntrustNo		int								--委托之后，服务器返回的委托号
	,BatchNo		int								--委托之后，服务器返回的批号
	,EntrustStatus	int								--委托状态
	,DealStatus		int								--成交状态
	,SubmitPerson	int								--提交人
	,ArchiveDate	datetime						--归档时间
	,CreatedDate	datetime						--提交时间	
	,ModifiedDate	datetime						--修改时间	
	,EntrustFailCode	int							--委托错误码
	,EntrustFailCause	varchar(128)				--委托失败原因
)

go
if exists (select name from sysobjects where name='procArchiveEntrustCommandInsert')
drop proc procArchiveEntrustCommandInsert

go
create proc procArchiveEntrustCommandInsert(
	@SubmitId		int
	,@CommandId		int
	,@Copies		int
	,@EntrustNo		int
	,@BatchNo		int
	,@EntrustStatus	int
	,@DealStatus	int
	,@SubmitPerson	int
	,@ArchiveDate	datetime
	,@CreatedDate	datetime
	,@ModifiedDate	datetime
	,@EntrustFailCode	int
	,@EntrustFailCause	varchar(128)
)
as
begin
	declare @newid int
	insert into archiveentrustcommand(
		SubmitId
		,CommandId
		,Copies
		,EntrustNo
		,BatchNo
		,EntrustStatus
		,DealStatus
		,SubmitPerson
		,ArchiveDate
		,CreatedDate
		,ModifiedDate
		,EntrustFailCode
		,EntrustFailCause
	)
	values(
		@SubmitId
		,@CommandId
		,@Copies
		,@EntrustNo
		,@BatchNo
		,@EntrustStatus
		,@DealStatus
		,@SubmitPerson
		,@ArchiveDate
		,@CreatedDate
		,@ModifiedDate
		,@EntrustFailCode
		,@EntrustFailCause
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end

go
if exists (select name from sysobjects where name='procArchiveEntrustCommandDelete')
drop proc procArchiveEntrustCommandDelete

go
create proc procArchiveEntrustCommandDelete(
	@ArchiveId	int
)
as
begin
	delete from archiveentrustcommand
	where ArchiveId=@ArchiveId
end

go
if exists (select name from sysobjects where name='procArchiveEntrustCommandSelect')
drop proc procArchiveEntrustCommandSelect

go
create proc procArchiveEntrustCommandSelect(
	@CommandId	int
)
as
begin
	select
		ArchiveId
		,SubmitId
		,CommandId
		,Copies
		,EntrustNo
		,BatchNo
		,EntrustStatus
		,DealStatus
		,SubmitPerson
		,ArchiveDate
		,CreatedDate
		,ModifiedDate
		,EntrustFailCode
		,EntrustFailCause
	from archiveentrustcommand
	where CommandId=@CommandId
end

go
if exists (select name from sysobjects where name='procArchiveEntrustCommandSelectBySubmitId')
drop proc procArchiveEntrustCommandSelectBySubmitId

go
create proc procArchiveEntrustCommandSelectBySubmitId(
	@SubmitId	int
)
as
begin
	select
		ArchiveId
		,SubmitId
		,CommandId
		,Copies
		,EntrustNo
		,BatchNo
		,EntrustStatus
		,DealStatus
		,SubmitPerson
		,ArchiveDate
		,CreatedDate
		,ModifiedDate
		,EntrustFailCode
		,EntrustFailCause
	from archiveentrustcommand
	where SubmitId=@SubmitId
end