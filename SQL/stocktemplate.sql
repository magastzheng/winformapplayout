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
if exists (select name from sysobjects where name='procInsertTemplate')
drop proc procInsertTemplate

go

create proc procInsertTemplate(
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
if exists (select name from sysobjects where name='procUpdateTemplate')
drop proc procUpdateTemplate

go

create proc procUpdateTemplate(
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
if exists (select name from sysobjects where name='procSelectTemplate')
drop proc procSelectTemplate

go

create proc procSelectTemplate(
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
if exists (select name from sysobjects where name='procDeleteTemplate')
drop proc procDeleteTemplate

go

create proc procDeleteTemplate(
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
if exists (select name from sysobjects where name='procInsertTemplateStock')
drop proc procInsertTemplateStock

go

create proc procInsertTemplateStock(
	@TemplateId int,
	@SecuCode varchar(10),
	@Amount int,
	@MarketCap numeric(20, 4),
	@MarketCapOpt numeric(5, 2),
	@SettingWeight numeric(5, 2)
)
as
begin
	declare @newid varchar(20)
	begin try
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
	end try
	begin catch
		select ERROR_NUMBER() as ErrorNumber, ERROR_MESSAGE() as ErrorMesssage
	end catch

	set @newid=@SecuCode+';'+convert(varchar(10),@TemplateId)
	return @newid
end

go
if exists (select name from sysobjects where name='procUpdateTemplateStock')
drop proc procUpdateTemplateStock

go

create proc procUpdateTemplateStock(
	@TemplateId int,
	@SecuCode varchar(10),
	@Amount int,
	@MarketCap numeric(20, 4),
	@MarketCapOpt numeric(5, 2),
	@SettingWeight numeric(5, 2)
)
as
begin
	declare @newid varchar(20)
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

	set @newid=@SecuCode+';'+convert(varchar(10),@TemplateId)
	return @newid
end

go
if exists (select name from sysobjects where name='procDeleteTemplateStock')
drop proc procDeleteTemplateStock

go

create proc procDeleteTemplateStock(
	@TemplateId int,
	@SecuCode varchar(10)
)
as
begin
	delete from templatestock
	where TemplateId = @TemplateId and SecuCode = @SecuCode
end


go
if exists (select name from sysobjects where name='procSelectTemplateStock')
drop proc procSelectTemplateStock

go

create proc procSelectTemplateStock(
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