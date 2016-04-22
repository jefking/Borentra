CREATE FUNCTION dbo.SubstringFormat
(
	@String NVARCHAR(MAX)
	, @MaxLength INT
)
RETURNS NVARCHAR(MAX)
BEGIN

	SET @String = [dbo].[Trim](@String);

	IF @MaxLength < LEN(@String)
	BEGIN

		SET @String = SUBSTRING(@String, 0, @MaxLength) + '...'

	END
	
	RETURN @String;

END