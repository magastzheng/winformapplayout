use tradingsystem

select * from [176.1.11.55].localwind.dbo.TB_OBJECT_1090
where F4_1090 ='A'             --A��
	and F27_1090 in ('SSE', 'SZSE')
	and F19_1090 <> 1	--������
	and F21_1090=1		--������
order by F17_1090 desc
		--and F5_1090 in ('�Ϻ�','����') --�������
		--and F6_1090 in ('����','��С��ҵ��','��ҵ��')    --���塢��С�塢��ҵ��

select a.*, b.* from [176.1.11.55].localwind.dbo.TB_OBJECT_1090 a
left join [176.1.11.55].localwind.dbo.TB_OBJECT_1120 b
on a.F2_1090=b.F1_1120 and b.F2_1120='20160519'
where a.F4_1090 ='A'             --A��
	and a.F27_1090 in ('SSE', 'SZSE')
	and a.F19_1090 <> 1	--������
	--and a.F21_1090=1		--������
	--and b.F21_1120 in ('����','��С��ҵ��','��ҵ��') 
	--and b.F22_1120 = 'A'
	--and b.F2_1120='20160519'

truncate table securityinfo

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
where F4_1090 ='A'             --A��
	and F27_1090 in ('SSE', 'SZSE')
	and F19_1090 <> 1	--������
	and F21_1090=1		--������


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
where F4_1090 ='S'             --A��
	and F27_1090 in ('SSE', 'SZSE')
	and F19_1090 <> 1	--������
	and F21_1090=1		--������
	and F16_1090 in (select BenchmarkId from benchmark)

select * from securityinfo
where SecuCode in (select BenchmarkId from benchmark)

select a.*, b.* from localwind.dbo.TB_OBJECT_1090 a
left join localwind.dbo.TB_OBJECT_1120 b
on b.F2_1120='20160519' and a.F2_1090=b.F1_1120
where a.F4_1090 ='A'             --A��
	and a.F27_1090 in ('SSE', 'SZSE')
	and a.F19_1090 <> 1	--������
	and a.F21_1090=1		--������

select * from [176.1.11.55].localwind.dbo.TB_OBJECT_1090
where F16_1090 in (select BenchmarkId from benchmark)