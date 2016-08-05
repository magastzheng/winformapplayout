use tradingsystem

select * from ufxportfolio

select * from monitorunit

select * from tradinginstancesecurity

select * from tradinginstance

select * from entrustcommand

select * from entrustsecurity

select * from tradingcommand

update tradingcommand
set TargetNum=0
where CommandId=1

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

declare @dt datetime
set @dt=getdate()
exec procTradingCommandUpdateTargetNum 1, -1, @dt