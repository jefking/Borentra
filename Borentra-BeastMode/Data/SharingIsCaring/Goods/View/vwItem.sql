﻿CREATE VIEW [Goods].[vwItem]
AS
	SELECT [Identifier]
		, [UserIdentifier]
		, [Title]
		, [Description]
		, [CreatedOn]
		, [ModifiedOn]
		, [Key]
		, [SharePrivacyLevel]
		, [TradePrivacyLevel]
		, [RentPrivacyLevel]
		, [FreePrivacyLevel]
		, [MinimumPrivacyLevel]
	FROM [Goods].[Item]
	WHERE [DeletedOn] IS NULL