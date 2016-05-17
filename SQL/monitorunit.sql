use tradingsystem

if object_id('monitorunit') is not null
drop table monitorunit

create table monitorunit(
	MonitorUnitId		int identity(1, 1) primary key,
	MonitorUnitName		varchar(100) not null,
	AccountType			int, -- 1 单账户类型
	PortfolioId			int,
	BearContract		varchar(10),
	StockTemplateId		int,
	Active				int, -- 0 -inactive, 1 - active, 只有active的才会在开仓界面中可见
	Owner				varchar(10),
	CreatedDate			datetime,
	ModifiedDate		datetime
)

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
	@Owner varchar(10),
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

if exists (select name from sysobjects where name='procMonitorUnitSelect')
drop proc procMonitorUnitSelect

go

create proc procMonitorUnitSelect(
	@MonitorUnitId int = NULL
)
as
begin
	if @MonitorUnitId is not null or @MonitorUnitId=-1
	begin
		select MonitorUnitId
			,MonitorUnitName
			,AccountType
			,PortfolioId
			,BearContract
			,StockTemplateId
			,Active
			,Owner
			,CreatedDate
			,ModifiedDate
		from monitorunit
		where MonitorUnitId=@MonitorUnitId
	end
	else
	begin
		select MonitorUnitId
			,MonitorUnitName
			,AccountType
			,PortfolioId
			,BearContract
			,StockTemplateId
			,Active
			,Owner
			,CreatedDate
			,ModifiedDate
		from monitorunit
	end
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
		inner join portfolio b
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
		inner join portfolio b
		on a.PortfolioId = b.PortfolioId
		inner join stocktemplate c
		on a.StockTemplateId = c.TemplateId
	end
end

go
if exists (select name from sysobjects where name='procMonitorUnitSelectActive')
drop proc procMonitorUnitSelectActive

go

create proc procMonitorUnitSelectActive
as
begin
	select a.MonitorUnitId
		,a.MonitorUnitName
		,a.AccountType
		,a.PortfolioId
		,a.BearContract
		,a.StockTemplateId
		,a.Owner
		,a.CreatedDate
		,a.ModifiedDate
		,b.PortfolioName
		,c.TemplateName
	from monitorunit a
	inner join portfolio b
	on a.PortfolioId = b.PortfolioId
	inner join stocktemplate c
	on a.StockTemplateId = c.TemplateId
	where a.Active=1
end