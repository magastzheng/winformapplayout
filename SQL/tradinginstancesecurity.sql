use tradingsystem

if object_id('tradinginstancesecurity') is not null
drop table tradinginstancesecurity

create table tradinginstancesecurity(
	InstanceId	int not null
	--,InstanceCode		varchar(20)
	,SecuCode	varchar(10)
	,PositionNumber	int	--持仓数量
	
)