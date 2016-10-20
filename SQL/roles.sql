use tradingsystem

if object_id('roles') is not null
drop table roles

create table roles(
	Id	int	not null primary key,	--1 admin, 2 - systemmanager, 5 - fundmanager, 6 - dealer
	Name	varchar(20) not null,
	Status	int,	--0 inactive, 1 - normal
)

go
if exists (select name from sysobjects where name='procRolesInsert')
drop proc procRolesInsert

go
create proc procRolesInsert(
	@Id			int
	,@Name		varchar(20)
	,@Status	int
)
as
begin
	
	insert into roles(
		Id
		,Name
		,Status
	)
	values
	(
		@Id
		,@Name
		,@Status
	)

	return @Id
end

go
if exists (select name from sysobjects where name='procRolesUpdate')
drop proc procRolesUpdate

go
create proc procRolesUpdate(
	@Id		int
	,@Name		varchar(20)
	,@Status	int
)
as
begin
	update roles
	set
		Id		= @Id
		,Name	= @Name
		,Status = @Status
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
		from roles
		where Id=@Id
	end
	else
	begin
		select Id
			,Name
			,Status
		from roles
	end
end