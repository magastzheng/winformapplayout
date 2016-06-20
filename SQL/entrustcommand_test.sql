use tradingsystem

truncate table entrustcommand

select * from entrustcommand
where CommandId=2



select * from entrustsecurity
truncate table entrustsecurity

truncate table tradingcommand

select * from tradingcommand

exec procEntrustCommandSelectByStatus 0

--exec procEntrustSecurityUpdateCancel 1, 

select * from entrustsecurity
where CommandId=1
