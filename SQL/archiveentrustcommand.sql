use tradingsystem


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