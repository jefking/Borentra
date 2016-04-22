CREATE PROCEDURE [Social].[SaveConversation]
	@UserIdentifier uniqueidentifier = NULL
	, @ToUserIdentifier uniqueidentifier = NULL
	, @ParentConversationIdentifier uniqueidentifier = NULL
	, @Read bit = 0
	, @Comment nvarchar(2048) = NULL
	, @Identifier uniqueidentifier = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Comment = [dbo].[TrimOrNull](@Comment);

	SET @Read = [dbo].[EnsureSetBool](@Read);

	IF [dbo].[UUIDIsInvalid](@Identifier) = 1
	BEGIN

		SET @Identifier = NEWID();

	END
	
	IF [dbo].[UUIDIsInvalid](@UserIdentifier) = 1
	BEGIN

		RAISERROR(N'User identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF [dbo].[UUIDIsInvalid](@ToUserIdentifier) = 1
		AND @Read = 0
	BEGIN

		RAISERROR(N'To user identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @ToUserIdentifier = @UserIdentifier
	BEGIN

		RAISERROR(N'User cannot talk to themself.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF (@Comment IS NULL
			OR @Comment ='')
		AND @Read = 0
	BEGIN

		RAISERROR(N'Comment must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF [dbo].[UUIDIsInvalid](@ParentConversationIdentifier) = 0
		AND NOT EXISTS (SELECT 0
						FROM [Social].[vwConversation] WITH (NOLOCK)
						WHERE @ParentConversationIdentifier = Identifier
							AND @UserIdentifier IN (UserIdentifier, ToUserIdentifier) -- User must be in parent message
							AND @ToUserIdentifier IN (UserIdentifier, ToUserIdentifier) -- To user must be in parent message
							AND ParentConversationIdentifier IS NULL) -- parent message must be 'root'
	BEGIN

		RAISERROR(N'Parent message doesn''t exist.'
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
	ELSE IF @Read = 0
		AND NOT EXISTS (SELECT 0
					FROM [User].[vwProfile] WITH (NOLOCK)
					WHERE @ToUserIdentifier = UserIdentifier)
	BEGIN

		RAISERROR(N'To user doesn''t exist.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @Read = 1
		AND NOT EXISTS (SELECT 0
						FROM [Social].[vwConversation] WITH (NOLOCK)
						WHERE @Identifier = Identifier
							AND @UserIdentifier IN (ToUserIdentifier, UserIdentifier)) -- User must be in parent message
	BEGIN

		RAISERROR(N'Read message doesn''t exist.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @Read = 1
	BEGIN

		UPDATE [Social].[Conversation]
		SET ModifiedOn = GETUTCDATE()
			, [Read] = @Read
		WHERE ToUserIdentifier = @UserIdentifier
			AND @Identifier IN (ParentConversationIdentifier, Identifier)
			AND [Read] = 0;

	END
	BEGIN

		MERGE [Social].[Conversation] AS [Comment]
			USING (
				SELECT 
					@Identifier AS [Identifier]
					, @ParentConversationIdentifier AS [ParentConversationIdentifier]
					, @UserIdentifier AS [UserIdentifier]
					, @ToUserIdentifier AS [ToUserIdentifier]
					, @Read AS [Read]
					, @Comment AS [Comment]
				) AS NewData
			ON [Comment].[Identifier] = [NewData].[Identifier]
			WHEN NOT MATCHED
			THEN INSERT
				(
					[Identifier]
					, [ParentConversationIdentifier]
					, [UserIdentifier]
					, [ToUserIdentifier]
					, [Read]
					, [CreatedOn]
					, [ModifiedOn]
					, [Comment]
				)
				VALUES
				(
					[NewData].[Identifier]
					, [NewData].[ParentConversationIdentifier]
					, [NewData].[UserIdentifier]
					, [NewData].[ToUserIdentifier]
					, [NewData].[Read]
					, GETUTCDATE()
					, GETUTCDATE()
					, [NewData].[Comment]
				);

		EXECUTE [Social].[SearchConversation]
			@Identifier = @Identifier
			, @UserIdentifier = @UserIdentifier;

	END
END