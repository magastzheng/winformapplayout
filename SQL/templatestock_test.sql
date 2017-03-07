use tradingsystem

select * from templatestock

declare @newstid int
exec @newstid = procTemplateStockInsert 2, '600985', 1300, 13780, 1.02, 1.08
select @newstid

select * from templatestock
where TemplateId=3

declare @ret varchar(20)
exec procTemplateStockDelete 3, '000692', @ret output
select @ret

select * from benchmark


exec procTemplateStockSelect @TemplateId=14

select 
	a.TemplateId,
	a.SecuCode,
	a.Amount,
	a.MarketCap,
	a.MarketCapOpt,
	a.SettingWeight,
	b.SecuName,
	b.ExchangeCode
from templatestock a
join securityinfo b
on a.SecuCode = b.SecuCode and b.SecuType=2
where a.TemplateId=1

select * from stocktemplate

select * from benchmark

select * from monitorunit