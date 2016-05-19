use tradingsystem

if object_id('stocktemplate') is not null
drop table stocktemplate

create table stocktemplate(
	TemplateId int identity(1, 1) primary key,
	TemplateName varchar(50),
	Status int,
	WeightType int,
	ReplaceType int,
	FuturesCopies int,
	MarketCapOpt numeric(5, 2),
	BenchmarkId varchar(10),
	CreatedDate datetime,
	ModifiedDate datetime,
	CreatedUserId int
)

if object_id('templatestock') is not null
drop table templatestock

create table templatestock(
	TemplateId int not null,
	SecuCode varchar(10) not null,
	Amount int,
	MarketCap numeric(20, 4),
	MarketCapOpt numeric(5, 2),
	SettingWeight numeric(5, 2),

	constraint pk_templatestock_Id primary key(TemplateId,SecuCode)
)

if object_id('stock') is not null
drop table stock

create table stock(
	SecuCode varchar(10) primary key,
	SecuName varchar(30),
	Exchange varchar(10)
)

if object_id('benchmark') is not null
drop table benchmark

create table benchmark(
	BenchmarkId varchar(10) primary key,
	BenchmarkName varchar(50) not null,
	Exchange varchar(10) not null
)

---=========================stocktemplate begin======================
go
if exists (select name from sysobjects where name='procTemplateInsert')
drop proc procTemplateInsert

go

create proc procTemplateInsert(
	@TemplateName varchar(50),
	@WeightType int,
	@ReplaceType int,
	@FuturesCopies int,
	@MarketCapOpt numeric(5, 2),
	@BenchmarkId varchar(10),
	@CreatedDate datetime,
	@CreatedUserId int
)
as
begin
	declare @newid int
	insert into stocktemplate(
		TemplateName,
		Status,
		WeightType,
		ReplaceType,
		FuturesCopies,
		MarketCapOpt,
		BenchmarkId,
		CreatedDate,
		CreatedUserId
	)
	values(
		@TemplateName,
		1,
		@WeightType,
		@ReplaceType,
		@FuturesCopies,
		@MarketCapOpt,
		@BenchmarkId,
		@CreatedDate,
		@CreatedUserId
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end


go
if exists (select name from sysobjects where name='procTemplateUpdate')
drop proc procTemplateUpdate

go

create proc procTemplateUpdate(
	@TemplateId int,
	@TemplateName varchar(50),
	@Status int,
	@WeightType int,
	@ReplaceType int,
	@FuturesCopies int,
	@MarketCapOpt numeric(5, 2),
	@BenchmarkId varchar(10),
	@ModifiedDate datetime,
	@CreatedUserId int
)
as
begin
	update stocktemplate
	set
		TemplateName = @TemplateName,
		Status = @Status,
		WeightType = @WeightType,
		ReplaceType = @ReplaceType,
		FuturesCopies = @FuturesCopies,
		MarketCapOpt = @MarketCapOpt,
		BenchmarkId = @BenchmarkId,
		ModifiedDate = @ModifiedDate,
		CreatedUserId = @CreatedUserId
	where TemplateId = @TemplateId
	
	return @TemplateId
end

--Add userid as the parameter?
go
if exists (select name from sysobjects where name='procTemplateSelect')
drop proc procTemplateSelect

go

create proc procTemplateSelect(
	@UserId int = NULL
)
as
begin
	select 
		TemplateId,
		TemplateName,
		Status,
		WeightType,
		ReplaceType,
		FuturesCopies,
		MarketCapOpt,
		BenchmarkId,
		CreatedDate,
		ModifiedDate,
		CreatedUserId
	from stocktemplate
	where @UserId is null or CreatedUserId=@UserId
end

go
if exists (select name from sysobjects where name='procTemplateDelete')
drop proc procTemplateDelete

go

create proc procTemplateDelete(
	@TemplateId int
)
as
begin
	delete from stocktemplate
	where TemplateId=@TemplateId
end

---=========================stocktemplate end======================

---=========================templatestock begin======================
go
if exists (select name from sysobjects where name='procTemplateStockInsert')
drop proc procTemplateStockInsert

go

create proc procTemplateStockInsert(
	@TemplateId int,
	@SecuCode varchar(10),
	@Amount int,
	@MarketCap numeric(20, 4),
	@MarketCapOpt numeric(5, 2),
	@SettingWeight numeric(5, 2),
	@ReturnValue varchar(20) output
)
as
begin
	--declare @newid varchar(20)
	--begin try
		insert into templatestock(
			TemplateId,
			SecuCode,
			Amount,
			MarketCap,
			MarketCapOpt,
			SettingWeight
		)
		values(
			@TemplateId,
			@SecuCode,
			@Amount,
			@MarketCap,
			@MarketCapOpt,
			@SettingWeight
		)
	--end try
	--begin catch
	--	select ERROR_NUMBER() as ErrorNumber, ERROR_MESSAGE() as ErrorMesssage
	--end catch

	set @ReturnValue=@SecuCode+';'+convert(varchar,@TemplateId)
end

go
if exists (select name from sysobjects where name='procTemplateStockUpdate')
drop proc procTemplateStockUpdate

go

create proc procTemplateStockUpdate(
	@TemplateId int,
	@SecuCode varchar(10),
	@Amount int,
	@MarketCap numeric(20, 4),
	@MarketCapOpt numeric(5, 2),
	@SettingWeight numeric(5, 2),
	@ReturnValue varchar(20) output
)
as
begin
	--declare @newid varchar(20)
	begin try
		update templatestock
		set Amount = @Amount,
			MarketCap = @MarketCap,
			MarketCapOpt = @MarketCapOpt,
			SettingWeight = @SettingWeight
		where TemplateId = @TemplateId and SecuCode = @SecuCode
	end try
	begin catch
		select ERROR_NUMBER() as ErrorNumber, ERROR_MESSAGE() as ErrorMesssage
	end catch

	set @ReturnValue=@SecuCode+';'+convert(varchar,@TemplateId)
end

go
if exists (select name from sysobjects where name='procTemplateStockDelete')
drop proc procTemplateStockDelete

go

create proc procTemplateStockDelete(
	@TemplateId int,
	@SecuCode varchar(10),
	@ReturnValue varchar(20) output
)
as
begin
	delete from templatestock
	where TemplateId = @TemplateId and SecuCode = @SecuCode

	set @ReturnValue=@SecuCode+';'+convert(varchar, @TemplateId)
end


go
if exists (select name from sysobjects where name='procTemplateStockSelect')
drop proc procTemplateStockSelect

go

create proc procTemplateStockSelect(
	@TemplateId int
)
as
begin
	select 
		TemplateId,
		SecuCode,
		Amount,
		MarketCap,
		MarketCapOpt,
		SettingWeight
	from templatestock
	where TemplateId=@TemplateId
end

---=========================templatestock end======================


---=========================stock begin======================

---=========================stock end======================