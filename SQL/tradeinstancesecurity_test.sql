use tradingsystem

select * from tradeinstancesecurity
where SecuCode='000421'

delete from tradeinstancesecurity
where InstanceId=4 and PositionType = 0

update tradeinstancesecurity
set PositionAmount=InstructionPreBuy


select * from tradeinstance
where InstanceId=20
