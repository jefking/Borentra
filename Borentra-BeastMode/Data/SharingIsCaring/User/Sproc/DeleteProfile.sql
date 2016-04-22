CREATE PROCEDURE [User].[DeleteProfile]
	@UserIdentifier [uniqueidentifier] = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	EXECUTE [User].[SaveProfile]
		@UserIdentifier = @UserIdentifier,
		@Delete = 1

END