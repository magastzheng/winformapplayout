use tradingsystem

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