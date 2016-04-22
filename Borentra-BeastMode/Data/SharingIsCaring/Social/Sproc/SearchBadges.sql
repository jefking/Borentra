CREATE PROCEDURE [Social].[SearchBadges]
	@UserIdentifier [uniqueidentifier] = NULL
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

		SELECT [badge].[BadgeId] AS [Type]
			, [info].[Description]
			, [info].[IconName]
			, [info].[Points]
			, [info].[Title]
		FROM [Social].[vwBadge] [badge] WITH (NOLOCK)
			INNER JOIN [Social].[vwBadgeInformation] [info] WITH (NOLOCK)
				ON [info].[Identifier] = [badge].[BadgeId]
					AND [badge].[UserIdentifier] = @UserIdentifier;

	END
END