use tradingsystem

select * from ufxportfolio

select * from monitorunit

select * from tradinginstancesecurity

select * from tradinginstance

select * from entrustcommand

select * from entrustsecurity

update tradinginstance
set StockDirection=10

exec procMonitorUnitSelectActive

exec procTradingInstanceSelectCombine

exec procTradingCommandSelect -1

exec procTradingCommandSelectCombine -1

--===================Clear
truncate table monitorunit

truncate table tradinginstance

truncate table tradinginstancesecurity

truncate table tradingcommand

truncate table tradingcommandsecurity

truncate table entrustcommand

truncate table entrustsecurity