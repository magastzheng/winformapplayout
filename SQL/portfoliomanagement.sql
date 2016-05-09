use tradingsystem

if object_id('fund') is not null
drop table fund

create table fund
(
	Id int identity(1, 1) primary key,
	FundCode varchar(10),
	FundName varchar(50),
	CreatedDate datetime,
	FundManager varchar(10)
)

if object_id('assetunit') is not null
drop table assetunit

create table assetunit
(
	Id int identity(1, 1) primary key,
	AssetUnitCode varchar(10),
	AssetUnitName varchar(50),
	FundId int,
	AssetUnitStatus int, -- 1 正常，2 过期， 3 冻结
	CanOverdraft int,  -- 1 允许透支; 2 不允许透支
	AssetType int -- 1 收益资产; 2 保本资产
)

if object_id('portfolio') is not null
drop table portfolio

create table portfolio
(
	PortfolioId int identity(1, 1) primary key,
	PortfolioCode varchar(10),
	PortfolioName varchar(50),
	AssetUnitId int,
	FundId int,
	PortfolioType int, -- 1 个股组合, 2 基本组合
	PortfolioStatus int, -- 1 正常，2 过期， 3 冻结
	FuturesInvestType varchar(2) -- a 投机,  b 套保, c 套利
)