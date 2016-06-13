use tradingsystem

--==通过交易系统委托之后，将委托指令添加到本表，由于可以分多次进行委托
if object_id('entrustcommand') is not null
drop table entrustcommand

create table entrustcommand(
	SubmitId		int identity(1, 1) primary key
	,CommandId		int not null
	,Copies			int			--指令份数
	,EntrustNo		int			--委托之后，服务器返回的委托号
	,BatchNo		int			--委托之后，服务器返回的批号
	,Status			int			--0初始状态，1 - 提交完成，2 - 委托完成
	,CreatedDate	datetime
	,ModifiedDate	datetime
)

go
if exists (select name from sysobjects where name='procEntrustCommandInsert')
drop proc procEntrustCommandInsert

go
create proc procEntrustCommandInsert(
	@CommandId		int
	,@Copies		int
	,@CreatedDate	datetime
)
as
begin
	declare @newid int
	insert into entrustcommand(
		CommandId
		,Copies
		,Status
		,CreatedDate
	)
	values(
		@CommandId
		,@Copies
		,0
		,@CreatedDate
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end

go
if exists (select name from sysobjects where name='procEntrustCommandUpdateStatus')
drop proc procEntrustCommandUpdateStatus

go
create proc procEntrustCommandUpdateStatus(
	@SubmitId		int
	,@Status		int
	,@ModifiedDate	datetime
)
as
begin
	declare @newid int
	update entrustcommand
	set Status			= @Status
		,ModifiedDate	= @ModifiedDate
	where SubmitId=@SubmitId
end

go
if exists (select name from sysobjects where name='procEntrustCommandUpdate')
drop proc procEntrustCommandUpdate

go
create proc procEntrustCommandUpdate(
	@SubmitId		int
	,@EntrustNo		int
	,@BatchNo		int
	,@Status		int
	,@ModifiedDate	datetime
)
as
begin
	declare @newid int
	update entrustcommand
	set EntrustNo		= @EntrustNo
		,BatchNo		= @BatchNo
		,Status			= @Status
		,ModifiedDate	= @ModifiedDate
	where SubmitId=@SubmitId
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
		  ,Status
		  ,CreatedDate
		  ,ModifiedDate
	from entrustcommand
	where SubmitId=@SubmitId
end

go
if exists (select name from sysobjects where name='procEntrustCommandSelectByCommandId')
drop proc procEntrustCommandSelectByCommandId

go
create proc procEntrustCommandSelectByCommandId(
	@CommandId		int
)
as
begin
	select SubmitId
		  ,CommandId
		  ,Copies
		  ,EntrustNo
		  ,BatchNo
		  ,Status
		  ,CreatedDate
		  ,ModifiedDate
	from entrustcommand
	where CommandId=@CommandId
end


go
if exists (select name from sysobjects where name='procEntrustCommandSelectAll')
drop proc procEntrustCommandSelectAll

go
create proc procEntrustCommandSelectAll
as
begin
	select SubmitId
		  ,CommandId
		  ,Copies
		  ,EntrustNo
		  ,BatchNo
		  ,Status
		  ,CreatedDate
		  ,ModifiedDate
	from entrustcommand
end