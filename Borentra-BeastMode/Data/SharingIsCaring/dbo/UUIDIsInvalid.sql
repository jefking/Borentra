CREATE FUNCTION [dbo].[UUIDIsInvalid]
(
	@Id uniqueidentifier = NULL
)
RETURNS BIT
BEGIN

	RETURN CASE WHEN @Id IS NULL OR @Id = CAST(CAST(0 AS binary) AS uniqueidentifier) THEN 1 ELSE 0 END;

END