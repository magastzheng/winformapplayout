--=====创建数据库
create database tradingsystem on
(
	/*--数据文件的具体描述--*/
	name='tradingsystem', --主数据文件的逻辑名称
	filename='E:\SqlServerData\tradingsystem.mdf', --主数据文件的物理名称
	size=30mb,	--主数据文件的初始大小
	maxsize=unlimited, --主数据文件增长的最大值（一般不限）
	filegrowth=1mb --主数据文件的增长率
)
log on
(
	/*--日志文件的具体描述,各参数含义同上--*/
    name='tradingsystem_log',
	filename='E:\SqlServerData\tradingsystem_log.ldf',
	size=1mb,  
	maxsize=100gb,
	filegrowth=10%
)

--=====账户
--创建登录账户
create login qtrader with password='$Qlhtd[1584@}?', default_database=tradingsystem

--创建数据库账户
go
use tradingsystem
create user qtrader for login qtrader with default_schema=dbo
go

--加入数据库角色
go
exec sp_addrolemember 'db_owner', 'qtrader'
exec sp_addrolemember 'db_datareader', 'qtrader'
exec sp_addrolemember 'db_datawriter', 'qtrader'
go