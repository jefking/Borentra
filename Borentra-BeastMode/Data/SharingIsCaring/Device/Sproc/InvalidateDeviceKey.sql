CREATE PROCEDURE [Device].[InvalidateDeviceKey]
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
	ELSE IF NOT EXISTS (SELECT 0
						FROM [Device].[vwDevice] WITH (NOLOCK)
						WHERE @Identifier = Identifier)
	BEGIN

		RAISERROR(N'Device doesn''t exist.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN

		UPDATE [Device].[Device]
		SET [ModifiedOn] = GETUTCDATE()
			, [KeyExpiresOn] = GETUTCDATE()
		WHERE [Identifier] = @Identifier;

		EXECUTE [Device].[GetDevice]
			@Identifier = @Identifier

	END
END