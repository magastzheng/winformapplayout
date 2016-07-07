select * from ufxportfolio

select * from monitorunit

select * from tradinginstancesecurity

select * from tradinginstance

update tradinginstance
set StockDirection=10

exec procMonitorUnitSelectActive

exec procTradingInstanceSelectCombine

truncate table monitorunit

truncate table tradinginstance

truncate table tradinginstancesecurity

truncate table tradingcommand

truncate table tradingcommandsecurity

truncate table entrustcommand

truncate table entrustsecurity