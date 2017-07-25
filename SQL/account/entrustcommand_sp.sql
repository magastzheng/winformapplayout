use tradingsystem

--==entrust command/entrust security begin====

--++entrustcommand begin++++
go
if exists (select name from sysobjects where name='procEntrustCommandInsert')
drop proc procEntrustCommandInsert

go
create proc procEntrustCommandInsert(
	@CommandId		int
	,@Copies		int
	,@SubmitPerson	int
	,@CreatedDate	datetime
)
as
begin
	declare @newid int
	insert into entrustcommand(
		CommandId
		,Copies
		,EntrustNo
		,BatchNo
		,EntrustStatus
		,DealStatus
		,SubmitPerson
		,CreatedDate
		,EntrustFailCode
		,EntrustFailCause
	)
	values(
		@CommandId
		,@Copies
		,-1
		,-1
		,0
		,1
		,@SubmitPerson
		,@CreatedDate
		,0
		,NULL
	)

	set @newid = SCOPE_IDENTITY()
	return @newid
end

go
if exists (select name from sysobjects where name='procEntrustCommandUpdateBatchNo')
drop proc procEntrustCommandUpdateBatchNo

go
create proc procEntrustCommandUpdateBatchNo(
	@SubmitId		int
	,@BatchNo		int
	,@EntrustStatus	int
	,@ModifiedDate	datetime
	,@EntrustFailCode	int
	,@EntrustFailCause varchar(128)
)
as
begin
	update entrustcommand
	set BatchNo		= @BatchNo
		,EntrustStatus	= @EntrustStatus
		,ModifiedDate	= @ModifiedDate
		,EntrustFailCode = @EntrustFailCode
		,EntrustFailCause = @EntrustFailCause
	where SubmitId=@SubmitId
end

go
if exists (select name from sysobjects where name='procEntrustCommandUpdateDealStatus')
drop proc procEntrustCommandUpdateDealStatus

go
create proc procEntrustCommandUpdateDealStatus(
	@SubmitId			int
	,@DealStatus		int
	,@ModifiedDate		datetime
)
as
begin
	--declare @newid int
	update entrustcommand
	set DealStatus		= @DealStatus
		,ModifiedDate	= @ModifiedDate
	where SubmitId=@SubmitId
end


go
if exists (select name from sysobjects where name='procEntrustCommandUpdateEntrustStatus')
drop proc procEntrustCommandUpdateEntrustStatus

go
create proc procEntrustCommandUpdateEntrustStatus(
	@SubmitId			int
	,@EntrustStatus		int
	,@ModifiedDate		datetime
)
as
begin
	--declare @newid int
	update entrustcommand
	set EntrustStatus	= @EntrustStatus
		,ModifiedDate	= @ModifiedDate
	where SubmitId=@SubmitId
end

go
if exists (select name from sysobjects where name='procEntrustCommandDeleteByCommandId')
drop proc procEntrustCommandDeleteByCommandId

go
create proc procEntrustCommandDeleteByCommandId(
	@CommandId		int
)
as
begin
	delete from entrustcommand
	where CommandId=@CommandId
end

go
if exists (select name from sysobjects where name='procEntrustCommandDeleteBySubmitId')
drop proc procEntrustCommandDeleteBySubmitId

go
create proc procEntrustCommandDeleteBySubmitId(
	@SubmitId		int
)
as
begin
	delete from entrustcommand
	where SubmitId=@SubmitId
end

go
if exists (select name from sysobjects where name='procEntrustCommandDeleteByCommandIdEntrustStatus')
drop proc procEntrustCommandDeleteByCommandIdEntrustStatus

go
create proc procEntrustCommandDeleteByCommandIdEntrustStatus(
	@CommandId		int
	,@EntrustStatus	int
)
as
begin
	delete from entrustcommand
	where CommandId=@CommandId and EntrustStatus=@EntrustStatus
end

go
if exists (select name from sysobjects where name='procEntrustCommandSelectBySubmitId')
drop proc procEntrustCommandSelectBySubmitId

go
create proc procEntrustCommandSelectBySubmitId(
	@SubmitId		int
)
as
begin
	select SubmitId
		,CommandId
		,Copies
		,EntrustNo
		,BatchNo
		,EntrustStatus
		,DealStatus
		,SubmitPerson
		,CreatedDate
		,ModifiedDate
		,EntrustFailCode
		,EntrustFailCause
	from entrustcommand
	where SubmitId=@SubmitId
end

go
if exists (select name from sysobjects where name='procEntrustCommandSelectByCommandId')
drop proc procEntrustCommandSelectByCommandId

go
create proc procEntrustCommandSelectByCommandId(
	@CommandId int
)
as
begin
	select SubmitId
		,CommandId
		,Copies
		,EntrustNo
		,BatchNo
		,EntrustStatus
		,DealStatus
		,SubmitPerson
		,CreatedDate
		,ModifiedDate
		,EntrustFailCode
		,EntrustFailCause
	from entrustcommand
	where CommandId = @CommandId 
end

go
if exists (select name from sysobjects where name='procEntrustCommandSelectCancel')
drop proc procEntrustCommandSelectCancel

go
create proc procEntrustCommandSelectCancel(
	@CommandId int
)
as
begin
	select SubmitId
		,CommandId
		,Copies
		,EntrustNo
		,BatchNo
		,EntrustStatus
		,DealStatus
		,SubmitPerson
		,CreatedDate
		,ModifiedDate
		,EntrustFailCode
		,EntrustFailCause
	from entrustcommand
	where CommandId = @CommandId 
		and (DealStatus=1 or DealStatus=2)		--未成交或部分成交
		and EntrustStatus=4		--仅对已委托完成的撤单
end
--++entrustcommand end++++

--++entrustsecurity begin++++
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
		,ConfirmNo
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
		,NULL				--初始时没有委托确认号
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
if exists (select name from sysobjects where name='procEntrustSecurityUpdateEntrustNo')
drop proc procEntrustSecurityUpdateEntrustNo

go
create proc procEntrustSecurityUpdateEntrustNo(
	@SubmitId			int
	,@CommandId			int
	,@SecuCode			varchar(10)
	,@EntrustNo			int
	,@BatchNo			int
	,@EntrustStatus		int
	,@ModifiedDate		datetime
)
as
begin
	update entrustsecurity
	set EntrustNo			= @EntrustNo
		,BatchNo			= @BatchNo
		,EntrustStatus		= @EntrustStatus
		,ModifiedDate		= @ModifiedDate
	where SubmitId=@SubmitId
		and CommandId=@CommandId 
		and SecuCode=@SecuCode
end

go
if exists (select name from sysobjects where name='procEntrustSecurityUpdateConfirmNo')
drop proc procEntrustSecurityUpdateConfirmNo

go
create proc procEntrustSecurityUpdateConfirmNo(
	@SubmitId			int
	,@CommandId			int
	,@SecuCode			varchar(10)
	,@ConfirmNo			varchar(32)
	,@ModifiedDate		datetime
)
as
begin
	update entrustsecurity
	set ConfirmNo			= @ConfirmNo
		,ModifiedDate		= @ModifiedDate
	where SubmitId=@SubmitId
		and CommandId=@CommandId 
		and SecuCode=@SecuCode
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
		,ConfirmNo
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
		,ConfirmNo
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
		,ConfirmNo
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
		,ConfirmNo
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
		,ConfirmNo
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
		,ConfirmNo
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
		,ConfirmNo
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
		,a.ConfirmNo
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
if exists (select name from sysobjects where name='procEntrustSecuritySelectCombineByCommandId')
drop proc procEntrustSecuritySelectCombineByCommandId

go
create proc procEntrustSecuritySelectCombineByCommandId(
	@CommandId			int
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
		,a.ConfirmNo
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
	inner join tradecommand c
	on a.CommandId=c.CommandId
	inner join tradeinstance d
	on c.InstanceId=d.InstanceId
	inner join monitorunit e
	on d.MonitorUnitId=e.MonitorUnitId
	inner join ufxportfolio f
	on e.PortfolioId=f.PortfolioId
	where a.CommandId = @CommandId
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
		,a.ConfirmNo
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
		,a.ConfirmNo
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
--++entrustsecurity end++++

--==entrust command/entrust security end====