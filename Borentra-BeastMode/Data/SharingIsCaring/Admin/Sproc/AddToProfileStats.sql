CREATE PROCEDURE [Admin].[AddToProfileStats]
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	INSERT INTO [User].[ProfileStats]
	(
		UserIdentifier
	)
	SELECT UserIdentifier
	FROM [User].[Profile] [profile] WITH (NOLOCK)
	WHERE [profile].[UserIdentifier] NOT IN (SELECT UserIdentifier
												FROM [User].[ProfileStats])

END