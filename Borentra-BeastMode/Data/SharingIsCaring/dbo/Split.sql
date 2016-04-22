CREATE FUNCTION [dbo].[Split]
(
	@String nvarchar(max)
)
returns @SplitValues table
(
    id uniqueidentifier primary key
)
AS
BEGIN
	declare @SplitLength int, @Delimiter varchar(5)
    
    set @Delimiter = ','
    
    while len(@String) > 0
    begin 
        select @SplitLength = (case charindex(@Delimiter,@String) when 0 then
            len(@String) else charindex(@Delimiter,@String) -1 end)
 
        insert into @SplitValues
        select substring(@String,1,@SplitLength) 
    
        select @String = (case (len(@String) - @SplitLength) when 0 then  ''
            else right(@String, len(@String) - @SplitLength - 1) end)
    end 
return  
END
