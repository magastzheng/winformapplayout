use tradingsystem

exec procFuturesContractInsert @Code='IC1605'
	,@Name='中证500期货1605合约'
	,@Exchange='CFFEX'
	,@PriceLimits=10 
	,@Deposit=40
	,@ListedDate='2016-03-31'
	,@LastTradingDay='2016-05-20'
	,@LastDeliveryDay='2016-05-20'

exec procFuturesContractInsert @Code='IC1606'
	,@Name='中证500期货1606合约'
	,@Exchange='CFFEX'
	,@PriceLimits=10 
	,@Deposit=40
	,@ListedDate='2015-10-19'
	,@LastTradingDay='2016-06-17'
	,@LastDeliveryDay='2016-06-17'

exec procFuturesContractInsert @Code='IC1609'
	,@Name='中证500期货1609合约'
	,@Exchange='CFFEX'
	,@PriceLimits=10 
	,@Deposit=40
	,@ListedDate='2016-01-18'
	,@LastTradingDay='2016-09-19'
	,@LastDeliveryDay='2016-09-19'

exec procFuturesContractInsert @Code='IC1612'
	,@Name='中证500期货1612合约'
	,@Exchange='CFFEX'
	,@PriceLimits=10 
	,@Deposit=40
	,@ListedDate='2016-04-18'
	,@LastTradingDay='2016-12-16'
	,@LastDeliveryDay='2016-12-16'

exec procFuturesContractInsert @Code='IF1605'
	,@Name='沪深300期货1605合约'
	,@Exchange='CFFEX'
	,@PriceLimits=10 
	,@Deposit=40
	,@ListedDate='2016-03-21'
	,@LastTradingDay='2016-12-16'
	,@LastDeliveryDay='2016-12-16'

--获取股指期货信息
select 
F1_3511 as Code
,F2_3511 as Name
,F4_3511 as Exchange
,F6_3511 as ListedDate
,F7_3511 as LastTradingDay
,F9_3511 as LastDeliveryDay
,F10_3511 as Status
from [10.1.37.70].wind.dbo.TB_OBJECT_3511
where F1_3511 like 'IF16%'

select 
*
from [10.1.37.70].wind.dbo.TB_OBJECT_3512
where F1_3512 like 'IF16%'

--获取16年有效股指期货合约
declare cursor_fc cursor
for
select 
	F1_3511 as Code
	,F2_3511 as Name
	,F4_3511 as Exchange
	,F6_3511 as ListedDate
	,F7_3511 as LastTradingDay
	,F9_3511 as LastDeliveryDay
	,F10_3511 as Status
from [10.1.37.70].wind.dbo.TB_OBJECT_3511
where F1_3511 like 'IF16%' or F1_3511 like 'IH16%' or F1_3511 like 'IC16%'
  	
declare @Code varchar(10)
declare @Name varchar(50)
declare @Exchange varchar(10)
declare @ListedDate varchar(10)
declare @LastTradingDay varchar(10)
declare @LastDeliveryDay varchar(10)
declare @Status int

open cursor_fc	
fetch next from cursor_fc into @Code, @Name, @Exchange, @ListedDate, @LastTradingDay, @LastDeliveryDay, @Status
while @@fetch_status = 0
begin
	if @Status = 1
	begin
		--exec procFuturesContractInsert @Code
		--,@Name
		--,@Exchange
		--,10 
		--,40
		--,@ListedDate
		--,@LastTradingDay
		--,@LastDeliveryDay
		--,convert(datetime, @ListedDate, 121)
		--,convert(datetime, @LastTradingDay, 121)
		--,convert(datetime, @LastDeliveryDay, 121)
		print @Code
		declare @total int
		select @total=count(Code) from futurescontract where Code=@Code
		if @total > 0
		begin
			print @Code + ' has been added!'
		end
		else
		begin
			print @Code + ' new one!'
			insert into futurescontract(
				Code
				,Name
				,Exchange
				,PriceLimits
				,Deposit
				,ListedDate
				,LastTradingDay
				,LastDeliveryDay
				,Status
			)
			values
			(
				@Code
				,@Name
				,@Exchange
				,10
				,40
				,@ListedDate
				,@LastTradingDay
				,@LastDeliveryDay
				,@Status
			)
		end
	end
  	fetch next from cursor_fc into @Code, @Name, @Exchange, @ListedDate, @LastTradingDay, @LastDeliveryDay, @Status
end
close cursor_fc
deallocate cursor_fc


--exec procFuturesContractInsert @Code='IC1605'
--	,@Name='中证500期货1605合约'
--	,@Exchange='CFFEX'
--	,@PriceLimits=10 
--	,@Deposit=40
--	,@ListedDate='2016-03-31'
--	,@LastTradingDay='2016-05-20'
--	,@LastDeliveryDay='2016-05-20'
select * from futurescontract
delete from futurescontract where Code like 'IF16%'
delete from futurescontract where Code like 'IH16%'

declare @Code varchar(10)
declare @findCode varchar(10)
set @Code='IC1607'
select @findCode=Code from futurescontract where Code=@Code
print @findCode

exec procFuturesContractSelect