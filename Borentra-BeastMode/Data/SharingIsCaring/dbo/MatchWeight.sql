CREATE FUNCTION [dbo].[MatchWeight]
(
	@Original nvarchar(MAX) = NULL
	, @Related nvarchar(MAX) = NULL
	, @Scale tinyint = 1
)
RETURNS TINYINT
BEGIN

	DECLARE @Weight tinyint;
	
	SET @Weight = 0;

	IF @Original IS NOT NULL
		AND @Related IS NOT NULL
	BEGIN
	
		IF @Original = @Related
		BEGIN
	
			SET @Weight = @Scale;

		END
		ELSE
		BEGIN

			DECLARE @OriginalTrim nvarchar(MAX);
			DECLARE @RelatedTrim nvarchar(MAX);

			SET @OriginalTrim = dbo.TrimOrNull(@Original);
			SET @RelatedTrim = dbo.TrimOrNull(@Related);
	
			IF @OriginalTrim IS NOT NULL
				AND @RelatedTrim IS NOT NULL
			BEGIN

				IF LOWER(@Original) = LOWER(@Related)
				BEGIN

					SET @Weight = CAST(ROUND(@Scale * .9, 0) AS TINYINT);

				END
				ELSE IF @OriginalTrim = @RelatedTrim
				BEGIN

					SET @Weight = CAST(ROUND(@Scale * .8, 0) AS TINYINT);

				END
				ELSE IF @Original LIKE '%' + @Related
					OR @Original LIKE @Related + '%'
				BEGIN
	
					SET @Weight = CAST(ROUND(@Scale * .7, 0) AS TINYINT);

				END
				ELSE IF @OriginalTrim LIKE '%' + @RelatedTrim
					OR @OriginalTrim LIKE @RelatedTrim + '%'
				BEGIN
	
					SET @Weight = CAST(ROUND(@Scale * .6, 0) AS TINYINT);

				END
				ELSE IF @Original LIKE '%' + @Related + '%'
				BEGIN
	
					SET @Weight = CAST(ROUND(@Scale * .5, 0) AS TINYINT);

				END
				ELSE IF @OriginalTrim LIKE '%' + @RelatedTrim + '%'
				BEGIN
	
					SET @Weight = CAST(ROUND(@Scale * .4, 0) AS TINYINT);

				END

			END

		END

	END
	
	RETURN @Weight;

END