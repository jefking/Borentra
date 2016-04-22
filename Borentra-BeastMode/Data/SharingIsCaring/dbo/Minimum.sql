CREATE FUNCTION [dbo].[Minimum]
(
	@Value1 tinyint
	, @Value2 tinyint
	, @Value3 tinyint
	, @Value4 tinyint
)
RETURNS TINYINT
BEGIN

	DECLARE @Value tinyint

	SELECT @Value = MIN(x)
	FROM (
			SELECT @Value1 AS 'x'
		UNION
			SELECT @Value2 AS 'x'
		UNION
			SELECT @Value3 AS 'x'
		UNION
			SELECT @Value4 AS 'x' 
	) a

	RETURN @Value
END