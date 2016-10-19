use tradingsystem

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

--==测试添加结果
select * from features