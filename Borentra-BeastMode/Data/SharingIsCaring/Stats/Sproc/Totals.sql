CREATE PROCEDURE [Stats].[Totals]
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SELECT
		(SELECT COUNT(*)
			FROM [User].[vwProfile] WITH(NOLOCK)) AS [Members]
		, (SELECT COUNT(*)
			FROM [Device].[vwDevice] WITH(NOLOCK)) AS [Devices]
		, (SELECT COUNT(*)
			FROM [Goods].[vwItem] WITH(NOLOCK)) AS [Offers]
		, (SELECT COUNT(*)
			FROM [Goods].[vwItemRequest] WITH(NOLOCK)) AS [Requests]
		, (SELECT COUNT(*) 
		  FROM (SELECT DISTINCT [UserIdentifier], [OS]
				FROM [Device].[vwDevice] WITH (NOLOCK)) AS [Data]) AS [MembersOnDevices]

END