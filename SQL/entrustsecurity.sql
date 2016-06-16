use tradingsystem


if object_id('entrustsecurity') is not null
drop table entrustsecurity

create table entrustsecurity(
	SubmitId			int not null
	,CommandId			int not null
	,SecuCode			varchar(10) not null
	,SecuType			int
	,EntrustAmount		int
	,EntrustPrice		numeric(20, 4) 
	,EntrustDirection	int			 --10 - 买入股票， 11 - 卖出股票， 12 - 卖出开仓， 13 - 买入平仓
	,EntrustStatus		int			 -- 1 - 未执行， 2 - 部分执行， 3- 已完成
	,PriceType			int			 -- 
	,DealStatus			int			 -- 1 - 未成交， 2 - 部分成交， 3 - 已完成
	,DealAmount			int			 -- 成交数量
	--,BatchId			int			 --委托后返回的批号ID
	,CreatedDate		datetime
	,ModifiedDate		datetime
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
	,@PriceType			int
	,@CreatedDate		datetime
)
as
begin
	insert into entrustsecurity(
		SubmitId
		,CommandId			
		,SecuCode			
		,SecuType			
		,EntrustAmount	
		,EntrustPrice		
		,EntrustDirection	
		,EntrustStatus
		,PriceType
		,DealStatus
		,DealAmount
		,CreatedDate
	)values(
		@SubmitId
		,@CommandId			
		,@SecuCode			
		,@SecuType			
		,@EntrustAmount	
		,@EntrustPrice		
		,@EntrustDirection	
		,@EntrustStatus
		,@PriceType
		,1					--未成交
		,0					--成交量初始为0
		,@CreatedDate	
	)		
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
	,@PriceType			int
	,@ModifiedDate		datetime
)
as
begin
	update entrustsecurity
	set EntrustAmount		= @EntrustAmount
		,EntrustPrice		= @EntrustPrice
		,EntrustDirection	= @EntrustDirection
		,EntrustStatus		= @EntrustStatus
		,PriceType			= @PriceType
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

go
if exists (select name from sysobjects where name='procEntrustSecurityUpdateDeal')
drop proc procEntrustSecurityUpdateDeal

go
create proc procEntrustSecurityUpdateDeal(
	@SubmitId			int
	,@CommandId			int
	,@SecuCode			varchar(10)
	,@DealStatus		int
	,@DealAmount		int
	,@ModifiedDate		datetime
)
as
begin
	--成交量?
	update entrustsecurity
	set DealStatus		= @DealStatus
		,DealAmount		= @DealAmount
		,ModifiedDate	= @ModifiedDate
	where SubmitId=@SubmitId
		and CommandId=@CommandId 
		and SecuCode=@SecuCode
end

go
if exists (select name from sysobjects where name='procEntrustSecurityUpdateCancel')
drop proc procEntrustSecurityUpdateCancel

go
create proc procEntrustSecurityUpdateCancel(
	@CommandId			int
	,@ModifiedDate		datetime
)
as
begin
	--成交量?
	update entrustsecurity
	set EntrustStatus	= 10
		,ModifiedDate	= @ModifiedDate
	where CommandId=@CommandId 
		and DealStatus = 1		--未成交
		and (EntrustStatus = 0	--提交到数据库
		or EntrustStatus = 1	--提交到UFX
		or EntrustStatus = 2	--未执行
		or EntrustStatus = 3	--部分执行
		or EntrustStatus = 4)	--已完成
end

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

go
if exists (select name from sysobjects where name='procEntrustSecuritySelectBySubmitId')
drop proc procEntrustSecuritySelectBySubmitId

go
create proc procEntrustSecuritySelectBySubmitId(
	@SubmitId	int
)
as
begin
	select SubmitId 
		,CommandId			
		,SecuCode			
		,SecuType			
		,EntrustAmount	
		,EntrustPrice		
		,EntrustDirection	
		,EntrustStatus
		,PriceType
		,DealStatus
		,DealAmount
		,CreatedDate
		,ModifiedDate
	from entrustsecurity
	where SubmitId = @SubmitId
end

go
if exists (select name from sysobjects where name='procEntrustSecuritySelectByCommandId')
drop proc procEntrustSecuritySelectByCommandId

go
create proc procEntrustSecuritySelectByCommandId(
	@CommandId			int
)
as
begin
	select SubmitId 
		,CommandId			
		,SecuCode			
		,SecuType			
		,EntrustAmount	
		,EntrustPrice		
		,EntrustDirection	
		,EntrustStatus
		,PriceType
		,DealStatus
		,DealAmount
		,CreatedDate
		,ModifiedDate
	from entrustsecurity
	where CommandId = @CommandId
end

go
if exists (select name from sysobjects where name='procEntrustSecuritySelectAll')
drop proc procEntrustSecuritySelectAll

go
create proc procEntrustSecuritySelectAll
as
begin
	select SubmitId 
		,CommandId			
		,SecuCode			
		,SecuType			
		,EntrustAmount	
		,EntrustPrice		
		,EntrustDirection	
		,EntrustStatus
		,PriceType
		,DealStatus
		,DealAmount
		,CreatedDate
		,ModifiedDate
	from entrustsecurity
end

go
if exists (select name from sysobjects where name='procEntrustSecuritySelectByEntrustStatus')
drop proc procEntrustSecuritySelectByEntrustStatus

go
create proc procEntrustSecuritySelectByEntrustStatus(
	@SubmitId		int
	,@CommandId		int
	,@EntrustStatus	int
)
as
begin
	select SubmitId 
		,CommandId			
		,SecuCode			
		,SecuType			
		,EntrustAmount	
		,EntrustPrice		
		,EntrustDirection	
		,EntrustStatus
		,PriceType
		,DealStatus
		,DealAmount
		,CreatedDate
		,ModifiedDate
	from entrustsecurity
	where SubmitId=@SubmitId 
		and CommandId=@CommandId
		and EntrustStatus=@EntrustStatus
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
	select SubmitId 
		,CommandId			
		,SecuCode			
		,SecuType			
		,EntrustAmount	
		,EntrustPrice		
		,EntrustDirection	
		,EntrustStatus
		,PriceType
		,DealStatus
		,DealAmount
		,CreatedDate
		,ModifiedDate
	from entrustsecurity
	where DealStatus = 1		--未成交
		and EntrustStatus != 10	--撤单
		and EntrustStatus != 11 --撤单到UFX
		and EntrustStatus != 12 --撤单成功
end

if exists (select name from sysobjects where name='procEntrustSecuritySelectCancelRedo')
drop proc procEntrustSecuritySelectCancelRedo

go
create proc procEntrustSecuritySelectCancelRedo(
	@CommandId int
)
as
begin
	select SubmitId 
		,CommandId			
		,SecuCode			
		,SecuType			
		,EntrustAmount	
		,EntrustPrice		
		,EntrustDirection	
		,EntrustStatus
		,PriceType
		,DealStatus
		,DealAmount
		,CreatedDate
		,ModifiedDate
	from entrustsecurity
	where (DealStatus = 1		--未成交
		or DealStatus = 2)		--部分成交
		and EntrustStatus = 4	--已完成委托
end