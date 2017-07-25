use tradingsystem

--**说明：块级别以--==xxx begin====开始，以--==xxx end====结束
--**表级别以--++xxx begin++++开始，以--++xxx end++++结束

--==account/user/permission begin====
--++users begin++++
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

--++users end++++

--++roles begin++++
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
--++roles end++++

--++userrole begin++++
go
if exists (select name from sysobjects where name='procUserRoleInsert')
drop proc procUserRoleInsert

go
create proc procUserRoleInsert(
	@UserId			int
	,@RoleId		int
)
as
begin
	declare @newid int
	
	insert into userrole(
		UserId
		,RoleId
		,CreateDate
	)
	values
	(
		@UserId
		,@RoleId
		,getdate()
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end

go
if exists (select name from sysobjects where name='procUserRoleDelete')
drop proc procUserRoleDelete

go
create proc procUserRoleDelete(
	@UserId		int
	,@RoleId	int
)
as
begin
	delete from userrole
	where UserId=@UserId and RoleId=@RoleId
end

go
if exists (select name from sysobjects where name='procUserRoleSelect')
drop proc procUserRoleSelect

go
create proc procUserRoleSelect(
	@UserId		int = NULL
	,@RoleId	int = NULL
)
as
begin
	if @UserId is not null and @RoleId is not null
	begin
		select Id
			,UserId
			,RoleId
			,CreateDate
			,ModifiedDate
		from userrole
		where UserId=@UserId 
		and RoleId=@RoleId
	end
	else if @UserId is not null
	begin
		select Id
			,UserId
			,RoleId
			,CreateDate
			,ModifiedDate
		from userrole
		where UserId=@UserId
	end
	else if @RoleId is not null
	begin
		select Id
			,UserId
			,RoleId
			,CreateDate
			,ModifiedDate
		from userrole
		where RoleId=@RoleId
	end
	else
	begin
		select Id
			,UserId
			,RoleId
			,CreateDate
			,ModifiedDate
		from userrole 
	end
end
--++userrole end++++

--++tokenresourcepermission begin++++
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
		,CreateDate
	)
	values(
		@Token
		,@TokenType
		,@ResourceId
		,@ResourceType
		,@Permission
		,getdate()
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
	set Permission		= @Permission
		,ModifiedDate	= getdate()
	where Token=@Token
		and TokenType=@TokenType
		and ResourceId=@ResourceId
		and ResourceType=@ResourceType
end

go
if exists (select name from sysobjects where name='procTokenResourcePermissionDelete')
drop proc procTokenResourcePermissionDelete

go
--删除资源之后也要一起删除跟资源相关的权限
create proc procTokenResourcePermissionDelete(
	@ResourceId	int
	,@ResourceType	int
)
as
begin
	delete from tokenresourcepermission
	where ResourceId=@ResourceId
		and ResourceType=@ResourceType
end

go
if exists (select name from sysobjects where name='procTokenResourcePermissionSelectByToken')
drop proc procTokenResourcePermissionSelectByToken

go
--获取某个用户/角色所拥有的全部资源权限
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
		,CreateDate
		,ModifiedDate
	from tokenresourcepermission
	where Token=@Token
		and TokenType=@TokenType
end

go
if exists (select name from sysobjects where name='procTokenResourcePermissionSelectResourceType')
drop proc procTokenResourcePermissionSelectResourceType

go
--获取某个用户/角色所具有特定类的所有资源权限
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
		,CreateDate
		,ModifiedDate
	from tokenresourcepermission
	where Token=@Token
		and TokenType=@TokenType
		and ResourceType=@ResourceType
end

go
if exists (select name from sysobjects where name='procTokenResourcePermissionSelectByResouce')
drop proc procTokenResourcePermissionSelectByResouce

go
--根据资源信息获取对该资源具有权限的所有用户信息
create proc procTokenResourcePermissionSelectByResouce(
	@ResourceId	int
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
		,CreateDate
		,ModifiedDate
	from tokenresourcepermission
	where ResourceId=@ResourceId
		and ResourceType=@ResourceType
end

go
if exists (select name from sysobjects where name='procTokenResourcePermissionSelectSingle')
drop proc procTokenResourcePermissionSelectSingle

go
--获取某个用户/角色对某一资源的所有权限
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
		,CreateDate
		,ModifiedDate
	from tokenresourcepermission
	where Token=@Token
		and TokenType=@TokenType
		and ResourceId=@ResourceId
		and ResourceType=@ResourceType
end
--++tokenresourcepermission end++++

--++features begin++++
go
if exists (select name from sysobjects where name='procFeaturesInsert')
drop proc procFeaturesInsert

go
create proc procFeaturesInsert(
	@Id					int
	,@Code				varchar(20)
	,@Name				varchar(30)
	,@Description		varchar(100)
)
as
begin
	insert into features(
		Id
		,Code			
		,Name		
		,Description
	)
	values
	(
		@Id
		,@Code			
		,@Name		
		,@Description
	)

	return @Id
end

go
if exists (select name from sysobjects where name='procFeaturesUpdate')
drop proc procFeaturesUpdate

go
create proc procFeaturesUpdate(
	@Id					int
	,@Code				varchar(30)
	,@Name				varchar(30)
	,@Description		varchar(100)
)
as
begin
	update features
	set	
		Code			= @Code		
		,Name			= @Name	
		,Description	= @Description
	where Id=@Id
end

go
if exists (select name from sysobjects where name='procFeaturesDelete')
drop proc procFeaturesDelete

go
create proc procFeaturesDelete(
	@Id int
)
as
begin
	delete from features where Id=@Id
end

go
if exists (select name from sysobjects where name='procFeaturesSelect')
drop proc procFeaturesSelect

go
create proc procFeaturesSelect(
	@Id int = NULL
)
as
begin
	if @Id is not null
	begin
		select Id
			,Code
			,Name
			,Description
		from features
		where Id=@Id
	end
	else
	begin
		select Id
			,Code
			,Name
			,Description
		from features
	end
end

go
if exists (select name from sysobjects where name='procFeaturesSelectByCode')
drop proc procFeaturesSelectByCode

go
create proc procFeaturesSelectByCode(
	@Code varchar(30)
)
as
begin
	select Id
		,Code
		,Name
		,Description
	from features
	where Code=@Code
end
--++features end++++

--==account/user/permission end====