CREATE PROCEDURE [Social].[SaveFacebookConnection]
	@OwnerIdentifier [uniqueidentifier] = NULL
	, @FacebookId [bigint] = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @FacebookId = [dbo].[EnsureSetBigInt](@FacebookId, 0);

	IF [dbo].[UUIDIsInvalid](@OwnerIdentifier) = 1
	BEGIN

		RAISERROR(N'Owner identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @FacebookId = 0
	BEGIN

		RAISERROR(N'Facebook identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF NOT EXISTS (SELECT 0
						FROM [User].[vwProfile] WITH (NOLOCK)
						WHERE UserIdentifier = @OwnerIdentifier)
	BEGIN

		RAISERROR(N'Owner doesnt exist.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF NOT EXISTS (SELECT 0
						FROM [User].[vwProfile] WITH (NOLOCK)
						WHERE FacebookId = @FacebookId)
	BEGIN

		RAISERROR(N'Facebook user doesnt exist.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN
	
		DECLARE @ConnectionIdentifier [uniqueidentifier];

		SELECT @ConnectionIdentifier = UserIdentifier
		FROM [User].[vwProfile] WITH (NOLOCK)
		WHERE FacebookId = @FacebookId;
		
		IF @ConnectionIdentifier = @OwnerIdentifier
		BEGIN

			RAISERROR(N'Facebook user doesnt exist.'
							, 15
							, 1
						)
						WITH SETERROR;
			RETURN;

		END
		ELSE IF [dbo].[UUIDIsInvalid](@ConnectionIdentifier) = 0
			AND NOT EXISTS (SELECT 0
				FROM [Social].[vwNetwork] WITH (NOLOCK)
					WHERE OwnerIdentifier = @OwnerIdentifier
						AND ConnectionIdentifier = @ConnectionIdentifier
				)
		BEGIN
		
			EXECUTE [Social].[SaveConnection]
				@OwnerIdentifier = @OwnerIdentifier
				, @ConnectionIdentifier = @ConnectionIdentifier
				, @NetworkType = 1; -- Facebook Network Auto-Connection
				
			-- So we don't have to spam as much
			EXECUTE [Social].[SaveConnection]
				@OwnerIdentifier = @ConnectionIdentifier
				, @ConnectionIdentifier = @OwnerIdentifier
				, @NetworkType = 1; -- Facebook Network Auto-Connection

			SELECT [user].[DisplayName] AS UserDisplayName
				, [user].[Email] AS UserEmail
				, [user].[FacebookId] AS UserFacebookId
				, [user].[UserIdentifier] AS UserIdentifier
				, [friend].[DisplayName] AS FriendDisplayName
				, [friend].[Key] AS FriendKey
				, [friend].[FacebookId] AS FriendFacebookId
				, [friend].[UserIdentifier] AS FriendIdentifier
			FROM [Social].[vwNetwork] [network] WITH (NOLOCK)
				INNER JOIN [User].[vwProfile] [friend] WITH (NOLOCK)
					ON [network].[OwnerIdentifier] = [friend].[UserIdentifier]
						AND [network].[OwnerIdentifier] = @OwnerIdentifier
				INNER JOIN [User].[vwProfile] [user] WITH (NOLOCK)
					ON [network].[ConnectionIdentifier] = [user].[UserIdentifier]
						AND [network].[ConnectionIdentifier] = @ConnectionIdentifier

		END
	END
END