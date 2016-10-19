use tradingsystem

if object_id('rolefeaturepermission') is not null
drop table rolefeaturepermission

create table rolefeaturepermission(
	Id			int identity(1, 1) primary key,
	RoleId		int not null,
	FeatureId	int not null,
	Permission	int				--必须是非负整数
)

--Permission(32 bit)
--1			2		4		8			16		32		64		                        
--Owner		Add		Delete  Edit  Select/View Execute	Query	
if exists (select name from sysobjects where name='procRoleFeaturePermissionInsert')
drop proc procRoleFeaturePermissionInsert

go
create proc procRoleFeaturePermissionInsert(
	@RoleId			int
	,@FeatureId		int
	,@Permission	int
)
as
begin
	declare @newid int
	
	insert into rolefeaturepermission(
		RoleId
		,FeatureId
		,Permission
	)
	values
	(
		@RoleId
		,@FeatureId
		,@Permission
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end

if exists (select name from sysobjects where name='procRoleFeaturePermissionUpdate')
drop proc procRoleFeaturePermissionUpdate

go
create proc procRoleFeaturePermissionUpdate(
	@RoleId			int
	,@FeatureId		int
	,@Permission	int
)
as
begin
	update rolefeaturepermission
	set
		Permission=@Permission
	where RoleId=@RoleId and FeatureId=@FeatureId
end

if exists (select name from sysobjects where name='procRoleFeaturePermissionDelete')
drop proc procRoleFeaturePermissionDelete

go
create proc procRoleFeaturePermissionDelete(
	@RoleId			int
	,@FeatureId		int
)
as
begin
	delete from rolefeaturepermission
	where RoleId=@RoleId and FeatureId=@FeatureId
end

if exists (select name from sysobjects where name='procRoleFeaturePermissionSelect')
drop proc procRoleFeaturePermissionSelect

go
create proc procRoleFeaturePermissionSelect(
	@RoleId			int = NULL
	,@FeatureId		int = NULL
)
as
begin
	if @RoleId is not null and @FeatureId is not null
	begin
		select Id
			,RoleId
			,FeatureId
			,Permission
		from rolefeaturepermission
		where RoleId=@RoleId and FeatureId=@FeatureId
	end
	else if @RoleId is not null
	begin
		select Id
			,RoleId
			,FeatureId
			,Permission
		from rolefeaturepermission
		where RoleId=@RoleId
	end
	else if @FeatureId is not null
	begin
		select Id
			,RoleId
			,FeatureId
			,Permission
		from rolefeaturepermission
		where FeatureId=@FeatureId
	end
	else 
	begin
		select Id
			,RoleId
			,FeatureId
			,Permission
		from rolefeaturepermission
	end
end