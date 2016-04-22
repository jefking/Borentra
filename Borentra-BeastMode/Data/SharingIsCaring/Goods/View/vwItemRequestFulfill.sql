CREATE VIEW [Goods].[vwItemRequestFulfill]
AS
	SELECT Identifier
		, ItemRequestIdentifier
		, UserIdentifier
		, [Status]
		, WillRent
		, WillGive
		, WillShare
		, WillTrade
	FROM [Goods].[ItemRequestFulfill]
	WHERE DeletedOn IS NULL