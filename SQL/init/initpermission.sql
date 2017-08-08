use tradingsystem

---======设置第一个用户,用以保留前999个预留id
set identity_insert users on
insert into users(Id, Operator, Name, Status, CreateDate) values(1000, '9999999', '9999999', 0, getdate())
set identity_insert users off

---======创建角色
exec procRolesInsert @Id=1,@Name='管理员', @Status=1
exec procRolesInsert @Id=2,@Name='系统管理员', @Status=1
exec procRolesInsert @Id=5,@Name='基金经理', @Status=1
exec procRolesInsert @Id=6,@Name='交易员', @Status=1

--==添加功能列表，从配置文件navbar.js中读取
--**一级菜单
exec procFeaturesInsert @Id=11, @Code='strategytrading', @Name='策略交易', @Description='策略交易面板'
exec procFeaturesInsert @Id=12, @Code='strategycommand', @Name='策略指令', @Description='策略指令面板'
exec procFeaturesInsert @Id=13, @Code='portfoliomanagement', @Name='组合管理', @Description='组合管理面板'
exec procFeaturesInsert @Id=14, @Code='templatemanager', @Name='现货模板管理', @Description='现货模板管理面板'
exec procFeaturesInsert @Id=15, @Code='holdingmanagement', @Name='持仓管理', @Description='持仓管理交易面板'

--**二级菜单
exec procFeaturesInsert @Id=101, @Code='cmdtrading', @Name='指令交易', @Description='指令交易'
exec procFeaturesInsert @Id=102, @Code='open', @Name='期现开仓', @Description='期现开仓'
exec procFeaturesInsert @Id=103, @Code='close', @Name='期现平仓', @Description='期现平仓'
exec procFeaturesInsert @Id=104, @Code='commandmanager', @Name='指令管理', @Description='指令管理'
exec procFeaturesInsert @Id=105, @Code='monitorunit', @Name='监控单元', @Description='监控单元'
exec procFeaturesInsert @Id=106, @Code='fundmanagement', @Name='产品管理', @Description='产品管理'
exec procFeaturesInsert @Id=107, @Code='assetunitmanagement', @Name='资产单元管理', @Description='资产单元管理'
exec procFeaturesInsert @Id=108, @Code='portfoliomaintain', @Name='组合维护', @Description='组合维护'
exec procFeaturesInsert @Id=109, @Code='spottemplate', @Name='现货模板', @Description='现货模板'
exec procFeaturesInsert @Id=110, @Code='historicaltemplate', @Name='历史模板', @Description='历史模板'
exec procFeaturesInsert @Id=111, @Code='instancemanagement', @Name='实例管理', @Description='实例管理'
exec procFeaturesInsert @Id=112, @Code='holdingtransfer', @Name='持仓划转', @Description='持仓划转'

--**使用tokenresourcepermission存放所有权限数据
--==管理员
exec procTokenResourcePermissionInsert @Token=1,@TokenType=1,@ResourceId=11,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=1,@TokenType=1,@ResourceId=12,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=1,@TokenType=1,@ResourceId=13,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=1,@TokenType=1,@ResourceId=14,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=1,@TokenType=1,@ResourceId=15,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=1,@TokenType=1,@ResourceId=101,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=1,@TokenType=1,@ResourceId=102,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=1,@TokenType=1,@ResourceId=103,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=1,@TokenType=1,@ResourceId=104,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=1,@TokenType=1,@ResourceId=105,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=1,@TokenType=1,@ResourceId=106,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=1,@TokenType=1,@ResourceId=107,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=1,@TokenType=1,@ResourceId=108,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=1,@TokenType=1,@ResourceId=109,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=1,@TokenType=1,@ResourceId=110,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=1,@TokenType=1,@ResourceId=111,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=1,@TokenType=1,@ResourceId=112,@ResourceType=2,@Permission=16

--基金经理
--exec procTokenResourcePermissionInsert @Token=5,@TokenType=1,@ResourceId=11,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=5,@TokenType=1,@ResourceId=12,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=5,@TokenType=1,@ResourceId=13,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=5,@TokenType=1,@ResourceId=14,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=5,@TokenType=1,@ResourceId=15,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=5,@TokenType=1,@ResourceId=101,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=5,@TokenType=1,@ResourceId=102,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=5,@TokenType=1,@ResourceId=103,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=5,@TokenType=1,@ResourceId=104,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=5,@TokenType=1,@ResourceId=105,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=5,@TokenType=1,@ResourceId=106,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=5,@TokenType=1,@ResourceId=107,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=5,@TokenType=1,@ResourceId=108,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=5,@TokenType=1,@ResourceId=109,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=5,@TokenType=1,@ResourceId=110,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=5,@TokenType=1,@ResourceId=111,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=5,@TokenType=1,@ResourceId=112,@ResourceType=2,@Permission=16

--交易员
exec procTokenResourcePermissionInsert @Token=6,@TokenType=1,@ResourceId=11,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=6,@TokenType=1,@ResourceId=13,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=6,@TokenType=1,@ResourceId=101,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=6,@TokenType=1,@ResourceId=102,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=6,@TokenType=1,@ResourceId=106,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=6,@TokenType=1,@ResourceId=107,@ResourceType=2,@Permission=16
exec procTokenResourcePermissionInsert @Token=6,@TokenType=1,@ResourceId=108,@ResourceType=2,@Permission=16

--===添加用户角色
--系统管理员
--exec procUserRoleInsert @UserId=2, @RoleId=1
--基金经理
--exec procUserRoleInsert @UserId=1, @RoleId=5
--交易员
--exec procUserRoleInsert @UserId=1, @RoleId=6
--exec procUserRoleInsert @UserId=3, @RoleId=6
--先要查到新用户的Id
declare @userid int
set @userid=1001
--把用户设置为基金经理
exec procUserRoleInsert @UserId=@userid, @RoleId=5
--把用户设置为交易员
exec procUserRoleInsert @UserId=@userid, @RoleId=6