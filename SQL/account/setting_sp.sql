use tradingsystem

--==setting begin====

--++usersetting begin++++
go
if exists (select name from sysobjects where name='procUserSettingInsertOrUpdate')
drop proc procUserSettingInsertOrUpdate

go
create proc procUserSettingInsertOrUpdate(
	@UserId						int
	,@ConnectTimeout			int
	,@UFXTimeout				int
	,@UFXLimitEntrustRatio		int
	,@UFXFutuLimitEntrustRatio	int
	,@UFXOptLimitEntrustRatio	int
	,@BuyFutuPrice				int
	,@SellFutuPrice				int
	,@BuySpotPrice				int
	,@SellSpotPrice				int
	,@BuySellEntrustOrder		int
	,@OddShareMode				int
	,@SZSEEntrustPriceType		int
	,@SSEEntrustPriceType		int
)
as
begin
	declare @Count int
	set @Count = (select count(UserId) from usersetting where UserId=@UserId)

	if @Count = 0 or @Count is null
	begin
		insert into usersetting(
			UserId					
			,ConnectTimeout			
			,UFXTimeout				
			,UFXLimitEntrustRatio	
			,UFXFutuLimitEntrustRatio
			,UFXOptLimitEntrustRatio	
			,BuyFutuPrice			
			,SellFutuPrice			
			,BuySpotPrice			
			,SellSpotPrice			
			,BuySellEntrustOrder		
			,OddShareMode			
			,SZSEEntrustPriceType	
			,SSEEntrustPriceType
			,CreatedDate		
		)
		values(
			@UserId						
			,@ConnectTimeout				
			,@UFXTimeout					
			,@UFXLimitEntrustRatio		
			,@UFXFutuLimitEntrustRatio	
			,@UFXOptLimitEntrustRatio	
			,@BuyFutuPrice				
			,@SellFutuPrice				
			,@BuySpotPrice				
			,@SellSpotPrice				
			,@BuySellEntrustOrder		
			,@OddShareMode				
			,@SZSEEntrustPriceType		
			,@SSEEntrustPriceType	
			,getdate()
		)
	end
	else
	begin
		update usersetting
		set
			ConnectTimeout				= @ConnectTimeout
			,UFXTimeout					= @UFXTimeout
			,UFXLimitEntrustRatio		= @UFXLimitEntrustRatio 
			,UFXFutuLimitEntrustRatio	= @UFXFutuLimitEntrustRatio
			,UFXOptLimitEntrustRatio	= @UFXOptLimitEntrustRatio
			,BuyFutuPrice				= @BuyFutuPrice
			,SellFutuPrice				= @SellFutuPrice
			,BuySpotPrice				= @BuySpotPrice
			,SellSpotPrice				= @SellSpotPrice
			,BuySellEntrustOrder		= @BuySellEntrustOrder
			,OddShareMode				= @OddShareMode
			,SZSEEntrustPriceType		= @SZSEEntrustPriceType
			,SSEEntrustPriceType		= @SSEEntrustPriceType
			,ModifiedDate				= getdate()
		where UserId=@UserId
	end
end

go
if exists (select name from sysobjects where name='procUserSettingSelect')
drop proc procUserSettingSelect

go
create proc procUserSettingSelect(
	@UserId	int
)
as
begin
	select
		UserId					
		,ConnectTimeout			
		,UFXTimeout				
		,UFXLimitEntrustRatio	
		,UFXFutuLimitEntrustRatio
		,UFXOptLimitEntrustRatio	
		,BuyFutuPrice			
		,SellFutuPrice			
		,BuySpotPrice			
		,SellSpotPrice			
		,BuySellEntrustOrder		
		,OddShareMode			
		,SZSEEntrustPriceType	
		,SSEEntrustPriceType
		,CreatedDate
		,ModifiedDate
	from usersetting
	where UserId=@UserId
end

go
if exists (select name from sysobjects where name='procUserSettingDelete')
drop proc procUserSettingDelete

go
create proc procUserSettingDelete(
	@UserId	int
)
as
begin
	delete from usersetting
	where UserId=@UserId
end
--++usersetting end++++

--==setting end====