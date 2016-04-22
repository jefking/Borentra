CREATE PROCEDURE [Stats].[LatestItems]
	@Days tinyint = 7
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Days = [dbo].[EnsureSetTinyInt](@Days, 7);

	CREATE TABLE #Dates
	(
		[Date] smalldatetime
	)

	DECLARE @CurrentDate smalldatetime
	SET @CurrentDate = DATEADD(DAY, @Days * -1, GETUTCDATE())

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

		SET @CurrentDate = DATEADD(DAY, 1, @CurrentDate)

	END

	SELECT
		(SELECT COUNT(1)
			FROM [Goods].[vwItem] [item] with(NOLOCK)
			WHERE DATEPART(DAYOFYEAR, [Date]) = DATEPART(DAYOFYEAR, [CreatedOn])
					AND DATEPART(YEAR, [Date]) = DATEPART(YEAR, [CreatedOn])
				) AS [OfferCount]
		, 
			(SELECT COUNT(1)
				FROM [Goods].[vwItemRequest] [request] with(NOLOCK)
				WHERE DATEPART(DAYOFYEAR, [Date]) = DATEPART(DAYOFYEAR, [CreatedOn])
					AND DATEPART(YEAR, [Date]) = DATEPART(YEAR, [CreatedOn])
				) AS [RequestCount]
		, CONVERT(varchar(4), DATEPART(year, [Date]))
			+ '-'
			+ CONVERT(varchar(2), DATEPART(month, [Date]))
			+ '-'
			+ CONVERT(varchar(2), DATEPART(day, [Date]))
			 AS CreateDate
	FROM #Dates
	ORDER BY [Date] DESC

	CLEANUP:
		DROP TABLE #Dates

END