use tradingsystem

--==fund/asset/portfolio begin====

--++ufxportfolio begin++++
go 
if exists (select name from sysobjects where name='procUFXPortfolioInsert')
drop proc procUFXPortfolioInsert

go
create proc procUFXPortfolioInsert(
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
	declare @Total int
	declare @newid int

	set @Total=(select count(PortfolioCode) from ufxportfolio where PortfolioCode=@PortfolioCode)
	--only insert new item if there is no
	if @Total = 0
		begin
		insert into ufxportfolio(
			PortfolioCode,
			PortfolioName,
			AccountCode,
			AccountName,
			AccountType,
			AssetNo,
			AssetName,
			PortfolioStatus
		)
		values(
			@PortfolioCode,
			@PortfolioName,
			@AccountCode,
			@AccountName,
			@AccountType,
			@AssetNo,
			@AssetName,
			1
		)

		set @newid = SCOPE_IDENTITY()
	end
	else
	begin
		set @newid=-1
	end

	return @newid
end

go 
if exists (select name from sysobjects where name='procUFXPortfolioUpdateName')
drop proc procUFXPortfolioUpdateName

go
create proc procUFXPortfolioUpdateName(
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
if exists (select name from sysobjects where name='procUFXPortfolioUpdateStatus')
drop proc procUFXPortfolioUpdateStatus

go
create proc procUFXPortfolioUpdateStatus(
	@PortfolioCode varchar(20),
	@PortfolioStatus int
)
as
begin
	update ufxportfolio
	set PortfolioStatus = @PortfolioStatus
	where PortfolioCode = @PortfolioCode
end

go 
if exists (select name from sysobjects where name='procUFXPortfolioDelete')
drop proc procUFXPortfolioDelete

go
create proc procUFXPortfolioDelete(
	@PortfolioCode varchar(20) = NULL
)
as
begin
	delete from ufxportfolio where PortfolioCode=@PortfolioCode
end

go 
if exists (select name from sysobjects where name='procUFXPortfolioSelect')
drop proc procUFXPortfolioSelect

go
create proc procUFXPortfolioSelect(
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
			AssetName,
			PortfolioStatus
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
			AssetName,
			PortfolioStatus
		from ufxportfolio
	end
end


go 
if exists (select name from sysobjects where name='procUFXPortfolioSelectByUser')
drop proc procUFXPortfolioSelectByUser

go
create proc procUFXPortfolioSelectByUser(
	@UserId int,
	@PortfolioCode varchar(20) = NULL
)
as
begin
	if @PortfolioCode is not null
	begin
		select a.PortfolioId,
			a.PortfolioCode,
			a.PortfolioName,
			a.AccountCode,
			a.AccountName,
			a.AccountType,
			a.AssetNo,
			a.AssetName,
			a.PortfolioStatus,
			b.Permission
		from ufxportfolio a
		join tokenresourcepermission b
		on a.PortfolioId=b.ResourceId
			and b.ResourceType=103		--投资组合
			and b.Token = @UserId
			and b.TokenType = 2			--用户类型
		where a.PortfolioCode = @PortfolioCode
	end
	else
	begin
		select a.PortfolioId,
			a.PortfolioCode,
			a.PortfolioName,
			a.AccountCode,
			a.AccountName,
			a.AccountType,
			a.AssetNo,
			a.AssetName,
			a.PortfolioStatus,
			b.Permission
		from ufxportfolio a
		join tokenresourcepermission b
		on a.PortfolioId=b.ResourceId
			and b.ResourceType=103		--投资组合
			and b.Token = @UserId
			and b.TokenType = 2			--用户类型
	end
end


go 
if exists (select name from sysobjects where name='procUFXPortfolioSelectById')
drop proc procUFXPortfolioSelectById

go
create proc procUFXPortfolioSelectById(
	@PortfolioId int
)
as
begin
	select PortfolioId,
		PortfolioCode,
		PortfolioName,
		AccountCode,
		AccountName,
		AccountType,
		AssetNo,
		AssetName,
		PortfolioStatus
	from ufxportfolio
	where PortfolioId = @PortfolioId
end
--++ufxportfolio end++++

--==fund/asset/portfolio end====