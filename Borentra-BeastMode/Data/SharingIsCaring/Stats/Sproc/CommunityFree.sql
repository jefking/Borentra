﻿CREATE PROCEDURE [Stats].[CommunityFree]
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SELECT [Free].[Identifier]
		, [Free].[ItemIdentifier]
		, [Free].[Status]
		, [Free].[On]
		, [Item].[UserIdentifier] AS [OwnerIdentifier]
		, [Item].[Key] AS [ItemKey]
		, [Item].[Title] AS [ItemTitle]
		, [Owner].[FacebookId] AS [OwnerFacebookId]
		, [Owner].[Key] AS [OwnerKey]
		, [Owner].[Location] AS [OwnerLocation]
		, [Owner].[DisplayName] AS [OwnerDisplayName]
		, [Requester].[FacebookId] AS [RequesterFacebookId]
		, [Requester].[Key] AS [RequesterKey]
		, [Requester].[DisplayName] AS [RequesterDisplayName]
		, [Requester].[Location] AS [RequesterLocation]
		, [Requester].[UserIdentifier] AS [RequesterUserIdentifier]
		, (SELECT TOP 1 [Path]
			FROM [Goods].[vwItemImage] WITH(NOLOCK)
			WHERE [ItemIdentifier] = [Item].Identifier
			ORDER BY IsPrimary DESC) AS [PrimaryImagePathFormat]
	FROM [Goods].[vwItemFree] [Free] WITH(NOLOCK)
		INNER JOIN [Goods].[vwItem] [Item] WITH (NOLOCK)
			ON [Free].[ItemIdentifier] = [Item].[Identifier]
		INNER JOIN [User].[vwProfile] [Owner] WITH (NOLOCK)
			ON [Item].[UserIdentifier] = [Owner].[UserIdentifier]
		INNER JOIN [User].[vwProfile] [Requester] WITH (NOLOCK)
			ON [Free].[UserIdentifier] = [Requester].[UserIdentifier]
	ORDER BY [Free].[On] DESC

END