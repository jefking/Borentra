CREATE VIEW [Social].[vwConversation]
AS
	SELECT [Identifier]
		, [ParentConversationIdentifier]
		, [UserIdentifier]
		, [ToUserIdentifier]
		, [Read]
		, [CreatedOn]
		, [ModifiedOn]
		, [Comment]
	FROM [Social].[Conversation]
	WHERE [DeletedOn] IS NULL