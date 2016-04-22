CREATE FUNCTION [dbo].[EnsureSetBigInt]
(
	@Value bigint = NULL
	, @Default bigint = NULL
)
RETURNS bigint
BEGIN

	IF @Value IS NULL
		OR @Value <= 0
	BEGIN

		SET @Value = @Default;

	END

	RETURN @Value;

END