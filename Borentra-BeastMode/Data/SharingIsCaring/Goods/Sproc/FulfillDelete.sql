CREATE PROCEDURE [Goods].[FulfillDelete]
	@Identifier uniqueidentifier = NULL
	, @UserIdentifier uniqueidentifier = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	EXECUTE [Goods].[SaveItemRequestFulfill]
		@Identifier = @Identifier
		, @UserIdentifier = @UserIdentifier
		, @Delete = 1

END