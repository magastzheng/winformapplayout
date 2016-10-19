use tradingsystem

---======创建角色
exec procRolesInsert '管理员', 1, 1
exec procRolesInsert '系统管理员', 1, 2
exec procRolesInsert '基金经理', 1, 5
exec procRolesInsert '交易员', 1, 6

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
exec procTokenResourcePermissionInsert @Token=1,@TokenType=1,@ResourceId=11,@ResourceType=2,@Permission=16


---======测试用例
select * from roles

select * from rolefeaturepermission
truncate table rolefeaturepermission