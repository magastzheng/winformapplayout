use tradingsystem

select * from [176.1.11.55].localwind.dbo.TB_OBJECT_1090
where F4_1090 ='A'             --A股
	and F27_1090 in ('SSE', 'SZSE')
	and F19_1090 <> 1	--非退市
	and F21_1090=1		--已上市
order by F17_1090 desc
		--and F5_1090 in ('上海','深圳') --沪深交易所
		--and F6_1090 in ('主板','中小企业板','创业板')    --主板、中小板、创业板

select a.*, b.* from [176.1.11.55].localwind.dbo.TB_OBJECT_1090 a
left join [176.1.11.55].localwind.dbo.TB_OBJECT_1120 b
on a.F2_1090=b.F1_1120 and b.F2_1120='20160519'
where a.F4_1090 ='A'             --A股
	and a.F27_1090 in ('SSE', 'SZSE')
	and a.F19_1090 <> 1	--非退市
	--and a.F21_1090=1		--已上市
	--and b.F21_1120 in ('主板','中小企业板','创业板') 
	--and b.F22_1120 = 'A'
	--and b.F2_1120='20160519'

---=====**********初始化证券开始*********========------------
truncate table securityinfo

--加入股票
insert into securityinfo(
	SecuCode
	,SecuName
	,ExchangeCode
	,SecuType
	,ListDate	
)
select 
	F16_1090
	,OB_OBJECT_NAME_1090
	,F27_1090
	,2
	,F17_1090
from [176.1.11.55].localwind.dbo.TB_OBJECT_1090
where F4_1090 ='A'             --A股
	and F27_1090 in ('SSE', 'SZSE')
	and F19_1090 <> 1	--非退市
	and F21_1090=1		--已上市

--加入指数
insert into securityinfo(
	SecuCode
	,SecuName
	,ExchangeCode
	,SecuType
	,ListDate	
)
select 
	F16_1090
	,OB_OBJECT_NAME_1090
	,F27_1090
	,1
	,F17_1090
from [176.1.11.55].localwind.dbo.TB_OBJECT_1090
where F4_1090 ='S'             --A股
	and F27_1090 in ('SSE', 'SZSE')
	and F19_1090 <> 1	--非退市
	and F21_1090=1		--已上市
	and F16_1090 in (select BenchmarkId from benchmark)

--加入股指期货
insert into securityinfo(
	SecuCode
	,SecuName
	,ExchangeCode
	,SecuType
	,ListDate	
	,DeListDate
)
select 
	Code
	,Name
	,Exchange
	,3
	,convert(varchar, ListedDate, 112) as ListedDate
	,convert(varchar, LastDeliveryDay, 112) as LastDeliveryDay
from futurescontract
---=====**********初始化证券结束*********========------------


select * from securityinfo
where SecuCode in (select BenchmarkId from benchmark)

select a.*, b.* from localwind.dbo.TB_OBJECT_1090 a
left join localwind.dbo.TB_OBJECT_1120 b
on b.F2_1120='20160519' and a.F2_1090=b.F1_1120
where a.F4_1090 ='A'             --A股
	and a.F27_1090 in ('SSE', 'SZSE')
	and a.F19_1090 <> 1	--非退市
	and a.F21_1090=1		--已上市

select * from [176.1.11.55].localwind.dbo.TB_OBJECT_1090
where F16_1090 in (select BenchmarkId from benchmark)

select * from securityinfo
where SecuCode='IC1605'

select * from [176.1.11.55].localwind.dbo.TB_OBJECT_1090
where F16_1090 like 'IC16%'
	and F19_1090=0
	and F21_1090=1
	and F27_1090='CFFEX'

select * from futurescontract