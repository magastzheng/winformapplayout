use tradingsystem

declare @today datetime
set @today = getdate()
insert into fund(
	FundCode,
	FundName,
	CreatedDate,
	FundManager
)
values('850010', '海蓝宝银', @today, '000001'),
('852200','半年升', @today, '000002'),
('SF0007','海通宝银量化对冲1号', @today, '000003')

select * from fund

--=================
insert into assetunit
(
	AssetUnitCode,
	AssetUnitName,
	FundId,
	AssetUnitStatus,
	CanOverdraft,
	AssetType
)
values('30', '宝银资产单元1', 1, 1, 1, NULL),
('850010', '宝银股指期货', 1, 1, 1, NULL),
('850010A', '宝银资产单元2', 1, 1, 1, NULL)

insert into assetunit
(
	AssetUnitCode,
	AssetUnitName,
	FundId,
	AssetUnitStatus,
	CanOverdraft,
	AssetType
)
values('85220000', '量化先锋1期资产单元', 2, 1, 1, NULL)

select * from assetunit

insert into portfolio
(
	PortfolioCode,
	PortfolioName,
	AssetUnitId,
	FundId,
	PortfolioType, -- 1 个股组合, 2 基本组合
	PortfolioStatus, -- 1 正常，2 过期， 3 冻结
	FuturesInvestType -- a 投机,  b 套保, c 套利
)
values('10000000', '个股缺省组合', 1, 2, 1, 1, 'a'),
('30', 'RF买入', 2, 1, 1, 1, 'a'),
('40', 'sell', 2, 1, 1, 1, 'a')

select * from portfolio