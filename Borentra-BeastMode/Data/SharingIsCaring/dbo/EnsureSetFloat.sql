CREATE FUNCTION [dbo].[EnsureSetFloat]
(
	@Value FLOAT = NULL
	, @Default FLOAT = NULL
)
RETURNS FLOAT
BEGIN

	IF @Value IS NULL
	BEGIN
	
		SET @Value = @Default;

	END

	RETURN @Value;

END