CREATE FUNCTION dbo.TrimOrNull
(
	@String NVARCHAR(MAX)
)
RETURNS NVARCHAR(MAX)
BEGIN

	SET @String = [dbo].[Trim](@String);

	IF @String IS NULL
		OR @String = ''
	BEGIN

		SET @String = NULL;

	END
	
	RETURN @String;

END