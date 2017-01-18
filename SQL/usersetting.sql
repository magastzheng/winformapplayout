use tradingsystem

if object_id('usersetting') is not null
drop table usersetting

create table usersetting(
	UserId						int not null primary key	--用户ID
	,ConnectTimeout				int	--连接超时时间
	,UFXTimeout					int	--UFX接口超时时间
	,UFXLimitEntrustRatio		int	--现货最小委托比例
	,UFXFutuLimitEntrustRatio	int	--期货最小委托比例
	,UFXOptLimitEntrustRatio	int	--期权最小委托比例
	,BuyFutuPrice				int	-- 0 市价，1 - 指定价，2 - 最新价， 3 - 自动盘口， 4 - 任意价， 
	,SellFutuPrice				int
	,BuySpotPrice				int
	,SellSpotPrice				int
	,BuySellEntrustOrder		int	--1 按市值从高到低，2-按市值从低到高， 3 - 按 中金所、深交所、上交所， 4 - 按上交所、深交所、中金所
	,OddShareMode				int	-- 1 四舍五入， 2 - 直接入， 3 - 直接舍
	,SZSEEntrustPriceType		int	-- a - 五档即成剩撤， b - 五档即成剩转
	,SSEEntrustPriceType		int	-- A - 五档即成剩撤， C - 即成剩撤，D - 对手方最优，E - 本方最优，F - 全额成或撤
	,CreatedDate				datetime	
	,ModifiedDate				datetime
)

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
	else
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