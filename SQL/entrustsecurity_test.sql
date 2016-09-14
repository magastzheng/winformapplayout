use tradingsystem

select * from tradingcommand

select * from tradingcommandsecurity

select * from entrustcommand

select * from entrustsecurity

exec procEntrustSecuritySelectCancelRedo 2

select * from entrustsecurity
where CommandId=7

declare @guid uniqueidentifier
set @guid = newid()
print @guid

--51A97692-0632-4467-890C-FF6BC341EAE4
--79596E9F-E71B-4E76-8FB4-350D822BB2A2
