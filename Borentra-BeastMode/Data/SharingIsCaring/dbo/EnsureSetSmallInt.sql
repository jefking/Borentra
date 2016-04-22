CREATE FUNCTION [dbo].[EnsureSetSmallInt]
(
	@Value SMALLINT = NULL
	, @Default SMALLINT = NULL
)
RETURNS SMALLINT
BEGIN

	IF @Value IS NULL
	BEGIN
	
		SET @Value = @Default;

	END

	RETURN @Value;

END