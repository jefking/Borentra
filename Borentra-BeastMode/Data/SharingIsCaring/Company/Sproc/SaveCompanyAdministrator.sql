CREATE PROCEDURE [Company].[SaveCompanyAdministrator]
	@CompanyIdentifier UNIQUEIDENTIFIER = NULL
    , @UserIdentifier UNIQUEIDENTIFIER = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	IF [dbo].[UUIDIsInvalid](@CompanyIdentifier) = 1
	BEGIN

		RAISERROR(N'Company Identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF [dbo].[UUIDIsInvalid](@UserIdentifier) = 1
	BEGIN

		RAISERROR(N'User Identifier must be specified and valid.'
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
	ELSE IF NOT EXISTS (SELECT 0
						FROM [Company].[vwCompany] WITH (NOLOCK)
						WHERE @CompanyIdentifier = Identifier)
	BEGIN

		RAISERROR(N'Company doesn''t exist.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN

		MERGE [Company].[CompanyAdministrator] AS [Data]
		USING (
			SELECT 
				@CompanyIdentifier AS [CompanyIdentifier]
				, @UserIdentifier AS [UserIdentifier]
			) AS NewData
		ON [Data].[CompanyIdentifier] = [NewData].[CompanyIdentifier]
			AND [Data].[CompanyIdentifier] = [NewData].[CompanyIdentifier]
		WHEN MATCHED THEN UPDATE
		SET ModifiedOn = GETUTCDATE()
		WHEN NOT MATCHED THEN INSERT
			(
				[CompanyIdentifier]
				, [UserIdentifier]
			)
			VALUES
			(
				[NewData].[CompanyIdentifier]
				, [NewData].[UserIdentifier]
			);

	END
END