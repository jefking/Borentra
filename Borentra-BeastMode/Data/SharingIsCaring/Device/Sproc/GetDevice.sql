CREATE PROCEDURE [Device].[GetDevice]
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

		SELECT [Identifier]
		  , [UserIdentifier]
		  , [FacebookIsValidated]
		  , [Amplitude]
		  , [VerticalOffset]
		  , [AngularFrequency]
		  , [PhaseShift]
		  , [KeyExpiresOn]
		  , [DeviceIdentifier]
		  , [OS] AS [OperatingSystem]
		FROM [Device].[vwDevice] [device] with (NOLOCK)
		WHERE Identifier = @Identifier

	END
END