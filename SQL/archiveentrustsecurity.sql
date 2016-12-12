use tradingsystem


if object_id('archiveentrustsecurity') is not null
drop table archiveentrustsecurity

create table archiveentrustsecurity(
	ArchiveId			int identity(1, 1) primary key
	,RequestId			int not null					--请求ID
	,SubmitId			int not null					--指令提交ID
	,CommandId			int not null					--指令ID
	,SecuCode			varchar(10) not null			--证券交易所代码
	,SecuType			int								--证券类型
	,EntrustAmount		int								--委托数量
	,EntrustPrice		numeric(20, 4)					--委托价格
	,EntrustDirection	int			 --委托方向：10 - 买入股票， 11 - 卖出股票， 12 - 卖出开仓， 13 - 买入平仓
	,EntrustStatus		int			 --委托状态： 0 - 提交到DB, 1 - 提交到UFX， 2 - 未执行， 3 - 部分执行， 4 - 已完成， 10 - 撤单DB, 11 - 撤单UFX, 12 - 撤单成功， 13 - 撤单失败
	,EntrustPriceType	int			 --委托价格类型： 0 - 限价，'a'五档即成剩撤(上交所市价)， 'A'五档即成剩撤(深交所市价)
	,PriceType			int			 --价格类型：委卖一， 委买一 ....
	,EntrustNo			int			 --委托之后，服务器返回的委托号
	,BatchNo			int			 --委托后返回的批号ID
	,DealStatus			int			 --成交状态：1 - 未成交， 2 - 部分成交， 3 - 已完成
	,TotalDealAmount	int			 --累计成交数量
	,TotalDealBalance	numeric(20, 4) --累计成交金额
	,TotalDealFee		numeric(20, 4) --累计费用
	,DealTimes			int			 -- 成交次数
	,ArchiveDate		datetime	 -- 归档时间
	,EntrustDate		datetime	 -- 委托时间
	,CreatedDate		datetime	 -- 委托时间	
	,ModifiedDate		datetime	 -- 修改时间
	,EntrustFailCode	int			 --委托失败代码
	,EntrustFailCause	varchar(128) --委托失败原因
)

--====================================
--archiveentrustsecurity table
--====================================
go
if exists (select name from sysobjects where name='procArchiveEntrustSecurityInsert')
drop proc procArchiveEntrustSecurityInsert

go
create proc procArchiveEntrustSecurityInsert(
	@RequestId			int
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
	,@ArchiveDate		datetime
	,@EntrustDate		datetime
	,@CreatedDate		datetime
	,@ModifiedDate		datetime
	,@EntrustFailCode	int			
	,@EntrustFailCause	varchar(128)
)
as
begin
	declare @newid int

	insert into archiveentrustsecurity(
		RequestId		
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
		,ArchiveDate	
		,EntrustDate	
		,CreatedDate	
		,ModifiedDate	
		,EntrustFailCode
		,EntrustFailCause
	)values(
		@RequestId		
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
		,@ArchiveDate	
		,@EntrustDate	
		,@CreatedDate	
		,@ModifiedDate	
		,@EntrustFailCode
		,@EntrustFailCause
	)	
	
	set @newid = SCOPE_IDENTITY()
	return @newid	
end

go
if exists (select name from sysobjects where name='procArchiveEntrustSecuritySelect')
drop proc procArchiveEntrustSecuritySelect

go
create proc procArchiveEntrustSecuritySelect(
	@SubmitId			int
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
		,ArchiveDate	
		,EntrustDate	
		,CreatedDate	
		,ModifiedDate	
		,EntrustFailCode
		,EntrustFailCause
	from archiveentrustsecurity
	where SubmitId=@SubmitId
end


