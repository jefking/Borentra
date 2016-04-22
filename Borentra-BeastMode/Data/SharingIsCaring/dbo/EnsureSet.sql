CREATE FUNCTION [dbo].[EnsureSet]
(
	@Value INT = NULL
	, @Default INT = NULL
)
RETURNS INT
BEGIN

	IF @Value IS NULL
		OR @Value <= 0
	BEGIN

		SET @Value = @Default;

	END

	RETURN @Value;

END