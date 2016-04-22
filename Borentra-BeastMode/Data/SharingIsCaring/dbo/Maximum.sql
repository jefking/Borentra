CREATE FUNCTION [dbo].[Maximum]
(
	@Value1 tinyint
	, @Value2 tinyint
)
RETURNS TINYINT
BEGIN

	DECLARE @Value tinyint

	SELECT @Value = MAX(x)
	FROM (
			SELECT @Value1 AS 'x'
		UNION
			SELECT @Value2 AS 'x'
	) a

	RETURN @Value
END