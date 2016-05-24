use tradingsystem

if object_id('tradingsecurity') is not null
drop table tradingsecurity

create table tradingsecurity(
	InstanceId		int,
	InstanceCode	varchar(20),
	SecuCode		varchar(10),
	LongShort		int,
	DealAmount		int,
	DealPrice		numeric(20,4),
	DealDate		datetime
)
