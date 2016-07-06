use tradingsystem


if object_id('ufxportfolio') is not null
drop table ufxportfolio

create table ufxportfolio
(
	PortfolioId int identity(1, 1) primary key,
	PortfolioCode varchar(20) not null,
	PortfolioName varchar(250) not null,
	AccountCode varchar(20),
	AccountName varchar(250),
	AccountType int, 
	AssetNo varchar(20),
	AssetName varchar(250)
)

go 
if exists (select name from sysobjects where name='procInsertUFXPortfolio')
drop proc procInsertUFXPortfolio

go
create proc procInsertUFXPortfolio(
	@PortfolioCode varchar(20),
	@PortfolioName varchar(250),
	@AccountCode varchar(20),
	@AccountName varchar(250),
	@AccountType int, 
	@AssetNo varchar(20),
	@AssetName varchar(250)
)
as
begin
	declare @newid int
	insert into ufxportfolio(
		PortfolioCode,
		PortfolioName,
		AccountCode,
		AccountName,
		AccountType,
		AssetNo,
		AssetName
	)
	values(
		@PortfolioCode,
		@PortfolioName,
		@AccountCode,
		@AccountName,
		@AccountType,
		@AssetNo,
		@AssetName
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end

go 
if exists (select name from sysobjects where name='procUpdateUFXPortfolio')
drop proc procUpdateUFXPortfolio

go
create proc procUpdateUFXPortfolio(
	@PortfolioCode varchar(20),
	@PortfolioName varchar(250)
)
as
begin
	update ufxportfolio
	set PortfolioName = @PortfolioName
	where PortfolioCode = @PortfolioCode
end

go 
if exists (select name from sysobjects where name='procGetUFXPortfolios')
drop proc procGetUFXPortfolios

go
create proc procGetUFXPortfolios(
	@PortfolioCode varchar(20) = NULL
)
as
begin
	if @PortfolioCode is not null
	begin
		select PortfolioId,
			PortfolioCode,
			PortfolioName,
			AccountCode,
			AccountName,
			AccountType,
			AssetNo,
			AssetName
		from ufxportfolio
		where PortfolioCode = @PortfolioCode
	end
	else
	begin
		select PortfolioId,
			PortfolioCode,
			PortfolioName,
			AccountCode,
			AccountName,
			AccountType,
			AssetNo,
			AssetName
		from ufxportfolio
	end
end

go 
if exists (select name from sysobjects where name='procDeleteUFXPortfolio')
drop proc procDeleteUFXPortfolio

go
create proc procDeleteUFXPortfolio(
	@PortfolioCode varchar(20) = NULL
)
as
begin
	delete from ufxportfolio where PortfolioCode=@PortfolioCode
end