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
	AssetUnitStatus int
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
	PortfolioType int
)