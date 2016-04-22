CREATE PROCEDURE [Test].[SaveGroupSignUp]
	@UserIdentifier [uniqueidentifier] = NULL
	, @Email nvarchar(255) = NULL
	, @Name nvarchar(255) = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Email = [dbo].[TrimOrNull](@Email);
	SET @Name = [dbo].[TrimOrNull](@Name);
	
	IF [dbo].[UUIDIsInvalid](@UserIdentifier) = 1
	BEGIN

		SET @UserIdentifier = NULL;

	END

	IF @Email IS NULL
	BEGIN
	
		RAISERROR(N'Email has to be set.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @Name IS NULL
	BEGIN
	
		RAISERROR(N'Name has to be set.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN

		INSERT INTO [Test].[SignUp]
		(
			Email
			, Name
			, [Type]
			, UserIdentifier
		)
		VALUES
		(
			@Email
			, @Name
			, 2
			, @UserIdentifier
		);

	END
END