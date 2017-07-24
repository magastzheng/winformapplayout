use tradingsystem

---=========================templatestock begin======================
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

---=========================templatestock end======================
