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
	AssetUnitStatus
)
values('30', '宝银资产单元1', 1, 1),
('850010', '宝银股指期货', 1, 1),
('850010A', '宝银资产单元2', 1, 1)

insert into assetunit
(
	AssetUnitCode,
	AssetUnitName,
	FundId,
	AssetUnitStatus
)
values('85220000', '量化先锋1期资产单元', 2, 1)

select * from assetunit