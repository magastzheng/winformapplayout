use tradingsystem

exec procUFXPortfolioSelect

declare @ret int
exec @ret = procUFXPortfolioInsert 'aaa', 'aname', '1111', 'fundname', 1, '2222', 'AssetName'
print @ret

--declare @ret int
exec @ret = procUFXPortfolioInsert 'bbb', 'aname', '1111', 'fundname', 1, '2222', 'AssetName'
print @ret


select *
from ufxportfolio
where PortfolioCode in (select PortfolioCode from ufxportfolio group by PortfolioCode having count(PortfolioCode) > 1)
order by PortfolioCode

with temp_cte as(
	select PortfolioCode, row_number() over(partition by PortfolioCode order by PortfolioCode) as rn
	from ufxportfolio
)
delete from temp_cte where rn > 1


delete from ufxportfolio
where PortfolioId not in
(
	select min(t1.PortfolioId)
	from ufxportfolio t1
	group by PortfolioCode
)

select * from ufxportfolio

select 
	PortfolioCode
	,AccountCode
	--,AccountType
	,count(PortfolioCode) as Total
from ufxportfolio
group by PortfolioCode, AccountCode--, AccountType
order by Total desc

--PortfolioId 9
select * from tokenresourcepermission
where ResourceId=9

select * from ufxportfolio
where PortfolioCode='30' and AccountCode='850010'

select * from tokenresourcepermission
where ResourceId=395

select * from tokenresourcepermission
where Token=1 and TokenType=2 and ResourceType=103

select * from ufxportfolio
where PortfolioId in (
select distinct ResourceId from tokenresourcepermission
where Token=1 and TokenType=2 and ResourceType=103)

select * from users