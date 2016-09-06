--use tradingsystem

--if object_id('tradingsecurity') is not null
--drop table tradingsecurity

--create table tradingsecurity(
--	InstanceId		int,			--交易实例ID
--	InstanceCode	varchar(20),	--交易实例代码
--	SecuCode		varchar(10),	--证券代码(沪市、深市交易所和中金所代码)
--	LongShort		int,			--多头空头
--	DealAmount		int,			--成交量
--	DealPrice		numeric(20,4),	--成交价格
--	DealDate		datetime		--成交时间
--)
