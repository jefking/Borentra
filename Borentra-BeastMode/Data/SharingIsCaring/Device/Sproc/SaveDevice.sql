CREATE PROCEDURE [Device].[SaveDevice]
	@Identifier uniqueidentifier = NULL
	, @UserIdentifier uniqueidentifier = NULL
	, @DeviceIdentifier uniqueidentifier = NULL
	, @FacebookId bigint = 0
	, @FacebookAccessToken nvarchar(512) = NULL
	, @FacebookTokenExpiration datetime = NULL
	, @FacebookIsValidated bit = 0
	, @Os tinyint = 0
	, @IpAddress nvarchar(15) = NULL
	, @Amplitude INT = NULL
	, @VerticalOffset INT = NULL
	, @AngularFrequency INT = NULL
	, @PhaseShift INT = NULL
	, @KeyExpiresOn DATETIME = NULL
	, @LastValidatedOn DATETIME = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Identifier = COALESCE(@Identifier, NEWID());

	IF [dbo].[UUIDIsInvalid](@UserIdentifier) = 1
	BEGIN

		RAISERROR(N'User identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF [dbo].[UUIDIsInvalid](@DeviceIdentifier) = 1
	BEGIN

		RAISERROR(N'Device identifier must be specified and valid.'
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

		SELECT @Identifier = Identifier
		FROM [Device].[vwDevice] [device] with(nolock)
		WHERE UserIdentifier = @UserIdentifier
			AND DeviceIdentifier = @DeviceIdentifier;

		MERGE [Device].[Device] AS Data
		USING (
				SELECT @Identifier AS [Identifier]
					, @UserIdentifier AS [UserIdentifier]
					, @DeviceIdentifier AS [DeviceIdentifier]
					, @FacebookAccessToken AS [FacebookAccessToken]
					, @FacebookTokenExpiration AS [FacebookTokenExpiration]
					, @FacebookIsValidated AS [FacebookIsValidated]
					, @Os AS [Os]
					, @IpAddress AS [IpAddress]
					, @Amplitude AS [Amplitude]
					, @VerticalOffset AS [VerticalOffset]
					, @AngularFrequency AS [AngularFrequency]
					, @PhaseShift AS [PhaseShift]
					, @KeyExpiresOn AS [KeyExpiresOn]
					, @LastValidatedOn AS [LastValidatedOn]
			) AS NewData
		ON [Data].[UserIdentifier] = [NewData].[UserIdentifier]
			AND [Data].[DeviceIdentifier] = [NewData].[DeviceIdentifier]
		WHEN MATCHED
		THEN UPDATE
			SET [Data].[ModifiedOn] = GETUTCDATE()
				, [Data].[FacebookAccessToken] = COALESCE([NewData].[FacebookAccessToken], [Data].[FacebookAccessToken])
				, [Data].[FacebookTokenExpiration] = COALESCE([NewData].[FacebookTokenExpiration], [Data].[FacebookTokenExpiration])
				, [Data].[FacebookIsValidated] = COALESCE([NewData].[FacebookIsValidated], [Data].[FacebookIsValidated])
				, [Data].[IpAddress] = COALESCE([NewData].[IpAddress], [Data].[IpAddress])
				, [Data].[Amplitude] = COALESCE([NewData].[Amplitude], [Data].[Amplitude])
				, [Data].[VerticalOffset] = COALESCE([NewData].[VerticalOffset], [Data].[VerticalOffset])
				, [Data].[AngularFrequency] = COALESCE([NewData].[AngularFrequency], [Data].[AngularFrequency])
				, [Data].[PhaseShift] = COALESCE([NewData].[PhaseShift], [Data].[PhaseShift])
				, [Data].[KeyExpiresOn] = COALESCE([NewData].[KeyExpiresOn], [Data].[KeyExpiresOn])
				, [Data].[LastValidatedOn] = COALESCE([NewData].[LastValidatedOn], [Data].[LastValidatedOn])
		WHEN NOT MATCHED
		THEN INSERT
			(
				[Identifier]
				, [UserIdentifier]
				, [DeviceIdentifier]
				, [FacebookAccessToken]
				, [FacebookTokenExpiration]
				, [FacebookIsValidated]
				, [OS]
				, [IpAddress]
				, [Amplitude]
				, [VerticalOffset]
				, [AngularFrequency]
				, [PhaseShift]
				, [KeyExpiresOn]
			)
			VALUES
			(
				[NewData].[Identifier]
				, [NewData].[UserIdentifier]
				, [NewData].[DeviceIdentifier]
				, [NewData].[FacebookAccessToken]
				, [NewData].[FacebookTokenExpiration]
				, [NewData].[FacebookIsValidated]
				, [NewData].[Os]
				, [NewData].[IpAddress]
				, [NewData].[Amplitude]
				, [NewData].[VerticalOffset]
				, [NewData].[AngularFrequency]
				, [NewData].[PhaseShift]
				, [NewData].[KeyExpiresOn]
			);

		EXECUTE [Device].[GetDevice]
			@Identifier = @Identifier

	END
END