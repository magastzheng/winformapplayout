use tradingsystem

if object_id('securityinfo') is not null
drop table securityinfo

create table securityinfo(
	SecuCode		varchar(10) not null,	--证券交易所代码
	SecuName		varchar(50),			--证券名称
	ExchangeCode	varchar(10),			--交易所代码
	SecuType		int,					--证券类型：1 - 指数， 2 - 股票， 3 - 股指期货
	ListDate		varchar(10),			--上市交易时间
	DeListDate		varchar(10),			--退市时间
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
	@ListDate		varchar(10)
)
as
begin
	insert into securityinfo(
		SecuCode
		,SecuName
		,ExchangeCode
		,SecuType
		,ListDate	
	)
	values(
		@SecuCode
		,@SecuName
		,@ExchangeCode
		,@SecuType
		,@ListDate	
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
if exists (select name from sysobjects where name='procSecurityInfoDelete')
drop proc procSecurityInfoDelete

go
create proc procSecurityInfoDelete(
	@SecuCode varchar(10),
	@SecuType int = 2 -- default remove the stock
)
as
begin
	delete from securityinfo where SecuCode=@SecuCode and SecuType = @SecuType
end

go
if exists (select name from sysobjects where name='procSecurityInfoSelect')
drop proc procSecurityInfoSelect

go
create proc procSecurityInfoSelect(
	@SecuCode varchar(10) = NULL,
	@SecuType int = 2
)
as
begin
	if @SecuCode is not null and len(@SecuCode) > 0
	begin
		if @SecuType is not null
		begin
			select SecuCode
				,SecuName
				,ExchangeCode
				,SecuType
				,ListDate
				,DeListDate 
			from securityinfo
			where SecuCode = @SecuCode and SecuType = @SecuType
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
			where SecuCode = @SecuCode
		end
	end
	else
	begin
		if @SecuType is not null
		begin
			select SecuCode
				,SecuName
				,ExchangeCode
				,SecuType
				,ListDate
				,DeListDate 
			from securityinfo
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
			where SecuType = @SecuType
		end
	end
end
