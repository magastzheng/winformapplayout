use tradingsystem

if object_id('stocktemplate') is not null
drop table stocktemplate

create table stocktemplate(
	TemplateId int identity(1, 1) primary key,		--模板ID
	TemplateName varchar(50),						--模板名称
	Status int,										-- 1 - normal, 2 - inactive
	WeightType int,									-- 1 - 数量权重，2 - 比例权重
	ReplaceType int,								-- 0 - 个股替代，1 - 模板替代
	FuturesCopies int,								-- 期货份数
	MarketCapOpt numeric(5, 2),						-- 市值比例(%)
	BenchmarkId varchar(10),						-- 标的指数
	CreatedDate datetime,						
	ModifiedDate datetime,
	CreatedUserId int
)

if object_id('templatestock') is not null
drop table templatestock

create table templatestock(
	TemplateId int not null,		--模板ID
	SecuCode varchar(10) not null,	--证券代码
	Amount int,						--证券数量
	MarketCap numeric(20, 4),		--证券市值
	MarketCapOpt numeric(5, 2),		--证券市值比例(%)
	SettingWeight numeric(5, 2),	--证券设置权重(%)

	constraint pk_templatestock_Id primary key(TemplateId,SecuCode)
)

--if object_id('stock') is not null
--drop table stock

--create table stock(
--	SecuCode varchar(10) primary key,
--	SecuName varchar(30),
--	Exchange varchar(10)
--)

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

go
if exists (select name from sysobjects where name='procTemplateSelectById')
drop proc procTemplateSelectById

go

create proc procTemplateSelectById(
	@TemplateId int = NULL
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
	where @TemplateId is null or TemplateId=@TemplateId
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
	--如果该模板被监控单元使用，不能删除
	declare @UsedTotal int
	set @UsedTotal=(select count(StockTemplateId) from monitorunit where StockTemplateId=@TemplateId)
	if @UsedTotal > 0
	begin
		return -1
	end
	else
	begin
		delete from stocktemplate
		where TemplateId=@TemplateId

		delete from templatestock
		where TemplateId=@TemplateId

		return 1
	end
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
if exists (select name from sysobjects where name='procTemplateStockInsertOrUpdate')
drop proc procTemplateStockInsertOrUpdate

go

create proc procTemplateStockInsertOrUpdate(
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
	declare @Total int
	set @Total=(select count(SecuCode) as Total from templatestock where TemplateId=@TemplateId and SecuCode=@SecuCode)
	if @Total = 0
	begin
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
	end
	else
	begin
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
	end

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
if exists (select name from sysobjects where name='procTemplateStockDeleteAll')
drop proc procTemplateStockDeleteAll

go

create proc procTemplateStockDeleteAll(
	@TemplateId int
)
as
begin
	delete from templatestock
	where TemplateId = @TemplateId
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
		a.TemplateId,
		a.SecuCode,
		a.Amount,
		a.MarketCap,
		a.MarketCapOpt,
		a.SettingWeight,
		b.SecuName,
		b.ExchangeCode
	from templatestock a
	join securityinfo b
	on a.SecuCode = b.SecuCode and b.SecuType=2
	where a.TemplateId=@TemplateId
end

---=========================templatestock end======================


---=========================stock begin======================

---=========================stock end======================