CREATE PROCEDURE [User].[SaveProfileArchive]
	@UserIdentifier uniqueidentifier = NULL
	, @LandingTheme nvarchar(128) = NULL
AS
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

		MERGE [User].[ProfileArchive] AS Data
			USING (
				SELECT @UserIdentifier AS [UserIdentifier]
					, @LandingTheme AS [LandingTheme]
				) AS NewData
			ON [Data].[UserIdentifier] = [NewData].[UserIdentifier]
		WHEN NOT MATCHED
		THEN INSERT
			(
				[UserIdentifier]
				, [LandingTheme]
			)
			VALUES
			(
				[NewData].[UserIdentifier]
				, [NewData].[LandingTheme]
			);

	END
END