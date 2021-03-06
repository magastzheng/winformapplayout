use tradingsystem

go
if exists (select name from sysobjects where name='procBenchmarkInsert')
drop proc procBenchmarkInsert

go
create proc procBenchmarkInsert(
	@BenchmarkId varchar(10),
	@BenchmarkName varchar(50),
	@Exchange varchar(10),
	@ContractMultiple int
)
as
begin
	insert into benchmark(
		BenchmarkId		
		,BenchmarkName	
		,Exchange		
		,ContractMultiple
	)
	values(
		@BenchmarkId		
		,@BenchmarkName	
		,@Exchange		
		,@ContractMultiple
	)
end

go
if exists (select name from sysobjects where name='procBenchmarkUpdate')
drop proc procBenchmarkUpdate

go
create proc procBenchmarkUpdate(
	@BenchmarkId varchar(10),
	@BenchmarkName varchar(50),
	@Exchange varchar(10),
	@ContractMultiple int
)
as
begin
	update benchmark
	set	BenchmarkName		= @BenchmarkName
		,Exchange			= @Exchange
		,ContractMultiple	= @ContractMultiple
	where BenchmarkId=@BenchmarkId
end

go
if exists (select name from sysobjects where name='procBenchmarkDelete')
drop proc procBenchmarkDelete

go
create proc procBenchmarkDelete(
	@BenchmarkId varchar(10)
)
as
begin
	delete from benchmark
	where BenchmarkId=@BenchmarkId
end

go
if exists (select name from sysobjects where name='procBenchmarkSelect')
drop proc procBenchmarkSelect

go
create proc procBenchmarkSelect(
	@BenchmarkId varchar(10) = NULL
)
as
begin
	if @BenchmarkId is not null and len(@BenchmarkId) > 0
	begin
		select
			BenchmarkId
			,BenchmarkName
			,Exchange
			,ContractMultiple
		from benchmark
		where BenchmarkId=@BenchmarkId
	end
	else
	begin
		select
			BenchmarkId
			,BenchmarkName
			,Exchange
			,ContractMultiple
		from benchmark
	end
end