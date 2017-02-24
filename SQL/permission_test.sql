use tradingsystem

select * 
into Users_bak
from users

select *
into userrole_bak
from userrole

select *
into tokenresourcepermission_bak
from tokenresourcepermission

select * from users
select * from userrole
select * from tokenresourcepermission

insert into users(
	Operator
	,Name
	,Status
	,CreateDate
)
select 
	Operator
	,Name
	,Status
	,getdate()
from Users_bak
order by Id

insert into userrole(
	UserId
	,RoleId
	,CreateDate
)
select
	UserId
	,RoleId
	,getdate()
from userrole_bak
order by Id 

insert into tokenresourcepermission(
	Token
	,TokenType
	,ResourceId
	,ResourceType
	,Permission
	,CreateDate
)
select distinct
	Token
	,TokenType
	,ResourceId
	,ResourceType
	,Permission
	,getdate()
from tokenresourcepermission_bak
order by Id

exec procUsersSelect @Operator='10099'

select * from tokenresourcepermission
where ResourceType=121