﻿
CREATE VIEW [User].[vwProfile]
AS
	SELECT [UserIdentifier]
		, [DisplayName]
		, [Profile].[FacebookId]
		, [Profile].[Status]
		, [Profile].[Key]
		, [Profile].[Email]
		, [Profile].[FacebookAccessToken]
		, [Profile].[FacebookTokenExpiration]
		, [Profile].[Location]
		, [Profile].[CreatedOn]
		, [Profile].[PrivacyLevel]
		, [Profile].[GeoLocation]
		, [Profile].[SearchRadius]
		, [Profile].[ModifiedOn]
		, [Profile].[IpAddress]
	FROM [User].[Profile] [Profile]
	WHERE [Profile].[DeletedOn] IS NULL