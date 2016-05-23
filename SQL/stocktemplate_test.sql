use tradingsystem

declare @today datetime
set @today=getdate()
exec procTemplateInsert '测试1', 1, 1, 100.00, '000001', @today, 1243

--@TemplateId int,
--@TemplateName varchar(50),
--@Status int,
--@WeightType int,
--@ReplaceType int,
--@FuturesCopies int,
--@MarketCapOpt numeric(5, 2),
--@BenchmarkId varchar(10),
--@ModifiedDate datetime,
--@CreatedUserId int
declare @today datetime
declare @ret int
set @today=getdate()
exec @ret = procTemplateUpdate 3, '测试1', 1, 1, 1, 1, 100.00, '000001', @today, 1243
select @ret


exec procSelectTemplate 11111

select * from stocktemplate
truncate table stocktemplate



insert into benchmark(
	BenchmarkId,
	BenchmarkName,
	Exchange,
	ContractMultiple
)values(
	'000016'
	,'上证50'
	,'SSE'
	,300
),(
	'000300'
	,'沪深300'
	,'SSE'
	,300
),(
	'000905'
	,'中证500'
	,'SSE'
	,200
),(
	'399300'
	,'沪深300'
	,'SZSE'
	,300
),(
	'399905'
	,'中证500'
	,'SZSE'
	,200
)

declare @newstid int
exec @newstid = procTemplateStockInsert 2, '600985', 1300, 13780, 1.02, 1.08
select @newstid

select * from templatestock
where TemplateId=3

declare @ret varchar(20)
exec procTemplateStockDelete 3, '000692', @ret output
select @ret

select * from benchmark

select * from securityinfo
where SecuType=1

exec procTemplateStockSelect @TemplateId=14