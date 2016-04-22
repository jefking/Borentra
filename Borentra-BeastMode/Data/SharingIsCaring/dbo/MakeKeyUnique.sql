CREATE FUNCTION [dbo].[MakeKeyUnique]
(
	@Value nvarchar(286) = NULL
	, @Unique uniqueidentifier
)
RETURNS nvarchar(286)
BEGIN

	RETURN [dbo].[MakeKey](@Value + '-' + LOWER(SUBSTRING(CAST(@Unique AS [nvarchar](36)), 0, 8)));

END