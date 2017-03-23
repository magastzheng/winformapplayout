use tradingsystem

select * from tradeinstance

select * from tradecommand

exec procTradeInstanceSelectCombine 

select * from monitorunit

select * from ufxportfolio

exec procTradeInstanceSelectCombine

select * from tradeinstance
where InstanceId=6

select * from tradeinstancesecurity
where InstanceId=6
order by SecuCode

select
	a.InstanceId			
	,a.InstanceCode	
	,a.PortfolioId	
	,a.MonitorUnitId	
	,a.TemplateId	
	,a.StockDirection		
	,a.FuturesContract	
	,a.FuturesDirection	
	,a.OperationCopies	
	,a.StockPriceType		
	,a.FuturesPriceType	
	,a.Status
	,a.Owner				
	,a.CreatedDate		
	,a.ModifiedDate	
	,b.PortfolioCode
	,b.PortfolioName
	,b.AccountCode
	,b.AccountName
	,b.AssetNo
	,b.AssetName
	,c.MonitorUnitName	
	,d.TemplateId
	,d.TemplateName
from tradeinstance a
inner join ufxportfolio b
on a.PortfolioId=b.PortfolioId
inner join monitorunit c
on a.MonitorUnitId = c.MonitorUnitId
left join stocktemplate d
on a.TemplateId = d.TemplateId

update t
set t.TemplateId=d.StockTemplateId
from tradeinstance t
join monitorunit d
on t.MonitorUnitId=d.MonitorUnitId

select * from users