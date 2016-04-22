CREATE PROCEDURE [Goods].[SearchItemRequestFulfill]
	@Identifier uniqueidentifier = NULL
	, @UserIdentifier uniqueidentifier = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SELECT [Fulfill].Identifier
		, [Fulfill].[ItemRequestIdentifier]
		, [Fulfill].[Status]
		, [Fulfill].WillRent
		, [Fulfill].WillGive
		, [Fulfill].WillShare
		, [Fulfill].WillTrade
		, [Request].[UserIdentifier] AS [OwnerIdentifier]
		, [Request].[Key] AS [Key]
		, [Request].[Title] AS [Title]
		, [Owner].[FacebookId] AS [OwnerFacebookId]
		, [Owner].[Key] AS [OwnerKey]
		, [Owner].[Location] AS [OwnerLocation]
		, [Owner].[DisplayName] AS [OwnerDisplayName]
		, [Requester].[FacebookId] AS [RequesterFacebookId]
		, [Requester].[Key] AS [RequesterKey]
		, [Requester].[DisplayName] AS [RequesterDisplayName]
		, [Requester].[Location] AS [RequesterLocation]
		, [Requester].[UserIdentifier] AS [RequesterIdentifier]
		, (CASE [Fulfill].[UserIdentifier] WHEN @UserIdentifier THEN 1 ELSE 0 END) AS [IsMine]
	FROM [Goods].[vwItemRequestFulfill] [Fulfill]
		INNER JOIN [Goods].[vwItemRequest] [Request] WITH (NOLOCK)
			ON [Request].[Identifier] = [Fulfill].ItemRequestIdentifier
				AND [Fulfill].Identifier = COALESCE(@Identifier, [Fulfill].Identifier)
				AND
					(
						[Fulfill].[UserIdentifier] = COALESCE(@UserIdentifier, [Fulfill].UserIdentifier)
					OR
						[Request].[UserIdentifier] = COALESCE(@UserIdentifier, [Request].UserIdentifier)
					)
		INNER JOIN [User].[vwProfile] [Owner] WITH (NOLOCK)
			ON [Request].[UserIdentifier] = [Owner].[UserIdentifier]
		INNER JOIN [User].[vwProfile] [Requester] WITH (NOLOCK)
			ON [Fulfill].[UserIdentifier] = [Requester].[UserIdentifier]

END