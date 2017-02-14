use tradingsystem


select * from tokenresourcepermission
where ResourceType=121

--delete the duplicated permission
delete from tokenresourcepermission
where Id in (
select max(aa.Id) from (
select a.Id, a.ResourceId from tokenresourcepermission a
join
(select Token, TokenType, ResourceId, ResourceType, Permission, count(Token) as Total
from tokenresourcepermission
group by Token, TokenType, ResourceId, ResourceType, Permission
having count(Token) > 1
) b
on a.Token=b.Token
and a.TokenType=b.TokenType
and a.ResourceId=b.ResourceId
and a.ResourceType=b.ResourceType) aa
group by aa.ResourceId)


select * from tokenresourcepermission
where Token=1 and TokenType=2 and ResourceId=1

select 