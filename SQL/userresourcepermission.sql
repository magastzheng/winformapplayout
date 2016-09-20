use tradingsystem

go
if object_id('tokenresourcepermission') is not null
drop table tokenresourcepermission

create table tokenresourcepermission(
	Id				int identity(1, 1) primary key,
	Token			int not null,
	TokenType		int not null,
	ResourceId		int not null,			--使用(ResourceId, ResourceType）唯一的定位资源，
	ResourceType	int not null,			--不需要额外的resource表
	Permission		int
)

go
if exists (select name from sysobjects where name='procTokenResourcePermissionInsert')
drop proc procTokenResourcePermissionInsert

go
create proc procTokenResourcePermissionInsert(
	@Token			int
	,@TokenType		int
	,@ResourceId	int
	,@ResourceType	int
	,@Permission	int
)
as
begin
	declare @newid int
	insert into tokenresourcepermission(
		Token
		,TokenType
		,ResourceId
		,ResourceType
		,Permission
	)
	values(
		@Token
		,@TokenType
		,@ResourceId
		,@ResourceType
		,@Permission
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end

go
if exists (select name from sysobjects where name='procTokenResourcePermissionUpdate')
drop proc procTokenResourcePermissionUpdate

go
create proc procTokenResourcePermissionUpdate(
	@Token			int
	,@TokenType		int
	,@ResourceId	int
	,@ResourceType	int
	,@Permission	int
)
as
begin
	update tokenresourcepermission
	set Permission	= @Permission
	where Token=@Token
		and TokenType=@TokenType
		and ResourceId=@ResourceId
		and ResourceType=@ResourceType
end

go
if exists (select name from sysobjects where name='procTokenResourcePermissionDelete')
drop proc procTokenResourcePermissionDelete

go
create proc procTokenResourcePermissionDelete(
	@Token			int
	,@TokenType		int
	,@ResourceId	int
	,@ResourceType	int
)
as
begin
	delete from tokenresourcepermission
	where Token=@Token
		and TokenType=@TokenType
		and ResourceId=@ResourceId
		and ResourceType=@ResourceType
end

go
if exists (select name from sysobjects where name='procTokenResourcePermissionSelectByToken')
drop proc procTokenResourcePermissionSelectByToken

go
create proc procTokenResourcePermissionSelectByToken(
	@Token			int
	,@TokenType		int
)
as
begin
	select
		Id
		,Token
		,TokenType
		,ResourceId
		,ResourceType
		,Permission
	from tokenresourcepermission
	where Token=@Token
		and TokenType=@TokenType
end

go
if exists (select name from sysobjects where name='procTokenResourcePermissionSelectResourceType')
drop proc procTokenResourcePermissionSelectResourceType

go
create proc procTokenResourcePermissionSelectResourceType(
	@Token			int
	,@TokenType		int
	,@ResourceType	int
)
as
begin
	select
		Id
		,Token
		,TokenType
		,ResourceId
		,ResourceType
		,Permission
	from tokenresourcepermission
	where Token=@Token
		and TokenType=@TokenType
		and ResourceType=@ResourceType
end

go
if exists (select name from sysobjects where name='procTokenResourcePermissionSelectSingle')
drop proc procTokenResourcePermissionSelectSingle

go
create proc procTokenResourcePermissionSelectSingle(
	@Token			int
	,@TokenType		int
	,@ResourceId	int
	,@ResourceType	int
)
as
begin
	select
		Id
		,Token
		,TokenType
		,ResourceId
		,ResourceType
		,Permission
	from tokenresourcepermission
	where Token=@Token
		and TokenType=@TokenType
		and ResourceId=@ResourceId
		and ResourceType=@ResourceType
end