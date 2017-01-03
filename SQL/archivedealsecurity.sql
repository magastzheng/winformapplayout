use tradingsystem

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