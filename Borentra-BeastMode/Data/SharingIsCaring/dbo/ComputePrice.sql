CREATE FUNCTION [dbo].[ComputePrice]
(
	@Price money
	, @Unit tinyint
	, @From smalldatetime
	, @To smalldatetime
)
RETURNS MONEY
BEGIN

	DECLARE @Units int;

	IF @Unit = 1
	BEGIN

		SET @Units = DATEDIFF(DAY, @From, @To);

	END

	RETURN @Price * @Units;

END