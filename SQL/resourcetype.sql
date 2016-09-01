use tradingsystem

if object_id('resoucetype') is not null
drop table resourcetype

create table resourcetype(
	TypeId		int not null primary key,
	TypeName	varchar(20)
)

--insert new type
go
if exists (select name from sysobjects where name='procResourceTypeInsert')
drop proc procResourceTypeInsert

go
create proc procResourceTypeInsert(
	@TypeId		int
	,@TypeName	varchar(20)
)
as
begin
	insert into resourcetype(TypeId, TypeName)
	values(@TypeId, @TypeName)
end

go
if exists (select name from sysobjects where name='procResourceTypeUpdate')
drop proc procResourceTypeUpdate

go
create proc procResourceTypeUpdate(
	@TypeId		int
	,@TypeName	varchar(20)
)
as
begin
	update resourcetype
	set TypeName=@TypeName
	where TypeId=@TypeId
end

go
if exists (select name from sysobjects where name='procResourceTypeDelete')
drop proc procResourceTypeDelete

go
create proc procResourceTypeDelete(
	@TypeId		int
)
as
begin
	delete from resourcetype
	where TypeId=@TypeId
end

go
if exists (select name from sysobjects where name='procResourceTypeSelect')
drop proc procResourceTypeSelect

go
create proc procResourceTypeSelect(
	@TypeId		int
)
as
begin
	if @TypeId is not null and @TypeId > 0
	begin
		select TypeId, TypeName from resourcetype
		where TypeId=@TypeId
	end
	else
	begin
		select TypeId, TypeName from resourcetype
	end
end