CREATE PROCEDURE [Social].[UserNotifications]
	@UserIdentifier uniqueidentifier = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	IF [dbo].[UUIDIsInvalid](@UserIdentifier) = 1
	BEGIN

		RAISERROR(N'User identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF NOT EXISTS (SELECT 0
						FROM [User].[vwProfile] WITH (NOLOCK)
						WHERE @UserIdentifier = UserIdentifier)
	BEGIN

		RAISERROR(N'User doesn''t exist.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN
	
		DECLARE @Messages INT

		SELECT @Messages = COUNT(0)
		FROM [Social].[vwConversation] WITH(NOLOCK)
		WHERE ToUserIdentifier = @UserIdentifier
			AND [Read] = 0

		SELECT COALESCE(@Messages, 0) AS NewMessages;

	END
END