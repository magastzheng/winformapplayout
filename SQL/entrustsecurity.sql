use tradingsystem


if object_id('entrustsecurity') is not null
drop table entrustsecurity

create table entrustsecurity(
	RequestId			int identity(1, 1) primary key	--请求ID
	,SubmitId			int not null					--指令提交ID
	,CommandId			int not null					--指令ID
	,SecuCode			varchar(10) not null			--证券交易所代码
	,SecuType			int								--证券类型
	,EntrustAmount		int								--委托数量
	,EntrustPrice		numeric(20, 4)					--委托价格
	,EntrustDirection	int			 --委托方向：10 - 买入股票， 11 - 卖出股票， 12 - 卖出开仓， 13 - 买入平仓
	,EntrustStatus		int			 --委托状态： 0 - 提交到DB, 1 - 提交到UFX， 2 - 未执行， 3 - 部分执行， 4 - 已完成， 10 - 撤单DB, 11 - 撤单UFX, 12 - 撤单成功，(-4) - 委托失败， (-12) - 撤单失败
	,EntrustPriceType	int			 --委托价格类型： 0 - 限价，'a'五档即成剩撤(上交所市价)， 'A'五档即成剩撤(深交所市价)
	,PriceType			int			 --价格类型：委卖一， 委买一 ....
	,EntrustNo			int			 --委托之后，服务器返回的委托号
	,BatchNo			int			 --委托后返回的批号ID
	,DealStatus			int			 --成交状态：1 - 未成交， 2 - 部分成交， 3 - 已完成
	,TotalDealAmount	int			 --累计成交数量
	,TotalDealBalance	numeric(20, 4) --累计成交金额
	,TotalDealFee		numeric(20, 4) --累计费用
	,DealTimes			int			 -- 成交次数
	,EntrustDate		datetime	 -- 委托时间
	,CreatedDate		datetime	 -- 委托时间	
	,ModifiedDate		datetime	 -- 修改时间
	,EntrustFailCode	int			 --委托失败代码
	,EntrustFailCause	varchar(1024) --委托失败原因
)

--====================================
--entrustsecurity table
--====================================
go
if exists (select name from sysobjects where name='procEntrustSecurityInsert')
drop proc procEntrustSecurityInsert

go
create proc procEntrustSecurityInsert(
	@SubmitId			int
	,@CommandId			int
	,@SecuCode			varchar(10)
	,@SecuType			int
	,@EntrustAmount		int
	,@EntrustPrice		numeric(20, 4)
	,@EntrustDirection	int
	,@EntrustStatus		int
	,@EntrustPriceType	int
	,@PriceType			int
	,@EntrustDate		datetime
	,@CreatedDate		datetime
)
as
begin
	declare @newid int

	insert into entrustsecurity(
		SubmitId
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
		,EntrustFailCode
		,EntrustFailCause
	)values(
		@SubmitId
		,@CommandId			
		,@SecuCode			
		,@SecuType			
		,@EntrustAmount	
		,@EntrustPrice		
		,@EntrustDirection	
		,@EntrustStatus
		,@EntrustPriceType
		,@PriceType
		,-1					--初始时没有委托序号，只有通过UFX委托完成之后，才会产生
		,-1					--初始时没有委托批次号
		,1					--未成交
		,0					--成交量初始为0
		,0.0				--累计成交金额初始为0
		,0.0				--累计成交费用初始为0
		,0					--成交次数0
		,@EntrustDate
		,@CreatedDate
		,0
		,NULL
	)	
	
	set @newid = SCOPE_IDENTITY()
	return @newid	
end

go
if exists (select name from sysobjects where name='procEntrustSecurityUpdate')
drop proc procEntrustSecurityUpdate

go
create proc procEntrustSecurityUpdate(
	@SubmitId			int
	,@CommandId			int
	,@SecuCode			varchar(10)
	,@EntrustAmount		int
	,@EntrustPrice		numeric(20, 4)
	,@EntrustDirection	int
	,@EntrustStatus		int
	,@EntrustPriceType	int
	,@PriceType			int
	,@EntrustNo			int
	,@BatchNo			int
	,@EntrustDate		datetime
	,@ModifiedDate		datetime
)
as
begin
	update entrustsecurity
	set EntrustAmount		= @EntrustAmount
		,EntrustPrice		= @EntrustPrice
		,EntrustDirection	= @EntrustDirection
		,EntrustStatus		= @EntrustStatus
		,EntrustPriceType	= @EntrustPriceType
		,PriceType			= @PriceType
		,EntrustNo			= @EntrustNo
		,BatchNo			= @BatchNo
		,EntrustDate		= @EntrustDate
		,ModifiedDate		= @ModifiedDate
	where SubmitId=@SubmitId
		and CommandId=@CommandId 
		and SecuCode=@SecuCode
end

--go
--if exists (select name from sysobjects where name='procEntrustSecurityUpdateEntrustResponse')
--drop proc procEntrustSecurityUpdateEntrustResponse

--go
--create proc procEntrustSecurityUpdateEntrustResponse(
--	@SubmitId			int
--	,@CommandId			int
--	,@SecuCode			varchar(10)
--	,@EntrustNo			int
--	,@BatchNo			int
--	,@ModifiedDate		datetime
--	,@EntrustFailCode	int
--	,@EntrustFailCause	varchar(128)
--)
--as
--begin
--	--没有错误表示成功
--	if @EntrustFailCode = 0
--	begin
--		update entrustsecurity
--		set EntrustNo			= @EntrustNo
--			,BatchNo			= @BatchNo
--			,EntrustStatus		= 4				--委托成功
--			,ModifiedDate		= @ModifiedDate
--			,EntrustFailCode	= @EntrustFailCode
--			,EntrustFailCause	= @EntrustFailCause
--		where SubmitId=@SubmitId
--			and CommandId=@CommandId 
--			and SecuCode=@SecuCode
--	end
--	else
--	begin
--		update entrustsecurity
--		set EntrustNo			= @EntrustNo
--			,BatchNo			= @BatchNo
--			,EntrustStatus		= -4				--委托失败
--			,ModifiedDate		= @ModifiedDate
--			,EntrustFailCode	= @EntrustFailCode
--			,EntrustFailCause	= @EntrustFailCause
--		where SubmitId=@SubmitId
--			and CommandId=@CommandId 
--			and SecuCode=@SecuCode
--	end
--end

go
if exists (select name from sysobjects where name='procEntrustSecurityUpdateResponseByRequestId')
drop proc procEntrustSecurityUpdateResponseByRequestId

go
create proc procEntrustSecurityUpdateResponseByRequestId(
	@RequestId			int
	,@EntrustNo			int
	,@BatchNo			int
	,@ModifiedDate		datetime
	,@EntrustFailCode	int
	,@EntrustFailCause	varchar(128)
)
as
begin
	--没有错误表示成功
	if @EntrustFailCode = 0
	begin
		update entrustsecurity
		set EntrustNo			= @EntrustNo
			,BatchNo			= @BatchNo
			,EntrustStatus		= 4				--委托成功
			,ModifiedDate		= @ModifiedDate
			,EntrustFailCode	= @EntrustFailCode
			,EntrustFailCause	= @EntrustFailCause
		where RequestId=@RequestId
	end
	else
	begin
		update entrustsecurity
		set EntrustNo			= @EntrustNo
			,BatchNo			= @BatchNo
			,EntrustStatus		= -4				--委托失败
			,ModifiedDate		= @ModifiedDate
			,EntrustFailCode	= @EntrustFailCode
			,EntrustFailCause	= @EntrustFailCause
		where RequestId=@RequestId
	end
end

go
if exists (select name from sysobjects where name='procEntrustSecurityUpdateEntrustStatus')
drop proc procEntrustSecurityUpdateEntrustStatus

go
create proc procEntrustSecurityUpdateEntrustStatus(
	@SubmitId			int
	,@CommandId			int
	,@SecuCode			varchar(10)
	,@EntrustStatus		int
	,@ModifiedDate		datetime
)
as
begin
	update entrustsecurity
	set EntrustStatus		= @EntrustStatus
		,ModifiedDate		= @ModifiedDate
	where SubmitId=@SubmitId
		and CommandId=@CommandId 
		and SecuCode=@SecuCode
		and DealStatus != 3			--已完成成交的不可以撤单
end

go
if exists (select name from sysobjects where name='procEntrustSecurityUpdateEntrustStatusByEntrustNo')
drop proc procEntrustSecurityUpdateEntrustStatusByEntrustNo

go
create proc procEntrustSecurityUpdateEntrustStatusByEntrustNo(
	@EntrustNo			int
	,@EntrustStatus		int
	,@ModifiedDate		datetime
)
as
begin
	update entrustsecurity
	set EntrustStatus		= @EntrustStatus
		,ModifiedDate		= @ModifiedDate
	where EntrustNo=@EntrustNo
		and DealStatus != 3			--已完成成交的不可以改变委托状态
end

go
if exists (select name from sysobjects where name='procEntrustSecurityUpdateEntrustStatusBySubmitId')
drop proc procEntrustSecurityUpdateEntrustStatusBySubmitId

go
create proc procEntrustSecurityUpdateEntrustStatusBySubmitId(
	@SubmitId			int
	,@EntrustStatus		int
	,@ModifiedDate		datetime
)
as
begin
	update entrustsecurity
	set EntrustStatus		= @EntrustStatus
		,ModifiedDate		= @ModifiedDate
	where SubmitId=@SubmitId
end

--go
--if exists (select name from sysobjects where name='procEntrustSecurityUpdateEntrustStatusByRequestId')
--drop proc procEntrustSecurityUpdateEntrustStatusByRequestId

--go
--create proc procEntrustSecurityUpdateEntrustStatusByRequestId(
--	@RequestId		int
--	,@EntrustStatus		int
--	,@ModifiedDate		datetime
--)
--as
--begin
--	update entrustsecurity
--	set EntrustStatus		= @EntrustStatus
--		,ModifiedDate		= @ModifiedDate
--	where RequestId=@RequestId
--end

go
if exists (select name from sysobjects where name='procEntrustSecurityUpdateDeal')
drop proc procEntrustSecurityUpdateDeal

go
create proc procEntrustSecurityUpdateDeal(
	@SubmitId			int
	,@CommandId			int
	,@SecuCode			varchar(10)
	,@DealAmount		int
	,@DealBalance		numeric(20, 4)
	,@DealFee			numeric(20, 4)
	,@ModifiedDate		datetime
)
as
begin
	--成交量?
	declare @TotalTimes int
	declare @TotalAmount int
	declare @EntrustAmount int
	declare @TotalBalance numeric(20, 4)
	declare @TotalFee numeric(20, 4)
	declare @DealStatus int
	
	select @TotalTimes=DealTimes
		,@TotalAmount=TotalDealAmount 
		,@TotalBalance=TotalDealBalance
		,@TotalFee=TotalDealFee
		,@EntrustAmount=EntrustAmount
	from entrustsecurity 
	where SubmitId=@SubmitId
		and CommandId=@CommandId 
		and SecuCode=@SecuCode

	set @TotalTimes=@TotalTimes+1
	set @TotalAmount=@TotalAmount+@DealAmount
	set @TotalBalance=@TotalBalance+@DealBalance
	set @TotalFee=@TotalFee+@DealFee

	if @TotalAmount=@EntrustAmount
	begin
		set @DealStatus=3 --已完成
	end
	else
	begin
		set @DealStatus=2 --部分完成
	end

	update entrustsecurity
	set DealStatus			= @DealStatus
		,TotalDealAmount	= @TotalAmount
		,TotalDealBalance	= @TotalBalance
		,TotalDealFee		= @TotalFee
		,DealTimes			= @TotalTimes
		,ModifiedDate		= @ModifiedDate
	where SubmitId=@SubmitId
		and CommandId=@CommandId 
		and SecuCode=@SecuCode
end

--go
--if exists (select name from sysobjects where name='procEntrustSecurityUpdateDealByRequestId')
--drop proc procEntrustSecurityUpdateDealByRequestId

--go
--create proc procEntrustSecurityUpdateDealByRequestId(
--	@RequestId			int
--	,@DealAmount		int
--	,@DealBalance		numeric(20, 4)
--	,@DealFee			numeric(20, 4)
--	,@ModifiedDate		datetime
--)
--as
--begin
--	--成交量?
--	declare @TotalTimes int
--	declare @TotalAmount int
--	declare @EntrustAmount int
--	declare @TotalBalance numeric(20, 4)
--	declare @TotalFee numeric(20, 4)
--	declare @DealStatus int

--	select @TotalTimes=DealTimes
--		,@TotalAmount=TotalDealAmount 
--		,@TotalBalance=TotalDealBalance
--		,@TotalFee=TotalDealFee
--		,@EntrustAmount=EntrustAmount
--	from entrustsecurity 
--	where RequestId=@RequestId

--	set @TotalTimes=@TotalTimes+1
--	set @TotalAmount=@TotalAmount+@DealAmount
--	set @TotalBalance=@TotalBalance+@DealBalance
--	set @TotalFee=@TotalFee+@DealFee

--	if @TotalAmount=@EntrustAmount
--	begin
--		set @DealStatus=3 --已完成
--	end
--	else
--	begin
--		set @DealStatus=2 --部分完成
--	end

--	update entrustsecurity
--	set DealStatus			= @DealStatus
--		,TotalDealAmount	= @TotalAmount
--		,TotalDealBalance	= @TotalBalance
--		,TotalDealFee		= @TotalFee
--		,DealTimes			= @TotalTimes
--		,ModifiedDate		= @ModifiedDate
--	where RequestId=@RequestId
--end

--go
--if exists (select name from sysobjects where name='procEntrustSecurityUpdateCancel')
--drop proc procEntrustSecurityUpdateCancel

--go
--create proc procEntrustSecurityUpdateCancel(
--	@CommandId			int
--	,@ModifiedDate		datetime
--)
--as
--begin
--	--成交量?
--	update entrustsecurity
--	set EntrustStatus	= 10
--		,ModifiedDate	= @ModifiedDate
--	where CommandId=@CommandId 
--		and (DealStatus = 1 or DealStatus = 2)		--未成交或部分成交
--		and EntrustStatus = 4	--已完成
--end

go
if exists (select name from sysobjects where name='procEntrustSecurityDelete')
drop proc procEntrustSecurityDelete

go
create proc procEntrustSecurityDelete(
	@SubmitId			int
	,@CommandId			int
	,@SecuCode			varchar(10)
)
as
begin
	delete from entrustsecurity 
	where SubmitId=@SubmitId 
		and CommandId=@CommandId 
		and SecuCode=@SecuCode
end

go
if exists (select name from sysobjects where name='procEntrustSecurityDeleteBySubmitId')
drop proc procEntrustSecurityDeleteBySubmitId

go
create proc procEntrustSecurityDeleteBySubmitId(
	@SubmitId			int
)
as
begin
	delete from entrustsecurity 
	where SubmitId=@SubmitId 
end

go
if exists (select name from sysobjects where name='procEntrustSecurityDeleteByCommandId')
drop proc procEntrustSecurityDeleteByCommandId

go
create proc procEntrustSecurityDeleteByCommandId(
	@CommandId			int
)
as
begin
	delete from entrustsecurity 
	where CommandId=@CommandId 
end

go
if exists (select name from sysobjects where name='procEntrustSecurityDeleteByCommandIdEntrustStatus')
drop proc procEntrustSecurityDeleteByCommandIdEntrustStatus

go
create proc procEntrustSecurityDeleteByCommandIdEntrustStatus(
	@CommandId			int
	,@EntrustStatus		int
)
as
begin
	delete from entrustsecurity 
	where CommandId=@CommandId 
		and EntrustStatus=@EntrustStatus
end

--go
--if exists (select name from sysobjects where name='procEntrustSecuritySelectBySubmitId')
--drop proc procEntrustSecuritySelectBySubmitId

--go
--create proc procEntrustSecuritySelectBySubmitId(
--	@SubmitId	int
--)
--as
--begin
--	select RequestId 
--		,SubmitId 
--		,CommandId			
--		,SecuCode			
--		,SecuType			
--		,EntrustAmount	
--		,EntrustPrice		
--		,EntrustDirection	
--		,EntrustStatus
--		,EntrustPriceType
--		,PriceType
--		,EntrustNo
--		,BatchNo
--		,DealStatus
--		,TotalDealAmount
--		,TotalDealBalance
--		,TotalDealFee
--		,DealTimes
--		,EntrustDate
--		,CreatedDate
--		,ModifiedDate
--		,EntrustFailCode
--		,EntrustFailCause
--	from entrustsecurity
--	where SubmitId = @SubmitId
--end

go
if exists (select name from sysobjects where name='procEntrustSecuritySelectByCommandId')
drop proc procEntrustSecuritySelectByCommandId

go
create proc procEntrustSecuritySelectByCommandId(
	@CommandId			int
)
as
begin
	select RequestId
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
	from entrustsecurity
	where CommandId = @CommandId
end

--go
--if exists (select name from sysobjects where name='procEntrustSecuritySelectAll')
--drop proc procEntrustSecuritySelectAll

--go
--create proc procEntrustSecuritySelectAll
--as
--begin
--	select RequestId
--		,SubmitId 
--		,CommandId			
--		,SecuCode			
--		,SecuType			
--		,EntrustAmount	
--		,EntrustPrice		
--		,EntrustDirection	
--		,EntrustStatus
--		,EntrustPriceType
--		,PriceType
--		,EntrustNo
--		,BatchNo
--		,DealStatus
--		,TotalDealAmount
--		,TotalDealBalance
--		,TotalDealFee
--		,DealTimes
--		,EntrustDate
--		,CreatedDate
--		,ModifiedDate
--		,EntrustFailCode
--		,EntrustFailCause
--	from entrustsecurity
--end

--go
--if exists (select name from sysobjects where name='procEntrustSecuritySelectByEntrustStatus')
--drop proc procEntrustSecuritySelectByEntrustStatus

--go
--create proc procEntrustSecuritySelectByEntrustStatus(
--	@SubmitId		int
--	,@CommandId		int
--	,@EntrustStatus	int
--)
--as
--begin
--	select RequestId
--		,SubmitId 
--		,CommandId			
--		,SecuCode			
--		,SecuType			
--		,EntrustAmount	
--		,EntrustPrice		
--		,EntrustDirection	
--		,EntrustStatus
--		,EntrustPriceType
--		,PriceType
--		,EntrustNo
--		,BatchNo
--		,DealStatus
--		,TotalDealAmount
--		,TotalDealBalance
--		,TotalDealFee
--		,DealTimes
--		,EntrustDate
--		,CreatedDate
--		,ModifiedDate
--		,EntrustFailCode
--		,EntrustFailCause
--	from entrustsecurity
--	where SubmitId=@SubmitId 
--		and CommandId=@CommandId
--		and EntrustStatus=@EntrustStatus
--end

go
if exists (select name from sysobjects where name='procEntrustSecuritySelectAllByEntrustStatus')
drop proc procEntrustSecuritySelectAllByEntrustStatus

go
create proc procEntrustSecuritySelectAllByEntrustStatus(
	@EntrustStatus	int
)
as
begin
	select RequestId
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
	from entrustsecurity
	where EntrustStatus=@EntrustStatus
end

go
if exists (select name from sysobjects where name='procEntrustSecuritySelectCancel')
drop proc procEntrustSecuritySelectCancel

go
create proc procEntrustSecuritySelectCancel(
	@CommandId int
)
as
begin
	--获取委托后可以撤单的证券
	select RequestId
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
	from entrustsecurity
	where CommandId = @CommandId 
		and (DealStatus = 1 or DealStatus = 2)		--未成交或部分成交
		and EntrustStatus = 4 --已完成委托
end

go
if exists (select name from sysobjects where name='procEntrustSecuritySelectCancelBySubmitId')
drop proc procEntrustSecuritySelectCancelBySubmitId

go
create proc procEntrustSecuritySelectCancelBySubmitId(
	@SubmitId int
)
as
begin
	--获取委托后可以撤单的证券
	select RequestId
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
	from entrustsecurity
	where SubmitId = @SubmitId
		and (DealStatus = 1 or DealStatus = 2)		--未成交或部分成交
		and (EntrustStatus = 4		--已完成委托
			or EntrustStatus = 10	--撤单到DB
			or EntrustStatus = 11	--撤单到UFX
			or EntrustStatus = -12)	--撤单失败  
end

--go

--if exists (select name from sysobjects where name='procEntrustSecuritySelectCancelCompletedRedo')
--drop proc procEntrustSecuritySelectCancelCompletedRedo

--go
--create proc procEntrustSecuritySelectCancelCompletedRedo(
--	@CommandId int
--)
--as
--begin
--	--获取撤单成功并可以重新委托的证券
--	select RequestId
--		,SubmitId 
--		,CommandId			
--		,SecuCode			
--		,SecuType			
--		,EntrustAmount	
--		,EntrustPrice		
--		,EntrustDirection	
--		,EntrustStatus
--		,EntrustPriceType
--		,PriceType
--		,EntrustNo
--		,BatchNo
--		,DealStatus
--		,TotalDealAmount
--		,TotalDealBalance
--		,TotalDealFee
--		,DealTimes
--		,EntrustDate
--		,CreatedDate
--		,ModifiedDate
--		,EntrustFailCode
--		,EntrustFailCause
--	from entrustsecurity
--	where CommandId = @CommandId 
--		and (DealStatus = 1		--未成交
--		or DealStatus = 2)		--部分成交
--		and EntrustStatus = 12	--已完成委托
--end

go

if exists (select name from sysobjects where name='procEntrustSecuritySelectCancelCompletedRedoBySubmitId')
drop proc procEntrustSecuritySelectCancelCompletedRedoBySubmitId

go
create proc procEntrustSecuritySelectCancelCompletedRedoBySubmitId(
	@SubmitId int
)
as
begin
	--获取撤单成功并可以重新委托的证券
	select RequestId
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
	from entrustsecurity
	where SubmitId = @SubmitId 
		and (DealStatus = 1		--未成交
		or DealStatus = 2)		--部分成交
		and EntrustStatus = 12	--已完成委托
end

go

if exists (select name from sysobjects where name='procEntrustSecuritySelectEntrustFlow')
drop proc procEntrustSecuritySelectEntrustFlow

go
create proc procEntrustSecuritySelectEntrustFlow
as
begin
	select RequestId
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
	from entrustsecurity
	where (DealStatus = 1		--未成交
		or DealStatus = 2)		--部分成交
		and (EntrustStatus = 4	--已完成委托
		or EntrustStatus = 10
		or EntrustStatus = 11 
		or EntrustStatus = 12
		or EntrustStatus = 13)
end

go

if exists (select name from sysobjects where name='procEntrustSecuritySelectDealFlow')
drop proc procEntrustSecuritySelectDealFlow

go
create proc procEntrustSecuritySelectDealFlow
as
begin
	select RequestId
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
	from entrustsecurity
	where DealStatus = 3
end

--===================combine version
go
if exists (select name from sysobjects where name='procEntrustSecuritySelectAllCombine')
drop proc procEntrustSecuritySelectAllCombine

go
create proc procEntrustSecuritySelectAllCombine
as
begin
	select a.RequestId
		,a.SubmitId 
		,a.CommandId			
		,a.SecuCode			
		,a.SecuType			
		,a.EntrustAmount	
		,a.EntrustPrice		
		,a.EntrustDirection	
		,a.EntrustStatus
		,a.EntrustPriceType
		,a.PriceType
		,a.EntrustNo
		,a.BatchNo
		,a.DealStatus
		,a.TotalDealAmount
		,a.TotalDealBalance
		,a.TotalDealFee
		,a.DealTimes
		,a.EntrustDate
		,a.CreatedDate
		,a.ModifiedDate
		,a.EntrustFailCode
		,a.EntrustFailCause
		,c.InstanceId
		,d.InstanceCode
		,d.MonitorUnitId
		,e.PortfolioId
		,f.PortfolioCode
		,f.PortfolioName
		,f.AccountCode
		,f.AccountName
	from entrustsecurity a
	--inner join entrustcommand b
	--on a.CommandId=b.CommandId and a.SubmitId=b.SubmitId
	inner join tradecommand c
	on a.CommandId=c.CommandId
	inner join tradeinstance d
	on c.InstanceId=d.InstanceId
	inner join monitorunit e
	on d.MonitorUnitId=e.MonitorUnitId
	inner join ufxportfolio f
	on e.PortfolioId=f.PortfolioId
end

go
if exists (select name from sysobjects where name='procEntrustSecuritySelectCombineByRequestId')
drop proc procEntrustSecuritySelectCombineByRequestId

go
create proc procEntrustSecuritySelectCombineByRequestId(
	@RequestId int
)
as
begin
	select a.RequestId
		,a.SubmitId 
		,a.CommandId			
		,a.SecuCode			
		,a.SecuType			
		,a.EntrustAmount	
		,a.EntrustPrice		
		,a.EntrustDirection	
		,a.EntrustStatus
		,a.EntrustPriceType
		,a.PriceType
		,a.EntrustNo
		,a.BatchNo
		,a.DealStatus
		,a.TotalDealAmount
		,a.TotalDealBalance
		,a.TotalDealFee
		,a.DealTimes
		,a.EntrustDate
		,a.CreatedDate
		,a.ModifiedDate
		,a.EntrustFailCode
		,a.EntrustFailCause
		,c.InstanceId
		,d.InstanceCode
		,d.MonitorUnitId
		,e.PortfolioId
		,f.PortfolioCode
		,f.PortfolioName
		,f.AccountCode
		,f.AccountName
	from entrustsecurity a
	--inner join entrustcommand b
	--on a.CommandId=b.CommandId and a.SubmitId=b.SubmitId
	inner join tradecommand c
	on a.CommandId=c.CommandId
	inner join tradeinstance d
	on c.InstanceId=d.InstanceId
	inner join monitorunit e
	on d.MonitorUnitId=e.MonitorUnitId
	inner join ufxportfolio f
	on e.PortfolioId=f.PortfolioId
	where a.RequestId=@RequestId
end

go
if exists (select name from sysobjects where name='procEntrustSecuritySelectCombineBySubmitId')
drop proc procEntrustSecuritySelectCombineBySubmitId

go
create proc procEntrustSecuritySelectCombineBySubmitId(
	@SubmitId int
)
as
begin
	--通过提交号获取同一批提交的证券
	select a.RequestId
		,a.SubmitId 
		,a.CommandId			
		,a.SecuCode			
		,a.SecuType			
		,a.EntrustAmount	
		,a.EntrustPrice		
		,a.EntrustDirection	
		,a.EntrustStatus
		,a.EntrustPriceType
		,a.PriceType
		,a.EntrustNo
		,a.BatchNo
		,a.DealStatus
		,a.TotalDealAmount
		,a.TotalDealBalance
		,a.TotalDealFee
		,a.DealTimes
		,a.EntrustDate
		,a.CreatedDate
		,a.ModifiedDate
		,a.EntrustFailCode
		,a.EntrustFailCause
		,c.InstanceId
		,d.InstanceCode
		,d.MonitorUnitId
		,e.PortfolioId
		,f.PortfolioCode
		,f.PortfolioName
		,f.AccountCode
		,f.AccountName
	from entrustsecurity a
	--inner join entrustcommand b
	--on a.CommandId=b.CommandId and a.SubmitId=b.SubmitId
	inner join tradecommand c
	on a.CommandId=c.CommandId
	inner join tradeinstance d
	on c.InstanceId=d.InstanceId
	inner join monitorunit e
	on d.MonitorUnitId=e.MonitorUnitId
	inner join ufxportfolio f
	on e.PortfolioId=f.PortfolioId
	where a.SubmitId = @SubmitId
end


