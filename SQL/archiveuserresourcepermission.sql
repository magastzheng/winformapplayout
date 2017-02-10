use tradingsystem

go
if object_id('archivetokenresourcepermission') is not null
drop table archivetokenresourcepermission

create table archivetokenresourcepermission(
	ArchiveId		int identity(1, 1) primary key,
	ArchiveDate		datetime,
	Id				int not null,
	Token			int not null,
	TokenType		int not null,
	ResourceId		int not null,			--使用(ResourceId, ResourceType）唯一的定位资源，
	ResourceType	int not null,			--不需要额外的resource表
	Permission		int
)

go
if exists (select name from sysobjects where name='procArchiveTokenResourcePermissionInsert')
drop proc procArchiveTokenResourcePermissionInsert

go
create proc procArchiveTokenResourcePermissionInsert(
	@ArchiveDate	datetime
	,@Id			int
	,@Token			int
	,@TokenType		int
	,@ResourceId	int
	,@ResourceType	int
	,@Permission	int
)
as
begin
	declare @newid int
	insert into archivetokenresourcepermission(
		ArchiveDate
		,Id
		,Token
		,TokenType
		,ResourceId
		,ResourceType
		,Permission
	)
	values(
		@ArchiveDate
		,@Id
		,@Token
		,@TokenType
		,@ResourceId
		,@ResourceType
		,@Permission
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end

go
if exists (select name from sysobjects where name='procArchiveTokenResourcePermissionDelete')
drop proc procArchiveTokenResourcePermissionDelete

go
create proc procArchiveTokenResourcePermissionDelete(
	@ArchiveId	int
)
as
begin
	delete from archivetokenresourcepermission
	where ArchiveId=@ArchiveId
end

go
if exists (select name from sysobjects where name='procArchiveTokenResourcePermissionSelectByArchiveId')
drop proc procArchiveTokenResourcePermissionSelectByArchiveId

go
create proc procArchiveTokenResourcePermissionSelectByArchiveId(
	@ArchiveId int
)
as
begin
	select
		ArchiveId
		,ArchiveDate
		,Id
		,Token
		,TokenType
		,ResourceId
		,ResourceType
		,Permission
	from archivetokenresourcepermission
	where ArchiveId=@ArchiveId
end
