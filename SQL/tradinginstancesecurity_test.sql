use tradingsystem

select * from tradinginstancesecurity
where SecuCode='000421'

delete from tradinginstancesecurity
where InstanceId=4 and PositionType = 0

update tradinginstancesecurity
set PositionAmount=InstructionPreBuy


select * from tradinginstance
where InstanceId=20
