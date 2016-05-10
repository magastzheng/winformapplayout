use tradingsystem
---============只需要一张表即可===============

--if object_id('investment') is not null
--drop table investment

--create table investment
--(
--	Id					int identity(1, 1) primary key,
--	Type				int, -- 1 fund, 2 - asset unit, 3 - portfolio			
--	FundCode			varchar(10) not null,
--	FundName			varchar(50) not null,
--	ManagerCode			varchar(10),
--	AssetUnitCode		varchar(10),
--	AssetUnitName		varchar(50),
--	AssetUnitStatus		int, -- 1 正常，2 过期， 3 冻结
--	CanOverdraft		int,  -- 1 允许透支; 2 不允许透支
--	AssetType			int, -- 1 收益资产; 2 保本资产
--	PortfolioCode		varchar(10),
--	PortfolioName		varchar(50),
--	PortfolioType		int, -- 1 个股组合, 2 基本组合
--	PortfolioStatus		int, -- 1 正常，2 过期， 3 冻结
--	FuturesInvestType	varchar(2), -- a 投机,  b 套保, c 套利
--	CreatedDate			datetime,
--	ModifiedDate		datetime
--)

--go

--if exists (select name from sysobjects where name='procInvestmentFundInsert')
--drop proc procInvestmentFundInsert

--go

--create proc procInvestmentFundInsert(
--	@FundCode varchar(10),
--	@FundName varchar(50),
--	@CreatedDate datetime,
--	@FundManager varchar(10)
--)
--as
--begin
--	declare @newid int
	
--	insert into investment(
--		Type, FundCode, FundName, CreatedDate, ManagerCode
--	)
--	values(
--		1, @FundCode, @FundName, @CreatedDate, @FundManager
--	)

--	set @newid = SCOPE_IDENTITY()
--	return @newid
--end

--if exists (select name from sysobjects where name='procInvestmentFundUpdate')
--drop proc procInvestmentFundUpdate

--go
--create proc procInvestmentFundUpdate(
--	@FundCode varchar(10),
--	@FundName varchar(50),
--	@ModifiedDate datetime,
--	@ManagerCode varchar(10)
--)
--as
--begin
--	update investment
--	set
--		FundName = @FundName,
--		ModifiedDate = @ModifiedDate
--	where Type = 1 and FundCode = @FundCode
	
--	if @ManagerCode is not null
--	begin
--		update investment
--		set
--			ManagerCode = @ManagerCode
--		where Type = 1 and FundCode = @FundCode
--	end
--end

--go

--if exists (select name from sysobjects where name='procInvestmentFundDelete')
--drop proc procInvestmentFundDelete

--go
--create proc procInvestmentFundDelete(
--	@FundCode varchar(10)
--)
--as
--begin
--	--删除所有的fund/asset unit/portfolio
--	delete from investment where FundCode = @FundCode
--end

--go

--if exists (select name from sysobjects where name='procInvestmentFundQuery')
--drop proc procInvestmentFundQuery

--go
--create proc procInvestmentFundQuery(
--	@FundCode varchar(10) = NULL
--)
--as
--begin
--	if @FundCode is not null
--	begin
--		select Id, FundCode, FundName, ManagerCode, CreatedDate, ModifiedDate
--		from investment
--		where Type=1 and FundCode=@FundCode
--	end
--	else
--	begin
--		select Id, FundCode, FundName, ManagerCode, CreatedDate, ModifiedDate
--		from investment
--	end
--end

--go

--go 
--if exists (select name from sysobjects where name='procInvestmentAssetUnitInsert')
--drop proc procInvestmentAssetUnitInsert

--go
--create proc procInvestmentAssetUnitInsert(
--	@AssetUnitCode varchar(10),
--	@FundCode varchar(10),
--	@AssetUnitName varchar(50),
--	@AssetUnitStatus int,
--	@CanOverdraft int,
--	@AssetType int,
--	@CreatedDate datetime
--)
--as
--begin
--	declare @newid int
--	insert into investment(
--		Type,
--		AssetUnitCode,
--		FundCode,
--		AssetUnitName,
--		AssetUnitStatus,
--		CanOverdraft,
--		AssetType,
--		CreatedDate
--	)
--	values(
--		2,
--		@AssetUnitCode,
--		@FundCode,
--		@AssetUnitName,
--		@AssetUnitStatus,
--		@CanOverdraft,
--		@AssetType,
--		@CreatedDate
--	)

--	set @newid = SCOPE_IDENTITY()
--	return @newid
--end

--go 
--if exists (select name from sysobjects where name='procUpdateAssetUnit')
--drop proc procUpdateAssetUnit

--go
--create proc procUpdateAssetUnit(
--	@AssetUnitCode varchar(10),
--	@FundId int,
--	@AssetUnitName varchar(50),
--	@AssetUnitStatus int,
--	@CanOverdraft int,
--	@AssetType int,
--	@ModifiedDate datetime
--)
--as
--begin
--	update assetunit 
--	set
--		AssetUnitCode = @AssetUnitCode,
--		FundId = @FundId,
--		AssetUnitName = @AssetUnitName,
--		AssetUnitStatus = @AssetUnitStatus,
--		CanOverdraft = @CanOverdraft,
--		AssetType = @AssetType,
--		ModifiedDate = @ModifiedDate
--	where AssetUnitCode = @AssetUnitCode and FundId = @FundId
--end

--go 
--if exists (select name from sysobjects where name='procGetAssetUnits')
--drop proc procGetAssetUnits

--go
--create proc procGetAssetUnits(
--	@AssetUnitCode varchar(10) = NULL
--)
--as
--begin

--	--	AssetUnitId int identity(1, 1) primary key,
--	--	AssetUnitCode varchar(10) not null,
--	--	AssetUnitName varchar(50) not null,
--	--	FundId int,
--	--	AssetUnitStatus int, -- 1 正常，2 过期， 3 冻结
--	--	CanOverdraft int,  -- 1 允许透支; 2 不允许透支
--	--	AssetType int, -- 1 收益资产; 2 保本资产
--	--	CreatedDate datetime,
--	--	ModifiedDate datetime
--	if @AssetUnitCode is not null
--	begin
--		select AssetUnitId, 
--			AssetUnitCode, 
--			AssetUnitName,
--			FundId,
--			AssetUnitStatus,
--			CanOverdraft,
--			AssetType,
--			CreatedDate,
--			ModifiedDate
--		from assetunit
--		where AssetUnitCode = @AssetUnitCode
--	end
--	else
--	begin
--		select AssetUnitId, 
--			AssetUnitCode, 
--			AssetUnitName,
--			FundId,
--			AssetUnitStatus,
--			CanOverdraft,
--			AssetType,
--			CreatedDate,
--			ModifiedDate
--		from assetunit
--	end
--end

--go 
--if exists (select name from sysobjects where name='procGetAssetUnitsByFundId')
--drop proc procGetAssetUnitsByFundId

--go
--create proc procGetAssetUnitsByFundId(
--	@FundId int
--)
--as
--begin

--	select AssetUnitId, 
--		AssetUnitCode, 
--		AssetUnitName,
--		FundId,
--		AssetUnitStatus,
--		CanOverdraft,
--		AssetType,
--		CreatedDate,
--		ModifiedDate
--	from assetunit
--	where FundId = @FundId
	
--end

--go 
--if exists (select name from sysobjects where name='procDeleteAssetUnit')
--drop proc procDeleteAssetUnit

--go
--create proc procDeleteAssetUnit(
--	@AssetUnitCode varchar(10) = NULL
--)
--as
--begin
--	delete from assetunit where AssetUnitCode = @AssetUnitCode
--end


----============================================
---======
----============================================
if object_id('fund') is not null
drop table fund

create table fund
(
	FundId int identity(1, 1) primary key,
	FundCode varchar(10) not null,
	FundName varchar(50) not null,
	CreatedDate datetime,
	ModifiedDate datetime,
	ManagerCode varchar(10)
)

if object_id('assetunit') is not null
drop table assetunit

create table assetunit
(
	AssetUnitId int identity(1, 1) primary key,
	AssetUnitCode varchar(10) not null,
	AssetUnitName varchar(50) not null,
	FundId int,
	AssetUnitStatus int, -- 1 正常，2 过期， 3 冻结
	CanOverdraft int,  -- 1 允许透支; 2 不允许透支
	AssetType int, -- 1 收益资产; 2 保本资产
	CreatedDate datetime,
	ModifiedDate datetime
)

if object_id('portfolio') is not null
drop table portfolio

create table portfolio
(
	PortfolioId int identity(1, 1) primary key,
	PortfolioCode varchar(10) not null,
	PortfolioName varchar(50) not null,
	AssetUnitId int,
	FundId int,
	PortfolioType int, -- 1 个股组合, 2 基本组合
	PortfolioStatus int, -- 1 正常，2 过期， 3 冻结
	FuturesInvestType varchar(2), -- a 投机,  b 套保, c 套利
	CreatedDate datetime,
	ModifiedDate datetime
)

--=====fund table
go

if exists (select name from sysobjects where name='procInsertFund')
drop proc procInsertFund

go

create proc procInsertFund(
	@FundCode varchar(10),
	@FundName varchar(50),
	@CreatedDate datetime,
	@ManagerCode varchar(10)
)
as
begin
	declare @newid int
	
	insert into fund(
		FundCode, FundName, CreatedDate, ManagerCode
	)
	values(
		@FundCode, @FundName, @CreatedDate, @ManagerCode
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end

go

if exists (select name from sysobjects where name='procUpdateFund')
drop proc procUpdateFund

go
create proc procUpdateFund(
	@FundCode varchar(10),
	@FundName varchar(50),
	@ModifiedDate datetime,
	@ManagerCode varchar(10)
)
as
begin
	update fund
	set
		FundName = @FundName,
		ModifiedDate = @ModifiedDate
	where FundCode = @FundCode
	
	if @ManagerCode is not null
	begin
		update fund
		set
			ManagerCode = @ManagerCode
		where FundCode = @FundCode
	end
end

go

if exists (select name from sysobjects where name='procGetFunds')
drop proc procGetFunds

go
create proc procGetFunds(
	@FundCode varchar(10) = NULL
)
as
begin
	if @FundCode is not null
	begin
		select FundId, FundCode, FundName, ManagerCode, CreatedDate, ModifiedDate
		from fund
		where FundCode=@FundCode
	end
	else
	begin
		select FundId, FundCode, FundName, ManagerCode, CreatedDate, ModifiedDate
		from fund
	end
end

go

if exists (select name from sysobjects where name='procDeleteFund')
drop proc procDeleteFund

go
create proc procDeleteFund(
	@FundCode varchar(10) = NULL
)
as
begin
	delete from fund where FundCode=@FundCode
end
---==assetunit table

--AssetUnitCode varchar(10) not null,
--	AssetUnitName varchar(50) not null,
--	FundId int,
--	AssetUnitStatus int, -- 1 正常，2 过期， 3 冻结
--	CanOverdraft int,  -- 1 允许透支; 2 不允许透支
--	AssetType int, -- 1 收益资产; 2 保本资产
--	CreatedDate datetime,
--	ModifiedDate datetime

go 
if exists (select name from sysobjects where name='procInsertAssetUnit')
drop proc procInsertAssetUnit

go
create proc procInsertAssetUnit(
	@AssetUnitCode varchar(10),
	@FundId int,
	@AssetUnitName varchar(50),
	@AssetUnitStatus int,
	@CanOverdraft int,
	@AssetType int,
	@CreatedDate datetime
)
as
begin
	declare @newid int
	insert into assetunit(
		AssetUnitCode,
		FundId,
		AssetUnitName,
		AssetUnitStatus,
		CanOverdraft,
		AssetType,
		CreatedDate
	)
	values(
		@AssetUnitCode,
		@FundId,
		@AssetUnitName,
		@AssetUnitStatus,
		@CanOverdraft,
		@AssetType,
		@CreatedDate
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end

go 
if exists (select name from sysobjects where name='procUpdateAssetUnit')
drop proc procUpdateAssetUnit

go
create proc procUpdateAssetUnit(
	@AssetUnitCode varchar(10),
	@FundId int,
	@AssetUnitName varchar(50),
	@AssetUnitStatus int,
	@CanOverdraft int,
	@AssetType int,
	@ModifiedDate datetime
)
as
begin
	update assetunit 
	set
		AssetUnitCode = @AssetUnitCode,
		FundId = @FundId,
		AssetUnitName = @AssetUnitName,
		AssetUnitStatus = @AssetUnitStatus,
		CanOverdraft = @CanOverdraft,
		AssetType = @AssetType,
		ModifiedDate = @ModifiedDate
	where AssetUnitCode = @AssetUnitCode and FundId = @FundId
end

go 
if exists (select name from sysobjects where name='procGetAssetUnits')
drop proc procGetAssetUnits

go
create proc procGetAssetUnits(
	@AssetUnitCode varchar(10) = NULL
)
as
begin

	--	AssetUnitId int identity(1, 1) primary key,
	--	AssetUnitCode varchar(10) not null,
	--	AssetUnitName varchar(50) not null,
	--	FundId int,
	--	AssetUnitStatus int, -- 1 正常，2 过期， 3 冻结
	--	CanOverdraft int,  -- 1 允许透支; 2 不允许透支
	--	AssetType int, -- 1 收益资产; 2 保本资产
	--	CreatedDate datetime,
	--	ModifiedDate datetime
	if @AssetUnitCode is not null
	begin
		select AssetUnitId, 
			AssetUnitCode, 
			AssetUnitName,
			FundId,
			AssetUnitStatus,
			CanOverdraft,
			AssetType,
			CreatedDate,
			ModifiedDate
		from assetunit
		where AssetUnitCode = @AssetUnitCode
	end
	else
	begin
		select AssetUnitId, 
			AssetUnitCode, 
			AssetUnitName,
			FundId,
			AssetUnitStatus,
			CanOverdraft,
			AssetType,
			CreatedDate,
			ModifiedDate
		from assetunit
	end
end

go 
if exists (select name from sysobjects where name='procGetAssetUnitsByFundId')
drop proc procGetAssetUnitsByFundId

go
create proc procGetAssetUnitsByFundId(
	@FundId int
)
as
begin

	select AssetUnitId, 
		AssetUnitCode, 
		AssetUnitName,
		FundId,
		AssetUnitStatus,
		CanOverdraft,
		AssetType,
		CreatedDate,
		ModifiedDate
	from assetunit
	where FundId = @FundId
	
end

go 
if exists (select name from sysobjects where name='procDeleteAssetUnit')
drop proc procDeleteAssetUnit

go
create proc procDeleteAssetUnit(
	@AssetUnitCode varchar(10) = NULL
)
as
begin
	delete from assetunit where AssetUnitCode = @AssetUnitCode
end

--=====portfolio table
go 
if exists (select name from sysobjects where name='procInsertPortfolio')
drop proc procInsertPortfolio

go
create proc procInsertPortfolio(
	@PortfolioCode varchar(10),
	@PortfolioName varchar(50),
	@AssetUnitId int,
	@FundId int,
	@PortfolioType int,
	@PortfolioStatus int,
	@FuturesInvestType varchar(2),
	@CreatedDate datetime
)
as
begin
	--PortfolioCode varchar(10) not null,
	--	PortfolioName varchar(50) not null,
	--	AssetUnitId int,
	--	FundId int,
	--	PortfolioType int, -- 1 个股组合, 2 基本组合
	--	PortfolioStatus int, -- 1 正常，2 过期， 3 冻结
	--	FuturesInvestType varchar(2), -- a 投机,  b 套保, c 套利
	--	CreatedDate datetime,
	--	ModifiedDate datetime
	declare @newid int
	insert into portfolio(
		PortfolioCode,
		PortfolioName,
		AssetUnitId,
		FundId,
		PortfolioType,
		PortfolioStatus,
		FuturesInvestType,
		CreatedDate
	)
	values(
		@PortfolioCode,
		@PortfolioName,
		@AssetUnitId,
		@FundId,
		@PortfolioType,
		@PortfolioStatus,
		@FuturesInvestType,
		@CreatedDate
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end
go 
if exists (select name from sysobjects where name='procUpdatePortfolio')
drop proc procUpdatePortfolio

go
create proc procUpdatePortfolio(
	@PortfolioCode varchar(10),
	@PortfolioName varchar(50),
	--@AssetUnitId int,
	--@FundId int,
	@PortfolioType int,
	@PortfolioStatus int,
	@FuturesInvestType varchar(2),
	@ModifiedDate datetime
)
as
begin
	update portfolio
	set PortfolioName = @PortfolioName,
		PortfolioType = @PortfolioType,
		PortfolioStatus = @PortfolioStatus,
		FuturesInvestType = @FuturesInvestType,
		ModifiedDate = @ModifiedDate
	where PortfolioCode = @PortfolioCode
end
go 
if exists (select name from sysobjects where name='procGetPortfolios')
drop proc procGetPortfolios

go
create proc procGetPortfolios(
	@PortfolioCode varchar(10) = NULL
)
as
begin
	if @PortfolioCode is not null
	begin
		select PortfolioId,
			PortfolioCode,
			PortfolioName,
			AssetUnitId,
			FundId,
			PortfolioType,
			PortfolioStatus,
			FuturesInvestType,
			CreatedDate,
			ModifiedDate
		from portfolio
		where PortfolioCode = @PortfolioCode
	end
	else
	begin
		select PortfolioId,
			PortfolioCode,
			PortfolioName,
			AssetUnitId,
			FundId,
			PortfolioType,
			PortfolioStatus,
			FuturesInvestType,
			CreatedDate,
			ModifiedDate
		from portfolio
	end
end
go 
if exists (select name from sysobjects where name='procDeletePortfolio')
drop proc procDeletePortfolio

go
create proc procDeletePortfolio(
	@PortfolioCode varchar(10) = NULL
)
as
begin
	delete from portfolio where PortfolioCode=@PortfolioCode
end