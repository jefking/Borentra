CREATE PROCEDURE [Social].[SearchConversation]
	@UserIdentifier uniqueidentifier = NULL
	, @Identifier uniqueidentifier = NULL
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
	ELSE IF [dbo].[UUIDIsInvalid](@Identifier) = 0
		AND NOT EXISTS (SELECT 0
						FROM [Social].[vwConversation] WITH (NOLOCK)
						WHERE @Identifier = Identifier
							AND @UserIdentifier IN ([ToUserIdentifier], [UserIdentifier]))
	BEGIN

		RAISERROR(N'Conversation doesn''t exist.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN
	
		CREATE TABLE #Results
		(
			[Identifier] uniqueidentifier PRIMARY KEY
			, [ChildUnRead] bit DEFAULT(0)
		)

		INSERT INTO #Results
		(
			[Identifier]
			, [ChildUnRead]
		)
		SELECT [Comment].[Identifier]
			, (SELECT TOP (1) 0
				FROM [Social].[vwConversation] [Child] WITH (NOLOCK)
				WHERE
				(
					[Child].[ParentConversationIdentifier] = [Comment].[Identifier]
					OR
					[Child].[Identifier] = [Comment].[Identifier]
				)
				AND [Child].[Read] = 0
				AND [Child].[ToUserIdentifier] = @UserIdentifier
			) AS [ChildUnRead]
		FROM [Social].[vwConversation] [Comment] WITH (NOLOCK)
		WHERE @UserIdentifier IN ([Comment].[ToUserIdentifier], [Comment].[UserIdentifier])
			AND
			(
				@Identifier IN ([Comment].[ParentConversationIdentifier], [Comment].Identifier)
				OR
				(
					@Identifier IS NULL
					AND
					[Comment].[ParentConversationIdentifier] IS NULL
				)
			)

		SELECT [Comment].[Identifier]
			, [Comment].[ParentConversationIdentifier]
			, [Comment].[UserIdentifier] AS [FromUserIdentifier]
			, [Comment].[ToUserIdentifier]
			, COALESCE([Results].[ChildUnRead], CASE [Comment].[ToUserIdentifier] WHEN @UserIdentifier THEN [Comment].[Read] ELSE 1 END) AS [Read]
			, [Comment].[Comment] AS [Body]
			, [Comment].[ModifiedOn] AS [On]
			, [FromUser].[DisplayName] AS [FromDisplayName]
			, [FromUser].[FacebookId] AS [FromFacebookId]
			, [FromUser].[Key] AS [FromKey]
			, [ToUser].[DisplayName] AS [ToDisplayName]
			, [ToUser].[FacebookId] AS [ToFacebookId]
			, [ToUser].[Key] AS [ToKey]
		FROM [Social].[vwConversation] [Comment] WITH (NOLOCK)
			INNER JOIN [User].[vwProfile] [FromUser] WITH (NOLOCK)
				ON [Comment].[UserIdentifier] = [FromUser].[UserIdentifier]
			INNER JOIN [User].[vwProfile] [ToUser] WITH (NOLOCK)
				ON [Comment].[ToUserIdentifier] = [ToUser].[UserIdentifier]
			INNER JOIN [#Results] [Results] WITH(NOLOCK)
				ON [Comment].[Identifier] = [Results].[Identifier]
		ORDER BY COALESCE([Results].[ChildUnRead], CASE [Comment].[ToUserIdentifier] WHEN @UserIdentifier THEN [Comment].[Read] ELSE 1 END) ASC
			, [Comment].[CreatedOn] DESC
		
		CLEANUP:
			DROP TABLE #Results;

	END
END