CREATE FUNCTION [dbo].[EnsureSetMoney]
(
	@Value MONEY = NULL
	, @Default MONEY = NULL
)
RETURNS FLOAT
BEGIN

	IF @Value IS NULL
	BEGIN
	
		SET @Value = @Default;

	END

	RETURN @Value;

END