CREATE FUNCTION [dbo].[ConvertIpAddress]
(
	@IpAddress nvarchar(15) = NULL
)
RETURNS BIGINT
BEGIN

	DECLARE @Value BIGINT;

	SET @IpAddress = [dbo].[TrimOrNull](@IpAddress)

	IF @IpAddress IS NULL
	BEGIN
	
		SET @Value = 0;

	END
	ELSE
	BEGIN
	
		DECLARE @o1 bigint;
		DECLARE @o2 smallint;
		DECLARE @o3 smallint;
		DECLARE @o4 smallint;
		DECLARE @FromIndex int
		DECLARE @Index int

		SET @FromIndex = 0
		SET @Index = CHARINDEX('.', @IpAddress, @FromIndex)

		SET @o1 = SUBSTRING(@IpAddress, @FromIndex, @Index - @FromIndex)

		SET @FromIndex = @Index + 1;
		SET @Index = CHARINDEX('.', @IpAddress, @FromIndex)

		SET @o2 = SUBSTRING(@IpAddress, @FromIndex, @Index - @FromIndex)
		
		SET @FromIndex = CHARINDEX('.', @IpAddress, @FromIndex) + 1
		SET @Index = CHARINDEX('.', @IpAddress, @FromIndex)
		
		SET @o3 = SUBSTRING(@IpAddress, @FromIndex, @Index - @FromIndex)
		
		SET @FromIndex = @Index + 1
		SET @Index = LEN(@IpAddress) + 1
		
		SET @o4 = SUBSTRING(@IpAddress, @FromIndex, @Index - @FromIndex)

		SET @Value = (16777216 * @o1) + (65536 * @o2) + (256 * @o3) + @o4;

	END

	RETURN @Value;

END