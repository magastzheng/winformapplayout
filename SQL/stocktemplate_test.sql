use tradingsystem

declare @today datetime
set @today=getdate()
exec procInsertTemplate '����1', 1, 1, 100.00, '000001', @today, 1243

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
exec @ret = procUpdateTemplate 3, '����1', 1, 1, 1, 1, 100.00, '000001', @today, 1243
select @ret


exec procSelectTemplate 11111

select * from stocktemplate
truncate table stocktemplate



insert into benchmark(
	BenchmarkId,
	BenchmarkName,
	Exchange
)values(
	'000016'
	,'��֤50'
	,'sh'
),(
	'000300'
	,'����300'
	,'sh'
),(
	'000905'
	,'��֤500'
	,'sh'
),(
	'399300'
	,'����300'
	,'sz'
),(
	'399905'
	,'��֤500'
	,'sz'
)