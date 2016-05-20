use tradingsystem

if object_id('securityinfo') is not null
drop table securityinfo

create table securityinfo(
	SecuCode		varchar(10) not null,
	SecuName		varchar(50),
	ExchangeCode	varchar(10),
	SecuType		int,
	ListDate		varchar(10),
	DeListDate		varchar(10),
	constraint pk_securityinfo_Id primary key(SecuCode, SecuType)
)

go
if exists (select name from sysobjects where name='procSecurityInfoInsert')
drop proc procSecurityInfoInsert

go
create proc procSecurityInfoInsert(
	@SecuCode		varchar(10),
	@SecuName		varchar(50),
	@ExchangeCode	varchar(10),
	@SecuType		int,
	@ListDate		varchar(10),
	@DeListDate		varchar(10)
)
as
begin
	insert into securityinfo(
		SecuCode
		,SecuName
		,ExchangeCode
		,SecuType
		,ListDate	
		,DeListDate
	)
	values(
		@SecuCode
		,@SecuName
		,@ExchangeCode
		,@SecuType
		,@ListDate	
		,@DeListDate
	)
end

go
if exists (select name from sysobjects where name='procSecurityInfoUpdate')
drop proc procSecurityInfoUpdate

go
create proc procSecurityInfoUpdate(
	@SecuCode		varchar(10),
	@SecuName		varchar(50),
	@ExchangeCode	varchar(10),
	@SecuType		int
)
as
begin
	update securityinfo
	set SecuName = @SecuName
		,ExchangeCode = @ExchangeCode
	where SecuCode = @SecuCode

	if @SecuType is not null
	begin
		update securityinfo
		set SecuType = @SecuType
		where SecuCode = @SecuCode
	end
end

go
if exists (select name from sysobjects where name='procSecurityInfoUpdate')
drop proc procSecurityInfoUpdate

go
create proc procSecurityInfoUpdate(
	@SecuCode varchar(10)
)
as
begin
	delete from securityinfo where SecuCode=@SecuCode
end

go
if exists (select name from sysobjects where name='procSecurityInfoSelect')
drop proc procSecurityInfoSelect

go
create proc procSecurityInfoSelect(
	@SecuCode varchar(10) = NULL
)
as
begin
	if @SecuCode is not null and len(@SecuCode) > 0
	begin
		select SecuCode
			,SecuName
			,ExchangeCode
			,SecuType
			,ListDate
			,DeListDate 
		from securityinfo
		where SecuCode = @SecuCode
	end
	else
	begin
		select SecuCode
			,SecuName
			,ExchangeCode
			,SecuType
			,ListDate
			,DeListDate 
		from securityinfo
	end
end
