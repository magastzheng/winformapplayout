use tradingsystem


select * from tokenresourcepermission
where ResourceType=121


select Token, TokenType, ResourceId, ResourceType, Permission,  count(Token) as Total
from tokenresourcepermission
group by Token, TokenType, ResourceId, ResourceType, Permission
having count(Token) > 1
order by Total desc

--select * from resources