use tradingsystem

if object_id('userresourcepermission') is not null
drop table userresourcepermission

create table userresourcepermission(
	Id			int identity(1, 1) primary key,
	UserId		int not null,
	ResourceId	int not null,
	Permission	int
)

if exists (select name from sysobjects where name='procUserResourcePermissionInsert')
drop proc procUserResourcePermissionInsert

go
create proc procUserResourcePermissionInsert(
	@UserId			int
	,@ResourceId	int
	,@Permission	int
)
as
begin
	declare @newid int
	insert into userresourcepermission(
		UserId
		,ResourceId
		,Permission
	)
	values(
		@UserId
		,@ResourceId
		,@Permission
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end

if exists (select name from sysobjects where name='procUserResourcePermissionUpdate')
drop proc procUserResourcePermissionUpdate

go
create proc procUserResourcePermissionUpdate(
	@UserId			int
	,@ResourceId	int
	,@Permission	int
)
as
begin
	update userresourcepermission
	set Permission	= @Permission
	where UserId=@UserId and ResourceId=@ResourceId
end

if exists (select name from sysobjects where name='procUserResourcePermissionDelete')
drop proc procUserResourcePermissionDelete

go
create proc procUserResourcePermissionDelete(
	@UserId			int
	,@ResourceId	int
)
as
begin
	delete from userresourcepermission
	where UserId=@UserId and ResourceId=@ResourceId
end

if exists (select name from sysobjects where name='procUserResourcePermissionSelect')
drop proc procUserResourcePermissionSelect

go
create proc procUserResourcePermissionSelect(
	@UserId			int = NULL
	,@ResourceId	int = NULL
)
as
begin
	if @UserId is not null and @ResourceId is not null
	begin
		select
			Id
			,UserId
			,ResourceId
			,Permission
		from userresourcepermission
		where UserId=@UserId and ResourceId=@ResourceId
	end
	else if @UserId is not null
	begin
		select
			Id
			,UserId
			,ResourceId
			,Permission
		from userresourcepermission
		where UserId=@UserId
	end
	else if @ResourceId is not null
	begin
		select
			Id
			,UserId
			,ResourceId
			,Permission
		from userresourcepermission
		where ResourceId=@ResourceId
	end
	else
	begin
		select
			Id
			,UserId
			,ResourceId
			,Permission
		from userresourcepermission
	end
end