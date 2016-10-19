use tradingsystem

---======创建角色
exec procRolesInsert '管理员', 1, 1
exec procRolesInsert '系统管理员', 1, 2
exec procRolesInsert '基金经理', 1, 5
exec procRolesInsert '交易员', 1, 6

---======分配权限
exec 


---======测试用例
select * from roles