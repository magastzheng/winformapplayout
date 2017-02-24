use tradingsystem

go

if object_id('archivetradeinstancesecurity') is not null
drop table archivetradeinstancesecurity

go

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
if exists (select name from sysobjects where name='procArchiveTradeInstanceSecurityInsert')
drop proc procArchiveTradeInstanceSecurityInsert

go
create proc procArchiveTradeInstanceSecurityInsert(
	@ArchiveId				int
	,@InstanceId			int
	,@SecuCode				varchar(10)
	,@SecuType				int
	,@PositionType			int
	,@PositionAmount		int
	,@InstructionPreBuy		int
	,@InstructionPreSell	int
	,@BuyBalance			numeric(20, 4)	
	,@SellBalance			numeric(20, 4)	
	,@DealFee				numeric(20, 4)  
	,@BuyToday				int				
	,@SellToday				int				
	,@CreatedDate			datetime		
	,@ModifiedDate			datetime		
	,@LastDate				datetime		
	,@ArchiveDate			datetime		
	,@RowId	varchar(20)		output
)
as
begin

	insert into archivetradeinstancesecurity(
		ArchiveId			
		,InstanceId			
		,SecuCode			
		,SecuType			
		,PositionType		
		,PositionAmount		
		,InstructionPreBuy	
		,InstructionPreSell	
		,BuyBalance			
		,SellBalance		
		,DealFee			
		,BuyToday			
		,SellToday			
		,CreatedDate		
		,ModifiedDate		
		,LastDate			
		,ArchiveDate
	)
	values(@ArchiveId			
		,@InstanceId			
		,@SecuCode			
		,@SecuType			
		,@PositionType		
		,@PositionAmount		
		,@InstructionPreBuy	
		,@InstructionPreSell	
		,@BuyBalance			
		,@SellBalance		
		,@DealFee			
		,@BuyToday			
		,@SellToday			
		,@CreatedDate		
		,@ModifiedDate		
		,@LastDate			
		,@ArchiveDate
		)

	set @RowId=@SecuCode+';'+cast(@InstanceId as varchar)+';'+cast(@ArchiveId as varchar)
end

go
if exists (select name from sysobjects where name='procArchiveTradeInstanceSecurityDeleteByArchiveId')
drop proc procArchiveTradeInstanceSecurityDeleteByArchiveId

go
create proc procArchiveTradeInstanceSecurityDeleteByArchiveId(
	@ArchiveId	int
)
as
begin
	delete from archivetradeinstancesecurity
	where ArchiveId=@ArchiveId
end

go
if exists (select name from sysobjects where name='procArchiveTradeInstanceSecurityDeleteByInstanceId')
drop proc procArchiveTradeInstanceSecurityDeleteByInstanceId

go
create proc procArchiveTradeInstanceSecurityDeleteByInstanceId(
	@InstanceId			int
)
as
begin
	delete from archivetradeinstancesecurity
	where InstanceId=@InstanceId
end

go
if exists (select name from sysobjects where name='procArchiveTradeInstanceSecuritySelect')
drop proc procArchiveTradeInstanceSecuritySelect

go
create proc procArchiveTradeInstanceSecuritySelect(
	@ArchiveId				int
)
as
begin
	select
		ArchiveId			
		,InstanceId			
		,SecuCode			
		,SecuType			
		,PositionType		
		,PositionAmount		
		,InstructionPreBuy	
		,InstructionPreSell	
		,BuyBalance			
		,SellBalance		
		,DealFee			
		,BuyToday			
		,SellToday			
		,CreatedDate		
		,ModifiedDate		
		,LastDate			
		,ArchiveDate
	from archivetradeinstancesecurity
	where ArchiveId=@ArchiveId
end