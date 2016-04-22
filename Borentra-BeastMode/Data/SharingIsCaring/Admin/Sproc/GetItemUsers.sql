CREATE PROCEDURE [Admin].[GetItemUsers]
	@Identifier uniqueidentifier = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	IF [dbo].[UUIDIsInvalid](@Identifier) = 1
	BEGIN

		RAISERROR(N'Identifier must be specified and valid.'
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
			[UserIdentifier] uniqueidentifier PRIMARY KEY
		)
		
		DECLARE @PrivacyLevel tinyint
		DECLARE @UserIdentifier uniqueidentifier
		
		SELECT @PrivacyLevel = [dbo].[Maximum]([profile].[PrivacyLevel], [item].[MinimumPrivacyLevel])
			, @UserIdentifier = [profile].[UserIdentifier]
		FROM [User].[vwProfile] [profile] WITH(NOLOCK)
			INNER JOIN [Goods].[vwItem] [item] WITH(NOLOCK)
				ON [profile].[UserIdentifier] = [item].[UserIdentifier]
					AND [item].[Identifier] = @Identifier

		IF @PrivacyLevel > 1
		BEGIN

			INSERT INTO #Results
			(
				UserIdentifier
			)
			SELECT @UserIdentifier

			IF @PrivacyLevel = 2
			BEGIN

				INSERT INTO #Results
				(
					UserIdentifier
				)
				SELECT UserIdentifier
				FROM [User].[vwProfile] [profile] WITH(NOLOCK)
				WHERE UserIdentifier <> @UserIdentifier

			END
			ELSE IF @PrivacyLevel = 3
			BEGIN

				INSERT INTO #Results
				(
					UserIdentifier
				)
				SELECT [network].[ConnectionIdentifier]
				FROM [Social].[vwNetwork] [network] WITH (NOLOCK)
					INNER JOIN [User].[vwProfile] [profile] WITH(NOLOCK)
						ON [network].[OwnerIdentifier] = @UserIdentifier
							AND [profile].[UserIdentifier] = [network].[OwnerIdentifier]
							AND [network].[ConnectionIdentifier] <> @UserIdentifier

			END
		END
		
		SELECT UserIdentifier
		FROM #Results
		
		CLEANUP:
			DROP TABLE #Results;

	END
END