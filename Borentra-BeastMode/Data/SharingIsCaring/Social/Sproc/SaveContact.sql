CREATE PROCEDURE [Social].[SaveContact]
	@UserIdentifier uniqueidentifier = NULL
	, @Email nvarchar(255) = NULL
	, @FirstName nvarchar(128) = NULL
	, @LastName nvarchar(128) = NULL
	, @PhoneNumber nvarchar(18) = NULL
	, @Invite bit = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Email = [dbo].[TrimOrNull](@Email);
	SET @FirstName = [dbo].[TrimOrNull](@FirstName);
	SET @LastName = [dbo].[TrimOrNull](@LastName);
	SET @PhoneNumber = [dbo].[TrimOrNull](@PhoneNumber);
	SET @Invite = [dbo].[EnsureSetBool](@Invite);

	IF [dbo].[UUIDIsInvalid](@UserIdentifier) = 1
	BEGIN

		RAISERROR(N'User identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @Email IS NULL
			OR @Email = ''
	BEGIN

		RAISERROR(N'Email must be specified and valid.'
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
		
		SET @Email = LOWER(@Email);
		SET @PhoneNumber = LOWER(@PhoneNumber);

		MERGE [Social].[Contact] AS Data
			USING (
				SELECT @UserIdentifier AS [UserIdentifier]
					, @Email AS [Email]
					, @FirstName AS [FirstName]
					, @LastName AS [LastName]
					, @PhoneNumber AS [PhoneNumber]
					, @Invite AS [Invite]
				) AS NewData
			ON [Data].[UserIdentifier] = [NewData].[UserIdentifier]
				AND [Data].[Email] = [NewData].[Email]
		WHEN MATCHED
		THEN UPDATE
			SET [Data].[ModifiedOn] = GETUTCDATE()
				, [Data].[FirstName] = COALESCE([NewData].[FirstName], [Data].[FirstName])
				, [Data].[LastName] = COALESCE([NewData].[LastName], [Data].[LastName])
				, [Data].[PhoneNumber] = COALESCE([NewData].[PhoneNumber], [Data].[PhoneNumber])
				, [Data].[Invite] = COALESCE([NewData].[FirstName], [Data].[Invite])
		WHEN NOT MATCHED
		THEN INSERT
			(
				[UserIdentifier]
				, [Email]
				, [FirstName]
				, [LastName]
				, [PhoneNumber]
				, [Invite]
			)
			VALUES
			(
				[NewData].[UserIdentifier]
				, [NewData].[Email]
				, [NewData].[FirstName]
				, [NewData].[LastName]
				, [NewData].[PhoneNumber]
				, [NewData].[Invite]
			);

	END
END