use tradingsystem

go
if exists (select name from sysobjects where name='procDealSecurityInsert')
drop proc procDealSecurityInsert

go
create proc procDealSecurityInsert(
	@RequestId			int
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
)
as
begin

	insert into dealsecurity(
		RequestId			
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
	)
	values(
		@RequestId			
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
	)

end

go
if exists (select name from sysobjects where name='procDealSecurityDeleteByDealNo')
drop proc procDealSecurityDeleteByDealNo

go
create proc procDealSecurityDeleteByDealNo(
	@DealNo			varchar(64)
)
as
begin
	delete from dealsecurity 
	where DealNo=@DealNo
end

go
if exists (select name from sysobjects where name='procDealSecurityDeleteBySubmitId')
drop proc procDealSecurityDeleteBySubmitId

go
create proc procDealSecurityDeleteBySubmitId(
	@SubmitId	int
)
as
begin
	delete from dealsecurity 
	where SubmitId=@SubmitId
end

go 
if exists (select name from sysobjects where name='procDealSecuritySelectByCommandId')
drop proc procDealSecuritySelectByCommandId

go
create proc procDealSecuritySelectByCommandId(
	@CommandId	int
)
as
begin
	select
		RequestId			
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
	from dealsecurity 
	where CommandId=@CommandId
end

go 
if exists (select name from sysobjects where name='procDealSecuritySelectBySubmitId')
drop proc procDealSecuritySelectBySubmitId

go
create proc procDealSecuritySelectBySubmitId(
	@SubmitId	int
)
as
begin
	select
		RequestId			
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
	from dealsecurity 
	where SubmitId=@SubmitId
end


go
if exists (select name from sysobjects where name='procDealSecuritySelectAll')
drop proc procDealSecuritySelectAll

go
create proc procDealSecuritySelectAll
as
begin
	select
		RequestId			
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
	from dealsecurity 
end