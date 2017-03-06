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
