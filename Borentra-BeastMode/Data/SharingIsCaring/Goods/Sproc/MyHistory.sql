CREATE PROCEDURE [Goods].[MyHistory]
	@UserIdentifier [uniqueidentifier] = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
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

			-- Given Free
			SELECT [Free].[Identifier]
				, [Free].[ItemIdentifier]
				, [Item].[Key] AS [ItemKey]
				, [Item].[Title] AS [ItemTitle]
				, [Free].[Status]
				, [Owner].[FacebookId] AS [OwnerFacebookId]
				, [Owner].[Key] AS [OwnerKey]
				, [Owner].[DisplayName] AS [OwnerDisplayName]
				, [Requester].[FacebookId] AS [RequesterFacebookId]
				, [Requester].[Key] AS [RequesterKey]
				, [Requester].[DisplayName] AS [RequesterDisplayName]
				, (SELECT TOP 1 [Path]
					FROM [Goods].[vwItemImage] WITH(NOLOCK)
					WHERE [ItemIdentifier] = [Item].Identifier
					ORDER BY IsPrimary DESC) AS [PrimaryImagePathFormat]
				, [Free].[On]
				, 1 AS [Type]
			FROM [Goods].[vwItemFree] [Free] WITH(NOLOCK)
				INNER JOIN [Goods].[vwItem] [Item] WITH (NOLOCK)
					ON [Free].[ItemIdentifier] = [Item].[Identifier]
						AND [Item].[UserIdentifier] = @UserIdentifier
						AND [Free].[Status] < 1
				INNER JOIN [User].[vwProfile] [Owner] WITH (NOLOCK)
					ON [Item].[UserIdentifier] = [Owner].[UserIdentifier]
				INNER JOIN [User].[vwProfile] [Requester] WITH (NOLOCK)
					ON [Free].[UserIdentifier] = [Requester].[UserIdentifier]
		UNION
			-- Recieved Free
			SELECT [Free].[Identifier]
				, [Free].[ItemIdentifier]
				, [Item].[Key] AS [ItemKey]
				, [Item].[Title] AS [ItemTitle]
				, [Free].[Status]
				, [Owner].[FacebookId] AS [OwnerFacebookId]
				, [Owner].[Key] AS [OwnerKey]
				, [Owner].[DisplayName] AS [OwnerDisplayName]
				, [Requester].[FacebookId] AS [RequesterFacebookId]
				, [Requester].[Key] AS [RequesterKey]
				, [Requester].[DisplayName] AS [RequesterDisplayName]
				, (SELECT TOP 1 [Path]
					FROM [Goods].[vwItemImage] WITH(NOLOCK)
					WHERE [ItemIdentifier] = [Item].Identifier
					ORDER BY IsPrimary DESC) AS [PrimaryImagePathFormat]
				, [Free].[On]
				, 1 AS [Type]
			FROM [Goods].[vwItemFree] [Free] WITH(NOLOCK)
				INNER JOIN [Goods].[vwItem] [Item] WITH (NOLOCK)
					ON [Free].[ItemIdentifier] = [Item].[Identifier]
						AND [Free].[UserIdentifier] = @UserIdentifier
						AND [Free].[Status] < 1
				INNER JOIN [User].[vwProfile] [Owner] WITH (NOLOCK)
					ON [Item].[UserIdentifier] = [Owner].[UserIdentifier]
				INNER JOIN [User].[vwProfile] [Requester] WITH (NOLOCK)
					ON [Free].[UserIdentifier] = [Requester].[UserIdentifier]
		UNION
			-- Lender
			SELECT [Share].[Identifier]
				, [Share].[ItemIdentifier]
				, [Item].[Key] AS [ItemKey]
				, [Item].[Title] AS [ItemTitle]
				, [Share].[Status]
				, [Owner].[FacebookId] AS [OwnerFacebookId]
				, [Owner].[Key] AS [OwnerKey]
				, [Owner].[DisplayName] AS [OwnerDisplayName]
				, [Requester].[FacebookId] AS [RequesterFacebookId]
				, [Requester].[Key] AS [RequesterKey]
				, [Requester].[DisplayName] AS [RequesterDisplayName]
				, (SELECT TOP 1 [Path]
					FROM [Goods].[vwItemImage] WITH(NOLOCK)
					WHERE [ItemIdentifier] = [Item].Identifier
					ORDER BY IsPrimary DESC) AS [PrimaryImagePathFormat]
				, [Share].[On]
				, 2 AS [Type]
				--, [Share].[Until]
				--, [Share].[ReturnedOn]
			FROM [Goods].[vwItemShare] [Share] WITH (NOLOCK)
				INNER JOIN [Goods].[vwItem] [Item] WITH (NOLOCK)
					ON [Share].[ItemIdentifier] = [Item].[Identifier]
						AND [Item].[UserIdentifier] = @UserIdentifier
				INNER JOIN [User].[vwProfile] [Owner] WITH (NOLOCK)
					ON [Item].[UserIdentifier] = [Owner].[UserIdentifier]
				INNER JOIN [User].[vwProfile] [Requester] WITH (NOLOCK)
					ON [Share].[UserIdentifier] = [Requester].[UserIdentifier]
		UNION
			-- Borrower
			SELECT [Share].[Identifier]
				, [Share].[ItemIdentifier]
				, [Item].[Key] AS [ItemKey]
				, [Item].[Title] AS [ItemTitle]
				, [Share].[Status]
				, [Owner].[FacebookId] AS [OwnerFacebookId]
				, [Owner].[Key] AS [OwnerKey]
				, [Owner].[DisplayName] AS [OwnerDisplayName]
				, [Requester].[FacebookId] AS [RequesterFacebookId]
				, [Requester].[Key] AS [RequesterKey]
				, [Requester].[DisplayName] AS [RequesterDisplayName]
				, (SELECT TOP 1 [Path]
					FROM [Goods].[vwItemImage] WITH(NOLOCK)
					WHERE [ItemIdentifier] = [Item].Identifier
					ORDER BY IsPrimary DESC) AS [PrimaryImagePathFormat]
				, [Share].[On]
				, 2 AS [Type]
				--, [Share].[Until]
				--, [Share].[ReturnedOn]
			FROM [Goods].[vwItemShare] [Share] WITH (NOLOCK)
				INNER JOIN [Goods].[vwItem] [Item] WITH (NOLOCK)
					ON [Share].[ItemIdentifier] = [Item].[Identifier]
						AND [Share].[UserIdentifier] = @UserIdentifier
				INNER JOIN [User].[vwProfile] [Owner] WITH (NOLOCK)
					ON [Item].[UserIdentifier] = [Owner].[UserIdentifier]
				INNER JOIN [User].[vwProfile] [Requester] WITH (NOLOCK)
					ON [Share].[UserIdentifier] = [Requester].[UserIdentifier]
		UNION
			-- Rented
			SELECT [rent].[Identifier]
				, [rent].[ItemIdentifier]
				, [Item].[Key] AS [ItemKey]
				, [Item].[Title] AS [ItemTitle]
				, [rent].[Status]
				, [Owner].[FacebookId] AS [OwnerFacebookId]
				, [Owner].[Key] AS [OwnerKey]
				, [Owner].[DisplayName] AS [OwnerDisplayName]
				, [Requester].[FacebookId] AS [RequesterFacebookId]
				, [Requester].[Key] AS [RequesterKey]
				, [Requester].[DisplayName] AS [RequesterDisplayName]
				, (SELECT TOP 1 [Path]
					FROM [Goods].[vwItemImage] WITH(NOLOCK)
					WHERE [ItemIdentifier] = [Item].Identifier
					ORDER BY IsPrimary DESC) AS [PrimaryImagePathFormat]
				, [rent].[On]
				, 4 AS [Type]
				--, [rent].[Until]
				--, [rent].[ReturnedOn]
				--, [rent].[Price]
			FROM [Goods].[vwItemRenting] [rent] WITH (NOLOCK)
				INNER JOIN [Goods].[vwItem] [Item] WITH (NOLOCK)
					ON [rent].[ItemIdentifier] = [Item].[Identifier]
						AND [rent].[UserIdentifier] = @UserIdentifier
				INNER JOIN [User].[vwProfile] [Owner] WITH (NOLOCK)
					ON [Item].[UserIdentifier] = [Owner].[UserIdentifier]
				INNER JOIN [User].[vwProfile] [Requester] WITH (NOLOCK)
					ON [rent].[UserIdentifier] = [Requester].[UserIdentifier]
		UNION
			-- Rented Out
			SELECT [rent].[Identifier]
				, [rent].[ItemIdentifier]
				, [Item].[Key] AS [ItemKey]
				, [Item].[Title] AS [ItemTitle]
				, [rent].[Status]
				, [Owner].[FacebookId] AS [OwnerFacebookId]
				, [Owner].[Key] AS [OwnerKey]
				, [Owner].[DisplayName] AS [OwnerDisplayName]
				, [Requester].[FacebookId] AS [RequesterFacebookId]
				, [Requester].[Key] AS [RequesterKey]
				, [Requester].[DisplayName] AS [RequesterDisplayName]
				, (SELECT TOP 1 [Path]
					FROM [Goods].[vwItemImage] WITH(NOLOCK)
					WHERE [ItemIdentifier] = [Item].Identifier
					ORDER BY IsPrimary DESC) AS [PrimaryImagePathFormat]
				, [rent].[On]
				, 4 AS [Type]
				--, [rent].[Until]
				--, [rent].[ReturnedOn]
				--, [rent].[Price]
			FROM [Goods].[vwItemRenting] [rent] WITH (NOLOCK)
				INNER JOIN [Goods].[vwItem] [Item] WITH (NOLOCK)
					ON [rent].[ItemIdentifier] = [Item].[Identifier]
						AND [Item].[UserIdentifier] = @UserIdentifier
				INNER JOIN [User].[vwProfile] [Owner] WITH (NOLOCK)
					ON [Item].[UserIdentifier] = [Owner].[UserIdentifier]
				INNER JOIN [User].[vwProfile] [Requester] WITH (NOLOCK)
					ON [rent].[UserIdentifier] = [Requester].[UserIdentifier]
		UNION
			SELECT [Trade].[Identifier]
				, [Item].[Identifier] AS [ItemIdentifier]
				, [Item].[Title] AS [ItemTitle]
				, [Item].[Key] AS [ItemKey]
				, 0 AS [Status]
				, [Requester].[FacebookId] AS [RequesterFacebookId]
				, [Requester].[Key] AS [RequesterKey]
				, [Requester].[DisplayName] AS [RequesterName]
				, [Receiver].[FacebookId] AS [ReceiverFacebookId]
				, [Receiver].[DisplayName] AS [ReceiverName]
				, [Receiver].[Key] AS [ReceiverKey]
				, (SELECT TOP 1 [Path]
					FROM [Goods].[vwItemImage] WITH(NOLOCK)
					WHERE [ItemIdentifier] = [Item].Identifier
					ORDER BY IsPrimary DESC) AS [PrimaryImagePathFormat]
				, [Trade].[AcceptedOn] AS [On]
				, 3 AS [Type]
				--, [Trade].[CreatedOn] AS [CreatedOn]
				--, [Trade].[RejectedOn] AS [RejectedOn]
			FROM [Goods].[vwTrade] [Trade] WITH (NOLOCK)
				INNER JOIN [Goods].[vwItemTrade] [ItemTrade] WITH (NOLOCK)
					ON [Trade].[Identifier] = [ItemTrade].[TradeIdentifier]
					AND
					(
						[Trade].[RejectedOn] IS NOT NULL
						OR
						[Trade].[AcceptedOn] IS NOT NULL
					)
				INNER JOIN [Goods].[vwItem] [Item] WITH (NOLOCK)
					ON [Item].[Identifier] = [ItemTrade].[ItemIdentifier]
				INNER JOIN [User].[vwProfile] [Requester] WITH (NOLOCK)
					ON [Requester].[UserIdentifier] = [Trade].[UserIdentifier1]
						AND [Requester].[UserIdentifier] = @UserIdentifier
				INNER JOIN [User].[vwProfile] [Receiver] WITH (NOLOCK)
					ON [Receiver].[UserIdentifier] = [Trade].[UserIdentifier2]
		UNION 
			SELECT [Trade].[Identifier] AS [TradeIdentifier]
				, [Item].[Identifier] AS [ItemIdentifier]
				, [Item].[Title] AS [ItemTitle]
				, [Item].[Key] AS [ItemKey]
				, 0 AS [Status]
				, [Requester].[FacebookId] AS [RequesterFacebookId]
				, [Requester].[Key] AS [RequesterKey]
				, [Requester].[DisplayName] AS [RequesterName]
				, [Receiver].[FacebookId] AS [ReceiverFacebookId]
				, [Receiver].[Key] AS [ReceiverKey]
				, [Receiver].[DisplayName] AS [ReceiverName]
				, (SELECT TOP 1 [Path]
					FROM [Goods].[vwItemImage] WITH(NOLOCK)
					WHERE [ItemIdentifier] = [Item].Identifier
					ORDER BY IsPrimary DESC) AS [PrimaryImagePathFormat]
				, [Trade].[AcceptedOn] AS [On]
				, 3 AS [Type]
				--, [Trade].[CreatedOn] AS [CreatedOn]
				--, [Trade].[RejectedOn] AS [RejectedOn]
			FROM [Goods].[vwTrade] [Trade] WITH (NOLOCK)
				INNER JOIN [Goods].[vwItemTrade] [ItemTrade] WITH (NOLOCK)
					ON [Trade].[Identifier] = [ItemTrade].[TradeIdentifier]
					AND
					(
						[Trade].[RejectedOn] IS NOT NULL
						OR
						[Trade].[AcceptedOn] IS NOT NULL
					)
				INNER JOIN [Goods].[vwItem] [Item] WITH (NOLOCK)
					ON [Item].[Identifier] = [ItemTrade].[ItemIdentifier]
				INNER JOIN [User].[vwProfile] [Requester] WITH (NOLOCK)
					ON [Requester].[UserIdentifier] = [Trade].[UserIdentifier1]
				INNER JOIN [User].[vwProfile] [Receiver] WITH (NOLOCK)
					ON [Receiver].[UserIdentifier] = [Trade].[UserIdentifier2]
						AND [Receiver].[UserIdentifier] = @UserIdentifier

	END
END