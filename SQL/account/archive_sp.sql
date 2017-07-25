use tradingsystem

--==archive begin====

--++archivetradeinstance begin++++
go
if exists (select name from sysobjects where name='procArchiveTradeInstanceInsert')
drop proc procArchiveTradeInstanceInsert

go
create proc procArchiveTradeInstanceInsert(
	@InstanceId			int
	,@InstanceCode		varchar(20)
	,@PortfolioId		int
	,@MonitorUnitId		int
	,@StockDirection	int
	,@FuturesContract	varchar(10)
	,@FuturesDirection	int
	,@OperationCopies	int
	,@StockPriceType	int
	,@FuturesPriceType	int
	,@Status			int
	,@Owner				int
	,@CreatedDate		datetime
	,@ModifiedDate		datetime
	,@ArchiveDate		datetime
)
as
begin
	declare @newid int
	insert into archivetradeinstance(
		InstanceId
		,InstanceCode	
		,PortfolioId	
		,MonitorUnitId		
		,StockDirection		
		,FuturesContract	
		,FuturesDirection	
		,OperationCopies	
		,StockPriceType		
		,FuturesPriceType
		,Status	
		,Owner				
		,CreatedDate
		,ModifiedDate
		,ArchiveDate			
	)
	values(
		@InstanceId
		,@InstanceCode	
		,@PortfolioId	
		,@MonitorUnitId		
		,@StockDirection	
		,@FuturesContract	
		,@FuturesDirection	
		,@OperationCopies	
		,@StockPriceType	
		,@FuturesPriceType
		,@Status	
		,@Owner				
		,@CreatedDate		
		,@ModifiedDate
		,@ArchiveDate
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end

go
if exists (select name from sysobjects where name='procArchiveTradeInstanceDeleteByArchiveId')
drop proc procArchiveTradeInstanceDeleteByArchiveId

go
create proc procArchiveTradeInstanceDeleteByArchiveId(
	@ArchiveId	int
)
as
begin
	delete from archivetradeinstance
	where ArchiveId=@ArchiveId
end

go
if exists (select name from sysobjects where name='procArchiveTradeInstanceDeleteByInstanceId')
drop proc procArchiveTradeInstanceDeleteByInstanceId

go
create proc procArchiveTradeInstanceDeleteByInstanceId(
	@InstanceId	int
)
as
begin
	delete from archivetradeinstance
	where InstanceId=@InstanceId
end

go
if exists (select name from sysobjects where name='procArchiveTradeInstanceSelect')
drop proc procArchiveTradeInstanceSelect

go
create proc procArchiveTradeInstanceSelect(
	@ArchiveId		int
)
as
begin
	select
		ArchiveId
		,InstanceId
		,InstanceCode	
		,PortfolioId	
		,MonitorUnitId		
		,StockDirection		
		,FuturesContract	
		,FuturesDirection	
		,OperationCopies	
		,StockPriceType		
		,FuturesPriceType
		,Status	
		,Owner				
		,CreatedDate
		,ModifiedDate
		,ArchiveDate	
	from archivetradeinstance
	where ArchiveId=@ArchiveId
end
--++archivetradeinstance end++++

--++archivetradeinstancesecurity begin++++
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
--++archivetradeinstancesecurity end++++

--++archivetradecommand begin++++
go

if exists (select name from sysobjects where name='procArchiveTradeCommandInsert')
drop proc procArchiveTradeCommandInsert

go
create proc procArchiveTradeCommandInsert(
	@CommandId			int
	,@InstanceId		int
	,@CommandNum		int				
	,@ModifiedTimes		int				
	,@CommandType		int				
	,@ExecuteType		int				
	,@StockDirection	int				
	,@FuturesDirection	int				
	,@CommandStatus		int
	,@DispatchStatus	int				
	,@EntrustStatus		int				
	,@DealStatus		int				
	,@SubmitPerson		int				
	,@ModifiedPerson	int				
	,@CancelPerson		int				
	,@ApprovalPerson	int				
	,@DispatchPerson	int				
	,@ExecutePerson		int				
	,@CreatedDate		datetime		
	,@ModifiedDate		datetime		
	,@ArchiveDate		datetime
	,@StartDate			datetime		
	,@EndDate			datetime		
	,@ModifiedCause		varchar(100)	
	,@CancelCause		varchar(100)	
	,@ApprovalCause		varchar(100)	
	,@DispatchRejectCause	varchar(100)
	,@Notes		varchar(100)	
)
as
begin
	declare @newid int

	insert into archivetradecommand(
		CommandId			
		,InstanceId		
		,CommandNum		
		,ModifiedTimes		
		,CommandType		
		,ExecuteType		
		,StockDirection	
		,FuturesDirection	
		,CommandStatus		
		,DispatchStatus	
		,EntrustStatus		
		,DealStatus		
		,SubmitPerson		
		,ModifiedPerson	
		,CancelPerson		
		,ApprovalPerson	
		,DispatchPerson	
		,ExecutePerson		
		,CreatedDate		
		,ModifiedDate	
		,ArchiveDate	
		,StartDate			
		,EndDate			
		,ModifiedCause		
		,CancelCause		
		,ApprovalCause		
		,DispatchRejectCause
		,Notes		
	)
	values(
		@CommandId			
		,@InstanceId		
		,@CommandNum		
		,@ModifiedTimes		
		,@CommandType		
		,@ExecuteType		
		,@StockDirection	
		,@FuturesDirection	
		,@CommandStatus		
		,@DispatchStatus	
		,@EntrustStatus		
		,@DealStatus		
		,@SubmitPerson		
		,@ModifiedPerson	
		,@CancelPerson		
		,@ApprovalPerson	
		,@DispatchPerson	
		,@ExecutePerson		
		,@CreatedDate		
		,@ModifiedDate	
		,@ArchiveDate	
		,@StartDate			
		,@EndDate			
		,@ModifiedCause		
		,@CancelCause		
		,@ApprovalCause		
		,@DispatchRejectCause
		,@Notes		
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end

go

if exists (select name from sysobjects where name='procArchiveTradeCommandDelete')
drop proc procArchiveTradeCommandDelete

go
create proc procArchiveTradeCommandDelete(
	@ArchiveId	int	= NULL	--两个参数必须传入一个
	,@CommandId	int = NULL
)
as
begin
	if @ArchiveId is not null
	begin
		delete from archivetradecommand
		where ArchiveId=@ArchiveId
	end
	else if @CommandId is not null
	begin
		delete from archivetradecommand
		where CommandId=@CommandId
	end
	else
	begin
		raiserror('The parameter ArchiveId and CommandId are not NULL. It needs to pass one.', 16, -1)
	end
end

go

if exists (select name from sysobjects where name='procArchiveTradeCommandSelect')
drop proc procArchiveTradeCommandSelect

go
create proc procArchiveTradeCommandSelect(
	@ArchiveId	int
)
as
begin
	select
		ArchiveId
		,CommandId			
		,InstanceId		
		,CommandNum		
		,ModifiedTimes		
		,CommandType		
		,ExecuteType		
		,StockDirection	
		,FuturesDirection	
		,CommandStatus		
		,DispatchStatus	
		,EntrustStatus		
		,DealStatus		
		,SubmitPerson		
		,ModifiedPerson	
		,CancelPerson		
		,ApprovalPerson	
		,DispatchPerson	
		,ExecutePerson		
		,CreatedDate		
		,ModifiedDate	
		,ArchiveDate	
		,StartDate			
		,EndDate			
		,ModifiedCause		
		,CancelCause		
		,ApprovalCause		
		,DispatchRejectCause
		,Notes	
	from archivetradecommand
	where ArchiveId=@ArchiveId
end
--++archivetradecommand end++++

--++archivetradecommandsecurity begin++++
go
if exists (select name from sysobjects where name='procArchiveTradeCommandSecurityInsert')
drop proc procArchiveTradeCommandSecurityInsert

go
create proc procArchiveTradeCommandSecurityInsert(
	@ArchiveId			int
	,@CommandId			int 
	,@SecuCode			varchar(10) 
	,@SecuType			int
	,@CommandAmount		int
	,@CommandDirection	int
	,@CommandPrice		numeric(20, 4) --如果不限价，则价格设置为0
	,@EntrustStatus		int
)
as
begin
	insert into archivetradecommandsecurity(
		ArchiveId
		,CommandId			
		,SecuCode			
		,SecuType		
		,CommandAmount	
		,CommandDirection	
		,CommandPrice		
		,EntrustStatus		
	)values(
		@ArchiveId
		,@CommandId			
		,@SecuCode			
		,@SecuType	
		,@CommandAmount	
		,@CommandDirection
		,@CommandPrice		
		,@EntrustStatus		
	)
end

go
if exists (select name from sysobjects where name='procArchiveTradeCommandSecurityDelete')
drop proc procArchiveTradeCommandSecurityDelete

go
create proc procArchiveTradeCommandSecurityDelete(
	@ArchiveId	int
	,@CommandId int			= NULL
	,@SecuCode	varchar(10)	= NULL
)
as
begin
	if @CommandId is not null and @SecuCode is not null
	begin
		delete from archivetradecommandsecurity
		where ArchiveId=@ArchiveId
			and CommandId=@CommandId
			and SecuCode=@SecuCode
	end
	else if @CommandId is not null
	begin
		delete from archivetradecommandsecurity
		where ArchiveId=@ArchiveId
			and CommandId=@CommandId
	end
	else if @SecuCode is not null
	begin
		delete from archivetradecommandsecurity
		where ArchiveId=@ArchiveId
			and SecuCode=@SecuCode
	end
	else
	begin
		delete from archivetradecommandsecurity
		where ArchiveId=@ArchiveId
	end
end

go
if exists (select name from sysobjects where name='procArchiveTradeCommandSecuritySelect')
drop proc procArchiveTradeCommandSecuritySelect

go
create proc procArchiveTradeCommandSecuritySelect(
	@ArchiveId	int
	,@CommandId	int			= NULL
	,@SecuCode	varchar(10) = NULL
)
as
begin
	if @CommandId is not null and @SecuCode is not null
	begin
		select
			ArchiveId
			,CommandId		
			,SecuCode		
			,SecuType	
			,CommandAmount
			,CommandDirection		
			,CommandPrice	
			,EntrustStatus	
		from archivetradecommandsecurity 
		where ArchiveId=@ArchiveId 
			and CommandId=@CommandId
			and SecuCode=@SecuCode
	end
	else if @CommandId is not null
	begin
		select
			ArchiveId
			,CommandId		
			,SecuCode		
			,SecuType	
			,CommandAmount
			,CommandDirection		
			,CommandPrice	
			,EntrustStatus	
		from archivetradecommandsecurity 
		where ArchiveId=@ArchiveId 
			and CommandId=@CommandId
	end
	else if @SecuCode is not null
	begin
		select
			ArchiveId
			,CommandId		
			,SecuCode		
			,SecuType	
			,CommandAmount
			,CommandDirection		
			,CommandPrice	
			,EntrustStatus	
		from archivetradecommandsecurity 
		where ArchiveId=@ArchiveId 
			and SecuCode=@SecuCode
	end
	else
	begin
		select
			ArchiveId
			,CommandId		
			,SecuCode		
			,SecuType	
			,CommandAmount
			,CommandDirection		
			,CommandPrice	
			,EntrustStatus	
		from archivetradecommandsecurity 
		where ArchiveId=@ArchiveId 
	end
end
--++archivetradecommandsecurity end++++

--++archiveentrustcommand begin++++
go
if exists (select name from sysobjects where name='procArchiveEntrustCommandInsert')
drop proc procArchiveEntrustCommandInsert

go
create proc procArchiveEntrustCommandInsert(
	@SubmitId		int
	,@CommandId		int
	,@Copies		int
	,@EntrustNo		int
	,@BatchNo		int
	,@EntrustStatus	int
	,@DealStatus	int
	,@SubmitPerson	int
	,@ArchiveDate	datetime
	,@CreatedDate	datetime
	,@ModifiedDate	datetime
	,@EntrustFailCode	int
	,@EntrustFailCause	varchar(128)
)
as
begin
	declare @newid int
	insert into archiveentrustcommand(
		SubmitId
		,CommandId
		,Copies
		,EntrustNo
		,BatchNo
		,EntrustStatus
		,DealStatus
		,SubmitPerson
		,ArchiveDate
		,CreatedDate
		,ModifiedDate
		,EntrustFailCode
		,EntrustFailCause
	)
	values(
		@SubmitId
		,@CommandId
		,@Copies
		,@EntrustNo
		,@BatchNo
		,@EntrustStatus
		,@DealStatus
		,@SubmitPerson
		,@ArchiveDate
		,@CreatedDate
		,@ModifiedDate
		,@EntrustFailCode
		,@EntrustFailCause
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end

go
if exists (select name from sysobjects where name='procArchiveEntrustCommandDelete')
drop proc procArchiveEntrustCommandDelete

go
create proc procArchiveEntrustCommandDelete(
	@ArchiveId	int
)
as
begin
	delete from archiveentrustcommand
	where ArchiveId=@ArchiveId
end

go
if exists (select name from sysobjects where name='procArchiveEntrustCommandSelect')
drop proc procArchiveEntrustCommandSelect

go
create proc procArchiveEntrustCommandSelect(
	@CommandId	int
)
as
begin
	select
		ArchiveId
		,SubmitId
		,CommandId
		,Copies
		,EntrustNo
		,BatchNo
		,EntrustStatus
		,DealStatus
		,SubmitPerson
		,ArchiveDate
		,CreatedDate
		,ModifiedDate
		,EntrustFailCode
		,EntrustFailCause
	from archiveentrustcommand
	where CommandId=@CommandId
end

go
if exists (select name from sysobjects where name='procArchiveEntrustCommandSelectBySubmitId')
drop proc procArchiveEntrustCommandSelectBySubmitId

go
create proc procArchiveEntrustCommandSelectBySubmitId(
	@SubmitId	int
)
as
begin
	select
		ArchiveId
		,SubmitId
		,CommandId
		,Copies
		,EntrustNo
		,BatchNo
		,EntrustStatus
		,DealStatus
		,SubmitPerson
		,ArchiveDate
		,CreatedDate
		,ModifiedDate
		,EntrustFailCode
		,EntrustFailCause
	from archiveentrustcommand
	where SubmitId=@SubmitId
end
--++archiveentrustcommand end++++

--++archiveentrustsecurity begin++++
go
if exists (select name from sysobjects where name='procArchiveEntrustSecurityInsert')
drop proc procArchiveEntrustSecurityInsert

go
create proc procArchiveEntrustSecurityInsert(
	@ArchiveId			int
	,@RequestId			int
	,@SubmitId			int
	,@CommandId			int
	,@SecuCode			varchar(10)
	,@SecuType			int
	,@EntrustAmount		int
	,@EntrustPrice		numeric(20, 4)
	,@EntrustDirection	int
	,@EntrustStatus		int
	,@EntrustPriceType	int
	,@PriceType			int
	,@EntrustNo			int
	,@BatchNo			int
	,@DealStatus		int
	,@TotalDealAmount	int			 
	,@TotalDealBalance	numeric(20, 4)
	,@TotalDealFee		numeric(20, 4)
	,@DealTimes			int			
	,@EntrustDate		datetime
	,@CreatedDate		datetime
	,@ModifiedDate		datetime
	,@EntrustFailCode	int			
	,@EntrustFailCause	varchar(128)
)
as
begin
	insert into archiveentrustsecurity(
		ArchiveId
		,RequestId		
		,SubmitId		
		,CommandId		
		,SecuCode		
		,SecuType		
		,EntrustAmount	
		,EntrustPrice	
		,EntrustDirection
		,EntrustStatus	
		,EntrustPriceType
		,PriceType		
		,EntrustNo		
		,BatchNo		
		,DealStatus		
		,TotalDealAmount
		,TotalDealBalance
		,TotalDealFee	
		,DealTimes		
		,EntrustDate	
		,CreatedDate	
		,ModifiedDate	
		,EntrustFailCode
		,EntrustFailCause
	)values(
		@ArchiveId
		,@RequestId		
		,@SubmitId		
		,@CommandId		
		,@SecuCode		
		,@SecuType		
		,@EntrustAmount	
		,@EntrustPrice	
		,@EntrustDirection
		,@EntrustStatus	
		,@EntrustPriceType
		,@PriceType		
		,@EntrustNo		
		,@BatchNo		
		,@DealStatus		
		,@TotalDealAmount
		,@TotalDealBalance
		,@TotalDealFee	
		,@DealTimes		
		,@EntrustDate	
		,@CreatedDate	
		,@ModifiedDate	
		,@EntrustFailCode
		,@EntrustFailCause
	)	
end

go
if exists (select name from sysobjects where name='procArchiveEntrustSecurityDelete')
drop proc procArchiveEntrustSecurityDelete

go
create proc procArchiveEntrustSecurityDelete(
	@ArchiveId			int
)
as
begin
	delete from archiveentrustsecurity
	where ArchiveId=@ArchiveId
end

go
if exists (select name from sysobjects where name='procArchiveEntrustSecuritySelect')
drop proc procArchiveEntrustSecuritySelect

go
create proc procArchiveEntrustSecuritySelect(
	@ArchiveId			int
)
as
begin
	select
		ArchiveId
		,RequestId		
		,SubmitId		
		,CommandId		
		,SecuCode		
		,SecuType		
		,EntrustAmount	
		,EntrustPrice	
		,EntrustDirection
		,EntrustStatus	
		,EntrustPriceType
		,PriceType		
		,EntrustNo		
		,BatchNo		
		,DealStatus		
		,TotalDealAmount
		,TotalDealBalance
		,TotalDealFee	
		,DealTimes			
		,EntrustDate	
		,CreatedDate	
		,ModifiedDate	
		,EntrustFailCode
		,EntrustFailCause
	from archiveentrustsecurity
	where ArchiveId=@ArchiveId
end
--++archiveentrustsecurity end++++

--++archivedealsecurity begin++++
go


if exists (select name from sysobjects where name='procArchiveDealSecurityInsert')
drop proc procArchiveDealSecurityInsert

go
create proc procArchiveDealSecurityInsert(
	@ArchiveId			int	--归档Id从通过交易指令查询
	,@RequestId			int
	,@SubmitId			int
	,@CommandId			int
	,@SecuCode			varchar(10)
	,@DealNo			varchar(64)
	,@BatchNo			int
	,@EntrustNo			int
	,@ExchangeCode		varchar(10)
	,@AccountCode		varchar(32)
	,@PortfolioCode		varchar(20)
	,@StockHolderId		varchar(20)
	,@ReportSeat		varchar(10)
	,@DealDate			int
	,@DealTime			int
	,@EntrustDirection	int
	,@EntrustAmount		int
	,@EntrustState		int
	,@DealAmount		int
	,@DealPrice			numeric(20, 4)
	,@DealBalance		numeric(20, 4)
	,@DealFee			numeric(20, 4)
	,@TotalDealAmount	int
	,@TotalDealBalance	numeric(20, 4)
	,@CancelAmount		int
	,@ArchiveDate		datetime
)
as
begin
	insert into archivedealsecurity(
		ArchiveId			
		,RequestId			
		,SubmitId			
		,CommandId			
		,SecuCode			
		,DealNo				
		,BatchNo			
		,EntrustNo			
		,ExchangeCode		
		,AccountCode		
		,PortfolioCode		
		,StockHolderId		
		,ReportSeat			
		,DealDate			
		,DealTime			
		,EntrustDirection	
		,EntrustAmount		
		,EntrustState		
		,DealAmount			
		,DealPrice			
		,DealBalance		
		,DealFee			
		,TotalDealAmount	
		,TotalDealBalance	
		,CancelAmount		
		,ArchiveDate		
	)
	values(
		@ArchiveId			
		,@RequestId			
		,@SubmitId			
		,@CommandId			
		,@SecuCode			
		,@DealNo			
		,@BatchNo			
		,@EntrustNo			
		,@ExchangeCode		
		,@AccountCode		
		,@PortfolioCode		
		,@StockHolderId		
		,@ReportSeat		
		,@DealDate			
		,@DealTime			
		,@EntrustDirection	
		,@EntrustAmount		
		,@EntrustState		
		,@DealAmount		
		,@DealPrice			
		,@DealBalance		
		,@DealFee			
		,@TotalDealAmount	
		,@TotalDealBalance	
		,@CancelAmount		
		,@ArchiveDate		
	)
end

if exists (select name from sysobjects where name='procArchiveDealSecurityDelete')
drop proc procArchiveDealSecurityDelete

go
create proc procArchiveDealSecurityDelete(
	@ArchiveId	int
)
as
begin
	delete from archivedealsecurity
	where ArchiveId=@ArchiveId
end

if exists (select name from sysobjects where name='procArchiveDealSecuritySelect')
drop proc procArchiveDealSecuritySelect

go
create proc procArchiveDealSecuritySelect(
	@ArchiveId	int
)
as
begin
	select 
		ArchiveId			
		,RequestId			
		,SubmitId			
		,CommandId			
		,SecuCode			
		,DealNo				
		,BatchNo			
		,EntrustNo			
		,ExchangeCode		
		,AccountCode		
		,PortfolioCode		
		,StockHolderId		
		,ReportSeat			
		,DealDate			
		,DealTime			
		,EntrustDirection	
		,EntrustAmount		
		,EntrustState		
		,DealAmount			
		,DealPrice			
		,DealBalance		
		,DealFee			
		,TotalDealAmount	
		,TotalDealBalance	
		,CancelAmount		
		,ArchiveDate
	from archivedealsecurity
	where ArchiveId=@ArchiveId
end
--++archivedealsecurity end++++

--==archive end====