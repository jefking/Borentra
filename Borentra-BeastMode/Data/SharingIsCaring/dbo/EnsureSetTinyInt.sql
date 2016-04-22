CREATE FUNCTION [dbo].[EnsureSetTinyInt]
(
	@Value TINYINT = NULL
	, @Default TINYINT = NULL
)
RETURNS TINYINT
BEGIN

	IF @Value IS NULL
	BEGIN
	
		SET @Value = @Default;

	END

	RETURN @Value;

END