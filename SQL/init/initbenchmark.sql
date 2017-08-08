use tradingsystem

--初始化基准
insert into benchmark(
	BenchmarkId,
	BenchmarkName,
	Exchange,
	ContractMultiple
)values(
	'000016'
	,'上证50'
	,'SSE'
	,300
),(
	'000300'
	,'沪深300'
	,'SSE'
	,300
),(
	'000905'
	,'中证500'
	,'SSE'
	,200
),(
	'399300'
	,'沪深300'
	,'SZSE'
	,300
),(
	'399905'
	,'中证500'
	,'SZSE'
	,200
)