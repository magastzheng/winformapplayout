use tradingsystem

---======创建角色
exec procRolesInsert @Id=1,@Name='管理员', @Status=1
exec procRolesInsert @Id=2,@Name='系统管理员', @Status=1
exec procRolesInsert @Id=5,@Name='基金经理', @Status=1
exec procRolesInsert @Id=6,@Name='交易员', @Status=1

---======分配权限
--**feature权限只分配'浏览'**废弃rolefeaturepermission表
--exec procRoleFeaturePermissionInsert @RoleId=1,@FeatureId=11,@Permission=16
--exec procRoleFeaturePermissionInsert @RoleId=1,@FeatureId=12,@Permission=16
--exec procRoleFeaturePermissionInsert @RoleId=1,@FeatureId=13,@Permission=16
--exec procRoleFeaturePermissionInsert @RoleId=1,@FeatureId=14,@Permission=16
--exec procRoleFeaturePermissionInsert @RoleId=1,@FeatureId=15,@Permission=16

--exec procRoleFeaturePermissionInsert @RoleId=1,@FeatureId=101,@Permission=16
--exec procRoleFeaturePermissionInsert @RoleId=1,@FeatureId=102,@Permission=16
--exec procRoleFeaturePermissionInsert @RoleId=1,@FeatureId=103,@Permission=16
--exec procRoleFeaturePermissionInsert @RoleId=1,@FeatureId=104,@Permission=16
--exec procRoleFeaturePermissionInsert @RoleId=1,@FeatureId=105,@Permission=16
--exec procRoleFeaturePermissionInsert @RoleId=1,@FeatureId=106,@Permission=16
--exec procRoleFeaturePermissionInsert @RoleId=1,@FeatureId=107,@Permission=16
--exec procRoleFeaturePermissionInsert @RoleId=1,@FeatureId=108,@Permission=16
--exec procRoleFeaturePermissionInsert @RoleId=1,@FeatureId=109,@Permission=16
--exec procRoleFeaturePermissionInsert @RoleId=1,@FeatureId=110,@Permission=16
--exec procRoleFeaturePermissionInsert @RoleId=1,@FeatureId=111,@Permission=16
--exec procRoleFeaturePermissionInsert @RoleId=1,@FeatureId=112,@Permission=16

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
exec procUserRoleInsert @UserId=2, @RoleId=1
exec procUserRoleInsert @UserId=1, @RoleId=5
exec procUserRoleInsert @UserId=3, @RoleId=6


---======测试用例
select * from roles

select * from features

select * from rolefeaturepermission
truncate table rolefeaturepermission
select * from userrole
select * from users

delete from tokenresourcepermission
where Token=1 and TokenType=1

delete from tokenresourcepermission
where Token=3 and TokenType=1