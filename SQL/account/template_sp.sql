use tradingsystem

--==template begin====

go
if exists (select name from sysobjects where name='procTemplateInsert')
drop proc procTemplateInsert

go

--++stocktemplate begin++++
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

--++stocktemplate end++++

--++templatestock begin++++
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
	@SecuCode varchar(10)
)
as
begin
	delete from templatestock
	where TemplateId = @TemplateId and SecuCode = @SecuCode
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
		TemplateId,
		SecuCode,
		Amount,
		MarketCap,
		MarketCapOpt,
		SettingWeight
	from templatestock
	where TemplateId=@TemplateId
end
--++templatestock end++++

--++stocktemplate end++++

--++monitor begin++++
go

if exists (select name from sysobjects where name='procMonitorUnitInsert')
drop proc procMonitorUnitInsert

go

create proc procMonitorUnitInsert(
	@MonitorUnitName varchar(100),
	@AccountType int,
	@PortfolioId int,
	@BearContract varchar(10),
	@StockTemplateId int,
	@Owner int,
	@CreatedDate datetime
)
as
begin
	declare @newid int
	
	insert into monitorunit(
		MonitorUnitName		
		,AccountType			
		,PortfolioId			
		,BearContract		
		,StockTemplateId	
		,Active	
		,Owner				
		,CreatedDate			
	)
	values(
		@MonitorUnitName		
		,@AccountType			
		,@PortfolioId			
		,@BearContract		
		,@StockTemplateId	
		,0	
		,@Owner				
		,@CreatedDate
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end

go

if exists (select name from sysobjects where name='procMonitorUnitUpdate')
drop proc procMonitorUnitUpdate

go

create proc procMonitorUnitUpdate(
	@MonitorUnitId int
	,@MonitorUnitName varchar(100)
	,@AccountType int
	,@PortfolioId int
	,@BearContract varchar(10)
	,@StockTemplateId int
	,@ModifiedDate datetime
)
as
begin
	update monitorunit
	set 
		MonitorUnitName		= @MonitorUnitName	
		,AccountType		= @AccountType			
		,PortfolioId		= @PortfolioId		
		,BearContract		= @BearContract	
		,StockTemplateId	= @StockTemplateId	
		,ModifiedDate		= @ModifiedDate
	where MonitorUnitId = @MonitorUnitId

	declare @newid int
	set @newid = @MonitorUnitId
	return @newid
end

go

if exists (select name from sysobjects where name='procMonitorUnitActive')
drop proc procMonitorUnitActive

go

create proc procMonitorUnitActive(
	@MonitorUnitId int
	,@Active int
)
as
begin
	if @Active is NULL or @Active < 0 or @Active > 1
	begin
		raiserror('Pass invalid parameter for @Active', 16, -1)
	end
	 
	if @MonitorUnitId is not null and @MonitorUnitId > 0
	begin
		update monitorunit
		set
			Active = @Active
		where MonitorUnitId=@MonitorUnitId
	end
end

go

if exists (select name from sysobjects where name='procMonitorUnitDelete')
drop proc procMonitorUnitDelete

go

create proc procMonitorUnitDelete(
	@MonitorUnitId int
)
as
begin
	delete from monitorunit where MonitorUnitId = @MonitorUnitId

	declare @newid int
	set @newid = @MonitorUnitId
	return @newid
end

go
if exists (select name from sysobjects where name='procMonitorUnitSelectCombine')
drop proc procMonitorUnitSelectCombine

go

create proc procMonitorUnitSelectCombine(
	@MonitorUnitId int = NULL
)
as
begin
	if @MonitorUnitId is not null or @MonitorUnitId=-1
	begin
		select a.MonitorUnitId
			,a.MonitorUnitName
			,a.AccountType
			,a.PortfolioId
			,a.BearContract
			,a.StockTemplateId
			,a.Active
			,a.Owner
			,a.CreatedDate
			,a.ModifiedDate
			,b.PortfolioName
			,c.TemplateName
		from monitorunit a
		inner join ufxportfolio b
		on a.PortfolioId = b.PortfolioId
		inner join stocktemplate c
		on a.StockTemplateId = c.TemplateId
		where MonitorUnitId=@MonitorUnitId
	end
	else
	begin
		select a.MonitorUnitId
			,a.MonitorUnitName
			,a.AccountType
			,a.PortfolioId
			,a.BearContract
			,a.StockTemplateId
			,a.Active
			,a.Owner
			,a.CreatedDate
			,a.ModifiedDate
			,b.PortfolioName
			,c.TemplateName
		from monitorunit a
		inner join ufxportfolio b
		on a.PortfolioId = b.PortfolioId
		inner join stocktemplate c
		on a.StockTemplateId = c.TemplateId
	end
end
--++monitor end++++
--==template end====