CREATE FUNCTION [dbo].[IsAdministrator]
(
	@Identifier uniqueidentifier
)
RETURNS BIT
BEGIN

	DECLARE @Value BIT
	IF @Identifier = 'BA8B47BD-63CE-40B7-B68E-03D2320AA279'
	BEGIN
	
		SET @Value = 1;

	END
	ELSE
	BEGIN
	
		SET @Value = 0;

	END

	RETURN @Value;

END