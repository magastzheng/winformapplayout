use tradingsystem

--====================================
--archiveentrustsecurity table
--====================================
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


