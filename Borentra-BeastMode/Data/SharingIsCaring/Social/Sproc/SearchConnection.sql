CREATE PROCEDURE [Social].[SearchConnection]
	@OwnerIdentifier [uniqueidentifier] = NULL
	, @ConnectionIdentifier [uniqueidentifier] = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SELECT [network].[ConnectionIdentifier]
		, [network].[OwnerIdentifier]
	FROM [Social].[vwNetwork] [network]
	WHERE [network].[ConnectionIdentifier] = @ConnectionIdentifier
		OR [network].[OwnerIdentifier] = @OwnerIdentifier

END