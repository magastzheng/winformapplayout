use tradingsystem

if object_id('features') is not null
drop table features

create table features(
	Id			int not null primary key,
	Name		varchar(30) not null,
	Description	varchar(100),
)

go
if exists (select name from sysobjects where name='procFeaturesInsert')
drop proc procFeaturesInsert

go
create proc procFeaturesInsert(
	@Id					int
	,@Name				varchar(30)
	,@Description		varchar(100)
)
as
begin
	insert into features(
		Id			
		,Name		
		,Description
	)
	values
	(
		@Id			
		,@Name		
		,@Description
	)
end

go
if exists (select name from sysobjects where name='procFeaturesUpdate')
drop proc procFeaturesUpdate

go
create proc procFeaturesUpdate(
	@Id					int
	,@Name				varchar(30)
	,@Description		varchar(100)
)
as
begin
	update features
	set			
		Name			= @Name	
		,Description	= @Description
	where Id=@Id
end

go
if exists (select name from sysobjects where name='procFeaturesUpdate')
drop proc procFeaturesUpdate

go
create proc procFeaturesUpdate(
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
			,Name
			,Description
		from features
		where Id=@Id
	end
	else
	begin
		select Id
			,Name
			,Description
		from features
	end
end