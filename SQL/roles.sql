use tradingsystem

if object_id('roles') is not null
drop table roles

create table roles(
	Id		int identity(1, 1) primary key,
	Name	varchar(20) not null,
	Status	int,	--0 inactive, 1 - normal
	Type	int		--1 admin, 2 - systemmanager, 5 - fundmanager, 6 - dealer
)

go
if exists (select name from sysobjects where name='procRolesInsert')
drop proc procRolesInsert

go
create proc procRolesInsert(
	@Name		varchar(20)
	,@Status	int
	,@Type		int
)
as
begin
	declare @newid int
	
	insert into roles(
		Name
		,Status
		,Type
	)
	values
	(
		@Name
		,@Status
		,@Type
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end

go
if exists (select name from sysobjects where name='procRolesUpdate')
drop proc procRolesUpdate

go
create proc procRolesUpdate(
	@Id			int
	,@Name		varchar(20)
	,@Status	int
	,@Type		int
)
as
begin
	update roles
	set
		Name	= @Name
		,Status = @Status
		,Type	= @Type
	where Id=@Id
end


go
if exists (select name from sysobjects where name='procRolesDelete')
drop proc procRolesDelete

go
create proc procRolesDelete(
	@Id	int
)
as
begin
	delete from roles where Id=@Id
end

go
if exists (select name from sysobjects where name='procRolesSelect')
drop proc procRolesSelect

go
create proc procRolesSelect(
	@Id	int = NULL
)
as
begin
	if @Id is not null
	begin
		select Id
			,Name
			,Status
			,Type
		from roles
		where Id=@Id
	end
	else
	begin
		select Id
			,Name
			,Status
			,Type
		from roles
	end
end