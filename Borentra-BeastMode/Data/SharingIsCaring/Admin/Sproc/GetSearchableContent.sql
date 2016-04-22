CREATE PROCEDURE [Admin].[GetSearchableContent]
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

		SELECT Identifier
			, Title
				+ ' '
				+ [Description] AS [Content]
			, [profile].[DisplayName] AS [MemberName]
			, [item].[Key]
			, 2 AS [Type]
			, (SELECT TOP 1 [Path]
				FROM [Goods].[vwItemImage] WITH (NOLOCK)
				WHERE [ItemIdentifier] = [item].[Identifier]
				ORDER BY IsPrimary DESC) AS [ImageData]
			, [profile].[GeoLocation].Lat AS [Latitude]
			, [profile].[GeoLocation].Long AS [Longitude]
			, [profile].[Location]
			, [dbo].[SubstringFormat]([Title], 50) AS [Title]
			, [dbo].[SubstringFormat]([Description], 100) AS [Description]
			, [profile].UserIdentifier
			, [item].[CreatedOn]
		FROM [Goods].[vwItem] [item] WITH(NOLOCK)
			INNER JOIN [User].[vwProfile] [profile] WITH(NOLOCK)
				ON [item].[UserIdentifier] = [profile].UserIdentifier
					AND [item].[MinimumPrivacyLevel] < 4
					AND [profile].[PrivacyLevel] < 4
	UNION
		SELECT Identifier
			, Title
				+ ' '
				+ [Description] AS [Content]
			, [profile].[DisplayName] AS [MemberName]
			, [request].[Key]
			, 3 AS [Type]
			, (SELECT TOP 1 [Path]
				FROM [Goods].[vwItemRequestImage] WITH (NOLOCK)
				WHERE [ItemRequestIdentifier] = [request].[Identifier]
				ORDER BY IsPrimary DESC) AS [ImageData]
			, [profile].[GeoLocation].Lat AS [Latitude]
			, [profile].[GeoLocation].Long AS [Longitude]
			, [profile].[Location]
			, [dbo].[SubstringFormat]([Title], 50) AS [Title]
			, [dbo].[SubstringFormat]([Description], 100) AS [Description]
			, [profile].UserIdentifier
			, [request].[CreatedOn]
		FROM [Goods].[vwItemRequest] [request] WITH(NOLOCK)
			INNER JOIN [User].[vwProfile] [profile] WITH(NOLOCK)
				ON [request].[UserIdentifier] = [profile].UserIdentifier
					AND [profile].[PrivacyLevel] < 4
	UNION
		SELECT UserIdentifier AS [Identifier]
			, [profile].[Status] AS [Content]
			, [profile].[DisplayName] AS [MemberName]
			, [profile].[Key]
			, 1 AS [Type]
			, CAST([profile].[FacebookId] AS NVARCHAR(128)) AS [ImageData]
			, [profile].[GeoLocation].Lat AS [Latitude]
			, [profile].[GeoLocation].Long AS [Longitude]
			, [profile].[Location]
			, [dbo].[SubstringFormat]([DisplayName], 50) AS [Title]
			, [dbo].[SubstringFormat]([Status], 100) AS [Description]
			, [profile].UserIdentifier
			, [profile].[CreatedOn]
		FROM [User].[vwProfile] [profile] WITH (NOLOCK)
		WHERE [profile].[PrivacyLevel] < 4

END