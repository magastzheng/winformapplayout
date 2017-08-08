use tradingsystem

if object_id('archivetemplate') is not null
drop table archivetemplate

create table archivetemplate(
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

if object_id('archivetemplatestock') is not null
drop table archivetemplatestock

create table archivetemplatestock(
	ArchiveId	int not null,		--归档ID
	TemplateId int not null,		--模板ID
	SecuCode varchar(10) not null,	--证券代码
	Amount int,						--证券数量
	MarketCap numeric(20, 4),		--证券市值
	MarketCapOpt numeric(5, 2),		--证券市值比例(%)
	SettingWeight numeric(5, 2),	--证券设置权重(%)

	constraint pk_archivetemplatestock_Id primary key(ArchiveId, TemplateId, SecuCode)
)

---=========================archivestocktemplate begin======================
go
if exists (select name from sysobjects where name='procArchiveTemplateInsert')
drop proc procArchiveTemplateInsert

go

create proc procArchiveTemplateInsert(
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
	insert into archivetemplate(
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
if exists (select name from sysobjects where name='procArchiveTemplateSelect')
drop proc procArchiveTemplateSelect

go

create proc procArchiveTemplateSelect(
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
	from archivetemplate
	where @UserId is null or CreatedUserId=@UserId
end

go
if exists (select name from sysobjects where name='procArchiveTemplateDelete')
drop proc procArchiveTemplateDelete

go

create proc procArchiveTemplateDelete(
	@ArchiveId int
)
as
begin
	--如果该历史模板，并删除相应模板中的股票
	delete from archivetemplate
	where ArchiveId=@ArchiveId

	delete from archivetemplatestock
	where ArchiveId=@ArchiveId
end

---=========================stocktemplate end======================

---=========================templatestock begin======================
go
if exists (select name from sysobjects where name='procArchiveTemplateStockInsert')
drop proc procArchiveTemplateStockInsert

go

create proc procArchiveTemplateStockInsert(
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
	insert into archivetemplatestock(
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
if exists (select name from sysobjects where name='procArchiveTemplateStockDelete')
drop proc procArchiveTemplateStockDelete

go

create proc procArchiveTemplateStockDelete(
	@ArchiveId int,
	@TemplateId int,
	@SecuCode varchar(10),
	@ReturnValue varchar(20) output
)
as
begin
	delete from archivetemplatestock
	where ArchiveId = @ArchiveId and TemplateId = @TemplateId and SecuCode = @SecuCode

	set @ReturnValue=@SecuCode+';'+convert(varchar, @TemplateId)+';'+convert(varchar, @ArchiveId)
end

go
if exists (select name from sysobjects where name='procArchiveTemplateStockDeleteAll')
drop proc procArchiveTemplateStockDeleteAll

go

create proc procArchiveTemplateStockDeleteAll(
	@ArchiveId int
)
as
begin
	delete from archivetemplatestock
	where ArchiveId = @ArchiveId
end


go
if exists (select name from sysobjects where name='procArchiveTemplateStockSelect')
drop proc procArchiveTemplateStockSelect

go

create proc procArchiveTemplateStockSelect(
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
	from archivetemplatestock a
	join securityinfo b
	on a.SecuCode = b.SecuCode and b.SecuType=2
	where a.ArchiveId = @ArchiveId
end

---=========================histtemplatestock end======================
