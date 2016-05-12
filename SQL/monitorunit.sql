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
		,Owner				
		,CreatedDate			
	)
	values(
		@MonitorUnitName		
		,@AccountType			
		,@PortfolioId			
		,@BearContract		
		,@StockTemplateId		
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
			,Owner
			,CreatedDate
			,ModifiedDate
		from monitorunit
	end
end