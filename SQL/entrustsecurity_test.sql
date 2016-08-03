use tradingsystem

select * from tradingcommand

select * from tradingcommandsecurity

select * from entrustcommand

select * from entrustsecurity

exec procEntrustSecuritySelectCancelRedo 2

select * from entrustsecurity
where CommandId=7