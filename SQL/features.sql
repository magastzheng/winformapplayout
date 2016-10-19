use tradingsystem

if object_id('features') is not null
drop table features

create table features(
	Id			int not null primary key,	--软件系统功能Id
	Code		varchar(30) not null,		--软件系统功能代码
	Name		varchar(30) not null,		--软件系统功能名称
	Description	varchar(100),				--软件系统功能描述
)

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