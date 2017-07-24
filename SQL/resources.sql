use tradingsystem

go
if exists (select name from sysobjects where name='procResourcesInsert')
drop proc procResourcesInsert

go
create proc procResourcesInsert(
	@RefId		int
	,@Type		int
	,@Name		varchar(30)
)
as
begin
	declare @newid int
	
	insert into resources(
		RefId
		,Type
		,Name
	)
	values
	(
		@RefId
		,@Type
		,@Name
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end

go
if exists (select name from sysobjects where name='procResourcesUpdate')
drop proc procResourcesUpdate

go
create proc procResourcesUpdate(
	@RefId		int
	,@Type		int
	,@Name		varchar(30)
)
as
begin
	update resources
	set Name=@Name
	where RefId=@RefId and Type=@Type
end

go
if exists (select name from sysobjects where name='procResourcesDelete')
drop proc procResourcesDelete

go
create proc procResourcesDelete(
	@RefId int
	,@Type int
)
as
begin
	delete from resources
	where RefId=@RefId and Type=@Type 
end

go
if exists (select name from sysobjects where name='procResourcesSelect')
drop proc procResourcesSelect

go
create proc procResourcesSelect(
	@RefId int = NULL
	,@Type int = NULL
)
as
begin
	if @RefId is not null and @Type is not null
	begin
		select 
			Id
			,RefId
			,Type
			,Name
		from resources
		where RefId=@RefId and Type=@Type
	end
	else if @RefId is not null
	begin
		select 
			Id
			,RefId
			,Type
			,Name
		from resources
		where RefId=@RefId
	end
	else if @Type is not null
	begin
		select 
			Id
			,RefId
			,Type
			,Name
		from resources
		where Type=@Type
	end
	else
	begin
		select 
			Id
			,RefId
			,Type
			,Name
		from resources
	end
end