
CREATE FUNCTION [dbo].[MakeKey]
(
	@Value nvarchar(256) = NULL
)
RETURNS nvarchar(256)
BEGIN

	DECLARE @replaceChar char(1);
	SET @replaceChar = '-';
	
	SET @Value = [dbo].[TrimOrNull](@Value);
	
	SET @Value = REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(@Value, '*' , @replaceChar)
				, CHAR(10) , @replaceChar) -- CR
				, CHAR(13) , @replaceChar) -- LF
				, '!' , @replaceChar)
				, '''' , @replaceChar)
				, '<' , @replaceChar)
				, '>' , @replaceChar)
				, '%' , @replaceChar)
				, '(' , @replaceChar)
				, ')' , @replaceChar)
				, '@' , @replaceChar)
				, '^' , @replaceChar)
				, '`' , @replaceChar)
				, '~' , @replaceChar)
				, '|' , @replaceChar)
				, '{' , @replaceChar)
				, '}' , @replaceChar)
				, '[' , @replaceChar)
				, ']' , @replaceChar)
				, '=' , @replaceChar)
				, ';' , @replaceChar)
				, '\' , @replaceChar)
				, ',' , @replaceChar)
				, '$' , @replaceChar)
				, '?' , @replaceChar)
				, '+' , @replaceChar)
				, '#' , @replaceChar)
				, ' ' , @replaceChar)
				, ':', @replaceChar)
				, '.', @replaceChar)
				, '&', @replaceChar)
				, '/', @replaceChar)
				, '"', @replaceChar);
				
		WHILE @Value LIKE '%--%'
		BEGIN

			SET @Value = REPLACE(@Value, '--', @replaceChar);

		END

		WHILE @Value LIKE '%-'
		BEGIN

			SET @Value = LEFT(@Value, LEN(@Value) - 1)

		END

		RETURN LOWER(@Value);
END