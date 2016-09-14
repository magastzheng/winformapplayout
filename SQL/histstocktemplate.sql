use tradingsystem

if object_id('histtemplate') is not null
drop table histtemplate

create table histtemplate(
	ArchiveId	int identity(1, 1) primary key,
	TemplateId	int,				--模板ID
	TemplateName varchar(50),		--模板名称
	Status int,						-- 1 - normal, 2 - inactive
	WeightType int,					-- 1 - 数量权重，2 - 比例权重
	ReplaceType int,				-- 0 - 个股替代，1 - 模板替代
	FuturesCopies int,				-- 期货份数
	MarketCapOpt numeric(5, 2),		-- 市值比例(%)
	BenchmarkId varchar(10),		-- 标的指数
	ArchiveDate	datetime,			-- 归档日期
	CreatedDate datetime,			--创建时间			
	ModifiedDate datetime,			--修改时间
	CreatedUserId int				--创建用户ID
)

if object_id('histtemplatestock') is not null
drop table histtemplatestock

create table histtemplatestock(
	ArchiveId	int not null,		--归档ID
	TemplateId int not null,		--模板ID
	SecuCode varchar(10) not null,	--证券代码
	Amount int,						--证券数量
	MarketCap numeric(20, 4),		--证券市值
	MarketCapOpt numeric(5, 2),		--证券市值比例(%)
	SettingWeight numeric(5, 2),	--证券设置权重(%)

	constraint pk_histtemplatestock_Id primary key(ArchiveId, TemplateId, SecuCode)
)

---=========================histstocktemplate begin======================
go
if exists (select name from sysobjects where name='procHistTemplateInsert')
drop proc procHistTemplateInsert

go

create proc procHistTemplateInsert(
	@TemplateId	int,
	@TemplateName varchar(50),
	@Status int,
	@WeightType int,
	@ReplaceType int,
	@FuturesCopies int,
	@MarketCapOpt numeric(5, 2),
	@BenchmarkId varchar(10),
	@ArchiveDate datetime,
	@CreatedDate datetime,
	@ModifiedDate datetime,
	@CreatedUserId int
)
as
begin
	declare @newid int
	insert into histtemplate(
		TemplateId,
		TemplateName,
		Status,
		WeightType,
		ReplaceType,
		FuturesCopies,
		MarketCapOpt,
		BenchmarkId,
		ArchiveDate,
		CreatedDate,
		ModifiedDate,
		CreatedUserId
	)
	values(
		@TemplateId,
		@TemplateName,
		@Status,
		@WeightType,
		@ReplaceType,
		@FuturesCopies,
		@MarketCapOpt,
		@BenchmarkId,
		@ArchiveDate,
		@CreatedDate,
		@ModifiedDate,
		@CreatedUserId
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end

go
if exists (select name from sysobjects where name='procHistTemplateSelect')
drop proc procHistTemplateSelect

go

create proc procHistTemplateSelect(
	@UserId int = NULL
)
as
begin
	select 
		ArchiveId,
		TemplateId,
		TemplateName,
		Status,
		WeightType,
		ReplaceType,
		FuturesCopies,
		MarketCapOpt,
		BenchmarkId,
		ArchiveDate,
		CreatedDate,
		ModifiedDate,
		CreatedUserId
	from histtemplate
	where @UserId is null or CreatedUserId=@UserId
end

go
if exists (select name from sysobjects where name='procHistTemplateDelete')
drop proc procHistTemplateDelete

go

create proc procHistTemplateDelete(
	@ArchiveId int
)
as
begin
	--如果该历史模板，并删除相应模板中的股票
	delete from histtemplate
	where ArchiveId=@ArchiveId

	delete from histtemplatestock
	where ArchiveId=@ArchiveId
end

---=========================stocktemplate end======================

---=========================templatestock begin======================
go
if exists (select name from sysobjects where name='procHistTemplateStockInsert')
drop proc procHistTemplateStockInsert

go

create proc procHistTemplateStockInsert(
	@ArchiveId int,
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
	insert into histtemplatestock(
		ArchiveId,
		TemplateId,
		SecuCode,
		Amount,
		MarketCap,
		MarketCapOpt,
		SettingWeight
	)
	values(
		@ArchiveId,
		@TemplateId,
		@SecuCode,
		@Amount,
		@MarketCap,
		@MarketCapOpt,
		@SettingWeight
	)

	set @ReturnValue=@SecuCode+';'+convert(varchar,@TemplateId)+';'+convert(varchar, @ArchiveId)
end

go
if exists (select name from sysobjects where name='procHistTemplateStockDelete')
drop proc procHistTemplateStockDelete

go

create proc procHistTemplateStockDelete(
	@ArchiveId int,
	@TemplateId int,
	@SecuCode varchar(10),
	@ReturnValue varchar(20) output
)
as
begin
	delete from histtemplatestock
	where ArchiveId = @ArchiveId and TemplateId = @TemplateId and SecuCode = @SecuCode

	set @ReturnValue=@SecuCode+';'+convert(varchar, @TemplateId)+';'+convert(varchar, @ArchiveId)
end

go
if exists (select name from sysobjects where name='procHistTemplateStockDeleteAll')
drop proc procHistTemplateStockDeleteAll

go

create proc procHistTemplateStockDeleteAll(
	@ArchiveId int
)
as
begin
	delete from histtemplatestock
	where ArchiveId = @ArchiveId
end


go
if exists (select name from sysobjects where name='procHistTemplateStockSelect')
drop proc procHistTemplateStockSelect

go

create proc procHistTemplateStockSelect(
	@ArchiveId int
)
as
begin
	select 
		a.ArchiveId,
		a.TemplateId,
		a.SecuCode,
		a.Amount,
		a.MarketCap,
		a.MarketCapOpt,
		a.SettingWeight,
		b.SecuName,
		b.ExchangeCode
	from histtemplatestock a
	join securityinfo b
	on a.SecuCode = b.SecuCode and b.SecuType=2
	where a.ArchiveId = @ArchiveId
end

---=========================histtemplatestock end======================
