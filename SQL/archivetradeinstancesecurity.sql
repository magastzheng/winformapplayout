use tradingsystem

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