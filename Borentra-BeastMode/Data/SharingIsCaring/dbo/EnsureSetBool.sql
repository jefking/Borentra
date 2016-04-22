CREATE FUNCTION [dbo].[EnsureSetBool]
(
	@Value BIT = NULL
)
RETURNS BIT
BEGIN

	IF @Value IS NULL
	BEGIN

		SET @Value = 0;

	END

	RETURN @Value;

END