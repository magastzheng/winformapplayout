use tradingsystem

go
if exists (select name from sysobjects where name='procUsersInsert')
drop proc procUsersInsert

go
create proc procUsersInsert(
	@Operator	varchar(10)
	,@Name		varchar(10)
	,@Status	int = NULL	--默认为非激活状态
)
as
begin
	declare @newid int
	declare @state int
	if @Status is not null
	begin
		set @state = @Status
	end
	else
	begin
		set @state = 0
	end

	insert into users(
		Operator
		,Name
		,Status
		,CreateDate
	)
	values(
		@Operator
		,@Name
		,@state
		,getdate()
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end

go
if exists (select name from sysobjects where name='procUsersUpdate')
drop proc procUsersUpdate

go
create proc procUsersUpdate(
	@Operator	varchar(10)
	,@Name		varchar(10)
	,@Status	int
)
as
begin
	update users
	set
		Name			= @Name
		,Status			= @Status
		,ModifiedDate	= getdate()
	where Operator=@Operator
end

go
if exists (select name from sysobjects where name='procUsersDelete')
drop proc procUsersDelete

go
create proc procUsersDelete(
	@Operator	varchar(10)
	)
as
begin
	delete from users
	where Operator=@Operator
end

go
if exists (select name from sysobjects where name='procUsersSelect')
drop proc procUsersSelect

go
create proc procUsersSelect(
	@Operator	varchar(10) = NULL
	)
as
begin
	if @Operator is not null
	begin
		select Id
			,Operator
			,Name
			,Status
			,CreateDate
			,ModifiedDate
		from users
		where Operator=@Operator
	end
	else
	begin
		select Id
			,Operator
			,Name
			,Status
			,CreateDate
			,ModifiedDate
		from users
	end
end


go
if exists (select name from sysobjects where name='procUsersSelectById')
drop proc procUsersSelectById

go
create proc procUsersSelectById(
	@Id	int
	)
as
begin
	select Id
		,Operator
		,Name
		,Status
		,CreateDate
		,ModifiedDate
	from users
	where Id=@Id
end