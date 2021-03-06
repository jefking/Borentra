﻿CREATE PROCEDURE [Stats].[ItemsByMonth]
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	CREATE TABLE #Dates
	(
		[Date] smalldatetime
	)

	DECLARE @CurrentDate smalldatetime
	SET @CurrentDate = CAST('2013-01-01' AS smalldatetime)

	WHILE @CurrentDate <= GETUTCDATE()
	BEGIN

		INSERT INTO #Dates
		(
			[Date]
		)
		VALUES
		(
			@CurrentDate
		)

		SET @CurrentDate = DATEADD(MONTH, 1, @CurrentDate)

	END

	SELECT
			(SELECT COUNT(1)
				FROM [Goods].[vwItem] [item] with(NOLOCK)
				WHERE DATEPART(MONTH, [Date]) = DATEPART(MONTH, [CreatedOn])
					AND DATEPART(YEAR, [Date]) = DATEPART(YEAR, [CreatedOn])
				) AS [OfferCount]
		, 
			(SELECT COUNT(1)
				FROM [Goods].[vwItemRequest] [request] with(NOLOCK)
				WHERE DATEPART(MONTH, [Date]) = DATEPART(MONTH, [CreatedOn])
					AND DATEPART(YEAR, [Date]) = DATEPART(YEAR, [CreatedOn])
				) AS [RequestCount]
		, CONVERT(varchar(4), DATEPART(year, [Date]))
			+ '-'
			+ CONVERT(varchar(2), DATEPART(month, [Date]))
			+ '-'
			+ CONVERT(varchar(2), DATEPART(day, [Date]))
			 AS CreateDate
	FROM #Dates
	ORDER BY [Date] ASC

	CLEANUP:
		DROP TABLE #Dates

END