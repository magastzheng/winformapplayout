use tradingsystem

--==account/user/permission begin====
go

if object_id('users') is not null
drop table users

create table users(
	Id int			identity(1, 1) primary key,		-- 用户ID，首次成功登录系统会自动产生
	Operator		varchar(10) not null,			-- 用户操作代码，用于登录
	Name			varchar(10),					-- 用户名称
	Status			int,							-- 用户状态：0 - inactive, 1 - active
	CreateDate		datetime,
	ModifiedDate	datetime
)

go

if object_id('userrole') is not null
drop table userrole

create table userrole(
	Id				int identity(1, 1) primary key
	,UserId			int not null	--用户Id
	,RoleId			int	not null	--角色Id	
	,CreateDate		datetime		--创建时间
	,ModifiedDate	datetime		--修改时间
)

go
if object_id('tokenresourcepermission') is not null
drop table tokenresourcepermission

create table tokenresourcepermission(
	Id				int identity(1, 1) primary key,
	Token			int not null,
	TokenType		int not null,
	ResourceId		int not null,			--使用(ResourceId, ResourceType）唯一的定位资源，
	ResourceType	int not null,			--不需要额外的resource表
	Permission		int,
	CreateDate		datetime,
	ModifiedDate	datetime
)

go
if object_id('features') is not null
drop table features

create table features(
	Id			int not null primary key,	--软件系统功能Id
	Code		varchar(30) not null,		--软件系统功能代码
	Name		varchar(30) not null,		--软件系统功能名称
	Description	varchar(100),				--软件系统功能描述
)

go

--if object_id('resources') is not null
--drop table resources

--create table resources(
--	Id		int identity(1, 1) primary key,
--	RefId	int not null,	--资源的实际Id
--	Type	int not null,	--资源类型: (RefId, Type)可以定义唯一的资源
--	Name	varchar(30),
--)

go
--==account/user/permission end====

--==setting begin======
go
if object_id('usersetting') is not null
drop table usersetting

create table usersetting(
	UserId						int not null primary key	--用户ID
	,ConnectTimeout				int	--连接超时时间
	,UFXTimeout					int	--UFX接口超时时间
	,UFXLimitEntrustRatio		int	--现货最小委托比例
	,UFXFutuLimitEntrustRatio	int	--期货最小委托比例
	,UFXOptLimitEntrustRatio	int	--期权最小委托比例
	,BuyFutuPrice				int	-- 0 市价，1 - 指定价，2 - 最新价， 3 - 自动盘口， 4 - 任意价， 
	,SellFutuPrice				int
	,BuySpotPrice				int
	,SellSpotPrice				int
	,BuySellEntrustOrder		int	--1 按市值从高到低，2-按市值从低到高， 3 - 按 中金所、深交所、上交所， 4 - 按上交所、深交所、中金所
	,OddShareMode				int	-- 1 四舍五入， 2 - 直接入， 3 - 直接舍
	,SZSEEntrustPriceType		int	-- a - 五档即成剩撤， b - 五档即成剩转
	,SSEEntrustPriceType		int	-- A - 五档即成剩撤， C - 即成剩撤，D - 对手方最优，E - 本方最优，F - 全额成或撤
	,CreatedDate				datetime	
	,ModifiedDate				datetime
)
go
--==setting end======

--==stock/benchmark/futures begin=====
go
if object_id('securityinfo') is not null
drop table securityinfo

create table securityinfo(
	SecuCode		varchar(10) not null,	--证券交易所代码
	SecuName		varchar(50),			--证券名称
	ExchangeCode	varchar(10),			--交易所代码
	SecuType		int,					--证券类型：1 - 指数， 2 - 股票， 3 - 股指期货
	ListDate		varchar(10),			--上市交易时间
	DeListDate		varchar(10),			--退市时间
	constraint pk_securityinfo_Id primary key(SecuCode, SecuType)
)

go
if object_id('futurescontract') is not null
drop table futurescontract

create table futurescontract(
	Code varchar(10) not null			--合约代码
	,Name varchar(50) not null			--合约名称
	,Exchange varchar(10) not null		--交易所代码
	,PriceLimits numeric(8, 2)			--涨跌幅限制(%)
	,Deposit numeric(8, 2)				--交易保证金(%)
	,ListedDate datetime				--合约上市日
	,LastTradingDay datetime			--最后交易日期
	,LastDeliveryDay datetime			--最后交割日
	,Status int							-- 1 正常交易, -1 过期
	,constraint pk_FuturesContract_Id primary key(Code)
)

go

if object_id('benchmark') is not null
drop table benchmark

create table benchmark(
	BenchmarkId			varchar(10) primary key,	--标的指数代码(交易所代码)
	BenchmarkName		varchar(50) not null,		--标的指数名称
	Exchange			varchar(10) not null,		--标的指数交易所
	ContractMultiple	int							--标的指数合约乘数:每个基点对应的价值
)
--==stock/benchmark end=====

--==fund/asset/portfolio begin======

if object_id('ufxportfolio') is not null
drop table ufxportfolio

create table ufxportfolio
(
	PortfolioId int identity(1, 1) primary key,	-- 组合ID
	PortfolioCode varchar(20) not null,			-- 组合代码
	PortfolioName varchar(250) not null,		-- 组合名称
	AccountCode varchar(32),					-- 基金代码
	AccountName varchar(250),					-- 基金名称
	AccountType int,			--基金类型：1 - 封闭式基金， 2 - 开放式基金, 3 - 社保基金, 5 - 年金产品, 6 - 专户产品, 
								--8 - 年金, 9 - 专户理财, 10 - 保险, 11 - 一对多专户, 12 - 定向理财, 13 - 集合理财,
								--14 - 自营, 15 - 信托, 16 - 私募, 17 - 委托资产
	AssetNo varchar(20),		--资产单元代码
	AssetName varchar(250),		--资产单元名称
	PortfolioStatus	int			--组合状态 1 active， 2 - inactive, -1 - none
)

--==fund/asset/portfolio end======

--==template/template stock begin===
go
if object_id('stocktemplate') is not null
drop table stocktemplate

create table stocktemplate(
	TemplateId int identity(1, 1) primary key,		--模板ID
	TemplateName varchar(50),						--模板名称
	Status int,										-- 1 - normal, 2 - inactive
	WeightType int,									-- 1 - 数量权重，2 - 比例权重
	ReplaceType int,								-- 0 - 个股替代，1 - 模板替代
	FuturesCopies int,								-- 期货份数
	MarketCapOpt numeric(5, 2),						-- 市值比例(%)
	BenchmarkId varchar(10),						-- 标的指数
	CreatedDate datetime,						
	ModifiedDate datetime,
	CreatedUserId int
)

go

if object_id('templatestock') is not null
drop table templatestock

create table templatestock(
	TemplateId int not null,		--模板ID
	SecuCode varchar(10) not null,	--证券代码
	Amount int,						--证券数量
	MarketCap numeric(20, 4),		--证券市值
	MarketCapOpt numeric(5, 2),		--证券市值比例(%)
	SettingWeight numeric(5, 2),	--证券设置权重(%)

	constraint pk_templatestock_Id primary key(TemplateId,SecuCode)
)

go
if object_id('monitorunit') is not null
drop table monitorunit

create table monitorunit(
	MonitorUnitId		int identity(1, 1) primary key, --监控模板ID
	MonitorUnitName		varchar(100) not null,			--监控模板名称
	AccountType			int,							--账户类型： 1 单账户类型， 2 - 多账户类型
	PortfolioId			int,							--组合ID
	BearContract		varchar(10),					--期货合约ID
	StockTemplateId		int,							--现货模板ID
	Active				int, -- 0 -inactive, 1 - active, 只有active的才会在开仓界面中可见
	Owner				int,
	CreatedDate			datetime,
	ModifiedDate		datetime
)


go

--==template/template stock end===

--==trade instance/trade command/trade security/entruct command/entrust security begin===
go
if object_id('tradecommand') is not null
drop table tradecommand

create table tradecommand(
	CommandId			int identity(1, 1) primary key	--指令序号
	,InstanceId			int not null					--交易实例ID
	,CommandNum			int								--指令份数
	,CommandStatus		int								--指令状态： 1 - 有效指令，2 - 已修改，3 - 已撤销， 4 - 委托完成， 5 - 已完成成交
	,ModifiedTimes		int								--修改次数
	,CommandType		int								-- 1 - 期现套利
	,ExecuteType		int								-- 1 开仓， 2 - 平仓
	,StockDirection		int								--10 -- 买入现货，11--卖出现货
	,FuturesDirection	int								--12-卖出开仓，13 -买入平仓
	,EntrustStatus		int								-- 1 - 未执行， 2 - 部分执行， 3- 已完成
	,DealStatus			int								-- 1 - 未成交， 2 - 部分成交， 3 - 已完成
	,SubmitPerson		int								--下达人
	,CreatedDate		datetime						-- 下达指令时间
	,ModifiedDate		datetime						-- 修改时间
	,CancelDate			datetime						-- 撤销时间
	,StartDate			datetime						-- 指令有效开始时间
	,EndDate			datetime						-- 指令有效结束时间
	,Notes				varchar(100)					-- 备注
	,ModifiedCause		varchar(100)					-- 修改原因
	,CancelCause		varchar(100)					-- 撤销原因
)

go
if object_id('tradecommandsecurity') is not null
drop table tradecommandsecurity

create table tradecommandsecurity(
	CommandId			int not null			--指令序号
	,SecuCode			varchar(10) not null	--证券交易所代码
	,SecuType			int						--证券类型 1 - 指数， 2 - 股票， 3 - 股指期货
	,CommandAmount		int						--指令数量
	,CommandDirection	int						--指令方向：10 - 买入股票， 11 - 卖出股票， 12 - 卖出开仓股指期货， 13 - 买入平仓股指期货
	,CommandPrice		numeric(20, 4)			--如果不限价，则价格设置为0
	,EntrustStatus		int						--委托状态：0 - 提交到数据库， 1 - 提交到UFX，2 - 未执行，3 - 部分执行，4 - 已完成， 10 - 撤单， 11 - 撤单到UFX, 12 - 撤单成功， -4 - 委托失败，-12 - 撤单失败
)

go

if object_id('tradeinstance') is not null
drop table tradeinstance

create table tradeinstance(
	InstanceId			int identity(1, 1) primary key	--交易实例ID
	,InstanceCode		varchar(20)						--交易实例代码
	,PortfolioId		int								--组合ID,唯一确定交易实例和组合之间的关系
	,MonitorUnitId		int								--监控单元ID，监控单元可以改变
	,TemplateId			int			--现货模板ID
	,StockDirection		int			--股票委托方向：1 - 买入， 2 - 卖出， 3 - 调整到[买卖]， 4 - 调整到[只买]， 5 - 调整到[只卖], 10 -- 买入现货，11--卖出现货，12-卖出开仓，13 -买入平仓
	,FuturesContract	varchar(10)	--股指期货合约代码
	,FuturesDirection	int			--股指期货委托方向：12-卖出开仓，13 -买入平仓
	,OperationCopies	int			--期货合约操作份数
	,StockPriceType		int			--股票价格类型： 0 - 不限价，1 - 最新价，A-J盘1至盘10
	,FuturesPriceType	int			--期货合约价格类型： 0 - 不限价，1 - 最新价， A-E盘1到盘5
	,Status				int			--交易实例状态 0 - 无效， 1 - 有效
	,Owner				int			--所有者
	,CreatedDate		datetime	--交易实例创建时间
	,ModifiedDate		datetime	--交易实例修改时间
	,Notes				varchar(100)--备注
)

go

if object_id('tradeinstancesecurity') is not null
drop table tradeinstancesecurity

--份数可以从模板中获取
create table tradeinstancesecurity(
	InstanceId			int not null	--实例Id
	--,InstanceCode		varchar(20)
	,SecuCode			varchar(10) not null		--证券代码
	,SecuType			int				--证券类型： 股票2， 期货3
	--,WeightAmount		int				--权重数量 直接从模板中获取
	,PositionType		int				--股票多头1，股票空头2，期货多头3， 期货空头4
	,PositionAmount		int				--持仓数量
	--,AvailableAmount	int				--可用数量
	,InstructionPreBuy	int				--指令预买数量
	,InstructionPreSell	int				--指令预卖数量
	,BuyBalance			numeric(20, 4)	--成本
	,SellBalance		numeric(20, 4)	--卖出金额
	,DealFee			numeric(20, 4)  --交易费用
	,BuyToday			int				--当日买量
	,SellToday			int				--当日卖量
	,CreatedDate		datetime		--创建时间
	,ModifiedDate		datetime		--修改时间
	,LastDate			datetime		--用于记录最近一天时间，该字段用于清算
	,constraint pk_TradeInstanceSecurity_IdSecuCode primary key(InstanceId, SecuCode)
)

go

if object_id('tradeinstanceadjustment') is not null
drop table tradeinstanceadjustment

create table tradeinstanceadjustment(
	Id int identity(1, 1) primary key	--序号
	,CreateDate datetime
	,SourceInstanceId	int
	,SourceFundCode	varchar(20)
	,SourcePortfolioCode varchar(20)
	,DestinationInstanceId int
	,DestinationFundCode varchar(20)
	,DestinationPortfolioCode varchar(20)
	,SecuCode varchar(10)
	,SecuType int
	,PositionType int
	,Price	decimal(20, 4)
	,Amount int
	,AdjustType int
	,Operator varchar(20)
	,StockHolderId varchar(20)
	,SeatNo	varchar(20)
	,Notes varchar(100)
)

go

--==trade instance/trade command/trade security/entruct command/entrust security end===

--==entrust command/entrust security begin==============

go
--==通过交易系统委托之后，将委托指令添加到本表，由于可以分多次进行委托
if object_id('entrustcommand') is not null
drop table entrustcommand

create table entrustcommand(
	SubmitId		int identity(1, 1) primary key	-- 指令提交ID,每次通过界面委托都会产生唯一的一个ID
	,CommandId		int not null					-- 指令ID
	,Copies			int								--指令份数
	,EntrustNo		int								--委托之后，服务器返回的委托号
	,BatchNo		int								--委托之后，服务器返回的批号
	,EntrustStatus	int								--委托状态	 4-已完成
	,DealStatus		int								--成交状态   1-未成交，2-部分成交，3-已完成
	,SubmitPerson	int								--提交人
	,CreatedDate	datetime						--提交时间	
	,ModifiedDate	datetime						--修改时间	
	,EntrustFailCode	int							--委托错误码
	,EntrustFailCause	varchar(1024)				--委托失败原因
)

go

if object_id('entrustsecurity') is not null
drop table entrustsecurity

create table entrustsecurity(
	RequestId			int identity(1, 1) primary key	--请求ID
	,SubmitId			int not null					--指令提交ID
	,CommandId			int not null					--指令ID
	,SecuCode			varchar(10) not null			--证券交易所代码
	,SecuType			int								--证券类型
	,EntrustAmount		int								--委托数量
	,EntrustPrice		numeric(20, 4)					--委托价格
	,EntrustDirection	int			 --委托方向：10 - 买入股票， 11 - 卖出股票， 12 - 卖出开仓， 13 - 买入平仓
	,EntrustStatus		int			 --委托状态： 0 - 提交到DB, 1 - 提交到UFX， 2 - 未执行， 3 - 部分执行， 4 - 已完成， 10 - 撤单DB, 11 - 撤单UFX, 12 - 撤单成功，(-4) - 委托失败， (-12) - 撤单失败
	,EntrustPriceType	int			 --委托价格类型： 0 - 限价，'a'五档即成剩撤(上交所市价)， 'A'五档即成剩撤(深交所市价)
	,PriceType			int			 --价格类型：委卖一， 委买一 ....
	,EntrustNo			int			 --委托之后，服务器返回的委托号
	,BatchNo			int			 --委托后返回的批号ID
	,ConfirmNo			varchar(32)	 --委托确认之后返回的确认号
	,DealStatus			int			 --成交状态：1 - 未成交， 2 - 部分成交， 3 - 已完成
	,TotalDealAmount	int			 --累计成交数量
	,TotalDealBalance	numeric(20, 4) --累计成交金额
	,TotalDealFee		numeric(20, 4) --累计费用
	,DealTimes			int			 -- 成交次数
	,EntrustDate		datetime	 -- 委托时间
	,CreatedDate		datetime	 -- 委托时间	
	,ModifiedDate		datetime	 -- 修改时间
	,EntrustFailCode	int			 --委托失败代码
	,EntrustFailCause	varchar(1024) --委托失败原因
)

go
--==entrust command/entrust security end==============

--==deal security begin======
if object_id('dealsecurity') is not null
drop table dealsecurity

create table dealsecurity(
	RequestId			int not null
	,SubmitId			int not null
	,CommandId			int not null
	,SecuCode			varchar(10) not null
	,DealNo				varchar(64)	not null
	,BatchNo			int not null
	,EntrustNo			int not null
	,ExchangeCode		varchar(10)
	,AccountCode		varchar(32)
	,PortfolioCode		varchar(20)
	,StockHolderId		varchar(20)
	,ReportSeat			varchar(10)
	,DealDate			int
	,DealTime			int
	,EntrustDirection	int
	,EntrustAmount		int
	,EntrustState		int
	,DealAmount			int
	,DealPrice			numeric(20, 4)
	,DealBalance		numeric(20, 4)
	,DealFee			numeric(20, 4)
	,TotalDealAmount	int
	,TotalDealBalance	numeric(20, 4)
	,CancelAmount		int
)
--==deal security end======

--==usage tracking begin=======
if object_id('useractiontracking') is not null
drop table useractiontracking

create table useractiontracking(
	Id				int identity(1, 1) primary key
	,UserId			int not null
	,CreatedDate	datetime
	,Action			int			--Model/UsageTracking/ActionType.cs
	,ResourceType	int			--Model/Permission/ResourceType.cs
	,ResourceId		int			
	,Num			int
	,ActionStatus	int			--Model/UsageTracking/ActionStatus
	,Details		varchar(3000)
)
go
--==usage tracking end=======

--==archive begin======
go
if object_id('archivetradeinstance') is not null
drop table archivetradeinstance

create table archivetradeinstance(
	ArchiveId			int identity(1, 1) primary key	--归档ID
	,InstanceId			int	not null					--交易实例ID
	,InstanceCode		varchar(20)						--交易实例代码
	,PortfolioId		int								--组合ID,唯一确定交易实例和组合之间的关系
	,MonitorUnitId		int								--监控单元ID，监控单元可以改变
	,StockDirection		int			--股票委托方向：1 - 买入， 2 - 卖出， 3 - 调整到[买卖]， 4 - 调整到[只买]， 5 - 调整到[只卖], 10 -- 买入现货，11--卖出现货，12-卖出开仓，13 -买入平仓
	,FuturesContract	varchar(10)	--股指期货合约代码
	,FuturesDirection	int			--股指期货委托方向：12-卖出开仓，13 -买入平仓
	,OperationCopies	int			--期货合约操作份数
	,StockPriceType		int			--股票价格类型： 0 - 不限价，1 - 最新价，A-J盘1至盘10
	,FuturesPriceType	int			--期货合约价格类型： 0 - 不限价，1 - 最新价， A-E盘1到盘5
	,Status				int			--交易实例状态 0 - 无效， 1 - 有效
	,Owner				int			--所有者
	,ArchiveDate		datetime	--归档时间
	,CreatedDate		datetime	--交易实例创建时间
	,ModifiedDate		datetime	--交易实例修改时间
)

go

if object_id('archivetradeinstancesecurity') is not null
drop table archivetradeinstancesecurity

create table archivetradeinstancesecurity(
	ArchiveId			int not null	--归档Id
	,InstanceId			int not null	--实例Id
	,SecuCode			varchar(10) not null		--证券代码
	,SecuType			int				--证券类型： 股票2， 期货3
	,PositionType		int				--股票多头1，股票空头2，期货多头3， 期货空头4
	,PositionAmount		int				--持仓数量
	,InstructionPreBuy	int				--指令预买数量
	,InstructionPreSell	int				--指令预卖数量
	,BuyBalance			numeric(20, 4)	--成本
	,SellBalance		numeric(20, 4)	--卖出金额
	,DealFee			numeric(20, 4)  --交易费用
	,BuyToday			int				--当日买量
	,SellToday			int				--当日卖量
	,CreatedDate		datetime		--创建时间
	,ModifiedDate		datetime		--修改时间
	,LastDate			datetime		--用于记录最近一天时间，该字段用于清算
	,ArchiveDate		datetime		--归档日期
)

go
if object_id('archivetradecommand') is not null
drop table archivetradecommand

create table archivetradecommand(
	ArchiveId			int identity(1, 1) primary key
	,CommandId			int not null					--指令序号
	,InstanceId			int not null					--交易实例ID
	,CommandNum			int								--指令份数
	,ModifiedTimes		int								--修改次数
	,CommandType		int								-- 1 - 期现套利
	,ExecuteType		int								-- 1 开仓， 2 - 平仓
	,StockDirection		int								--10 -- 买入现货，11--卖出现货
	,FuturesDirection	int								--12-卖出开仓，13 -买入平仓
	,CommandStatus		int not null					--指令状态：1 - 有效指令，2 - 已修改，3 - 已撤销, 4 - 委托完成， 5 - 已完成成交
	,DispatchStatus		int								--分发状态：1 - 未分发，2 - 已分发
	,EntrustStatus		int								-- 1 - 未执行， 2 - 部分执行， 3- 已完成
	,DealStatus			int								-- 1 - 未成交， 2 - 部分成交， 3 - 已完成
	,SubmitPerson		int								--下达人
	,ModifiedPerson		int								--修改人
	,CancelPerson		int								--撤销人
	,ApprovalPerson		int								--审批人
	,DispatchPerson		int								--分发人
	,ExecutePerson		int								--执行人
	,CreatedDate		datetime						-- 下达指令时间
	,ModifiedDate		datetime						-- 修改时间
	,ArchiveDate		datetime						-- 归档时间
	,StartDate			datetime						-- 指令有效开始时间
	,EndDate			datetime						-- 指令有效结束时间
	,ModifiedCause		varchar(100)					-- 修改指令原因
	,CancelCause		varchar(100)					-- 撤销指令原因
	,ApprovalCause		varchar(100)					-- 审批原因
	,DispatchRejectCause	varchar(100)				-- 分发拒绝原因
	,Notes		varchar(100)					-- 指令备注
)

go

if object_id('archivetradecommandsecurity') is not null
drop table archivetradecommandsecurity

create table archivetradecommandsecurity(
	ArchiveId			int not null			--表archivetradecommand中的ArchiveId
	,CommandId			int not null			--指令序号
	,SecuCode			varchar(10) not null	--证券交易所代码
	,SecuType			int						--证券类型 1 - 指数， 2 - 股票， 3 - 股指期货
	,CommandAmount		int						--指令数量
	,CommandDirection	int						--指令方向：10 - 买入股票， 11 - 卖出股票， 12 - 卖出开仓股指期货， 13 - 买入平仓股指期货
	,CommandPrice		numeric(20, 4)			--如果不限价，则价格设置为0
	,EntrustStatus		int						--委托状态：0 - 提交到数据库， 1 - 提交到UFX，2 - 未执行，3 - 部分执行，4 - 已完成， 10 - 撤单， 11 - 撤单到UFX, 12 - 撤单成功， -4 - 委托失败，-12 - 撤单失败
)

go

--==通过交易系统委托之后，将委托指令添加到本表，由于可以分多次进行委托
if object_id('archiveentrustcommand') is not null
drop table archiveentrustcommand

create table archiveentrustcommand(
	ArchiveId		int identity(1, 1) primary key
	,SubmitId		int not null	-- 指令提交ID,每次通过界面委托都会产生唯一的一个ID
	,CommandId		int not null					-- 指令ID
	,Copies			int								--指令份数
	,EntrustNo		int								--委托之后，服务器返回的委托号
	,BatchNo		int								--委托之后，服务器返回的批号
	,EntrustStatus	int								--委托状态
	,DealStatus		int								--成交状态
	,SubmitPerson	int								--提交人
	,ArchiveDate	datetime						--归档时间
	,CreatedDate	datetime						--提交时间	
	,ModifiedDate	datetime						--修改时间	
	,EntrustFailCode	int							--委托错误码
	,EntrustFailCause	varchar(128)				--委托失败原因
)

go

if object_id('archiveentrustsecurity') is not null
drop table archiveentrustsecurity

create table archiveentrustsecurity(
	ArchiveId			int not null
	,RequestId			int not null					--请求ID
	,SubmitId			int not null					--指令提交ID
	,CommandId			int not null					--指令ID
	,SecuCode			varchar(10) not null			--证券交易所代码
	,SecuType			int								--证券类型
	,EntrustAmount		int								--委托数量
	,EntrustPrice		numeric(20, 4)					--委托价格
	,EntrustDirection	int			 --委托方向：10 - 买入股票， 11 - 卖出股票， 12 - 卖出开仓， 13 - 买入平仓
	,EntrustStatus		int			 --委托状态： 0 - 提交到DB, 1 - 提交到UFX， 2 - 未执行， 3 - 部分执行， 4 - 已完成， 10 - 撤单DB, 11 - 撤单UFX, 12 - 撤单成功， 13 - 撤单失败
	,EntrustPriceType	int			 --委托价格类型： 0 - 限价，'a'五档即成剩撤(上交所市价)， 'A'五档即成剩撤(深交所市价)
	,PriceType			int			 --价格类型：委卖一， 委买一 ....
	,EntrustNo			int			 --委托之后，服务器返回的委托号
	,BatchNo			int			 --委托后返回的批号ID
	,DealStatus			int			 --成交状态：1 - 未成交， 2 - 部分成交， 3 - 已完成
	,TotalDealAmount	int			 --累计成交数量
	,TotalDealBalance	numeric(20, 4) --累计成交金额
	,TotalDealFee		numeric(20, 4) --累计费用
	,DealTimes			int			 -- 成交次数
	,EntrustDate		datetime	 -- 委托时间
	,CreatedDate		datetime	 -- 委托时间	
	,ModifiedDate		datetime	 -- 修改时间
	,EntrustFailCode	int			 --委托失败代码
	,EntrustFailCause	varchar(128) --委托失败原因
)

go

if object_id('archivedealsecurity') is not null
drop table archivedealsecurity

--ArchiveId从何而来
 create table archivedealsecurity(
	ArchiveId			int not null	--归档Id从通过交易指令查询
	,RequestId			int not null
	,SubmitId			int not null
	,CommandId			int not null
	,SecuCode			varchar(10) not null
	,DealNo				varchar(64)	not null
	,BatchNo			int not null
	,EntrustNo			int not null
	,ExchangeCode		varchar(10)
	,AccountCode		varchar(32)
	,PortfolioCode		varchar(20)
	,StockHolderId		varchar(20)
	,ReportSeat			varchar(10)
	,DealDate			int
	,DealTime			int
	,EntrustDirection	int
	,EntrustAmount		int
	,EntrustState		int
	,DealAmount			int
	,DealPrice			numeric(20, 4)
	,DealBalance		numeric(20, 4)
	,DealFee			numeric(20, 4)
	,TotalDealAmount	int
	,TotalDealBalance	numeric(20, 4)
	,CancelAmount		int
	,ArchiveDate		datetime
 )

go
--==archive end======
