CREATE PROCEDURE [Social].[SearchBadgeInformation]
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SELECT [Title]
		, [Description]
		, [Points]
		, [IconName]
		, (SELECT COUNT(0)
			FROM [Social].[vwBadge] [badge] with(nolock)
				INNER JOIN [User].[vwProfile] [profile] with(nolock)
					ON [badge].[UserIdentifier] = [profile].[UserIdentifier]
						AND [badge].[BadgeId] = [info].[Identifier]
			) AS [MembersWithBadge]
	FROM [Social].[vwBadgeInformation] [info] WITH(NOLOCK)
	ORDER BY Points

END