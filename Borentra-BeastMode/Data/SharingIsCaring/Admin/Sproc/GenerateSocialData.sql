CREATE PROCEDURE [Admin].[GenerateSocialData]
	@MinutesAgo smallint = 20
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @MinutesAgo = [dbo].[EnsureSetSmallInt](@MinutesAgo, 20);

	IF 0 >= @MinutesAgo
	BEGIN

		SET @MinutesAgo = 20;

	END

	SET @MinutesAgo = @MinutesAgo * -1;

	EXECUTE [Admin].[AddToProfileStats];
	
	CREATE TABLE #Stats
	(
		UserIdentifier uniqueidentifier
		, FriendCount smallint DEFAULT 0
		, MembersNearby int DEFAULT 0
		, FriendsOffersCount smallint DEFAULT 0
	)

	INSERT INTO #Stats
	(
		UserIdentifier
		, FriendCount
		, MembersNearby
		, FriendsOffersCount
	)
	SELECT [profile].[UserIdentifier]
		, FriendCount = (SELECT COUNT(0)
						FROM [Social].[vwNetwork] [network] WITH(NOLOCK)
							INNER JOIN [User].[vwProfile] [friend] WITH(NOLOCK)
								ON [network].[ConnectionIdentifier] = [friend].[UserIdentifier]
									AND [friend].[PrivacyLevel] < 4
									AND OwnerIdentifier = [profileStats].[UserIdentifier])
		, MembersNearby = (SELECT COUNT(*)
							FROM [User].[vwProfile] [near] WITH(NOLOCK)
							WHERE [near].[PrivacyLevel] < 3
								AND [profile].[SearchRadius] >= [profile].GeoLocation.STDistance([near].[GeoLocation])
								AND [profile].[UserIdentifier] <> [near].[UserIdentifier])
		, (SELECT COUNT(0)
			FROM [Goods].[vwItem] [item] WITH(NOLOCK)
				INNER JOIN [Social].[vwNetwork] [network] WITH (NOLOCK)
					ON [item].[UserIdentifier] = [network].[ConnectionIdentifier]
						AND [network].[OwnerIdentifier] = [profile].[UserIdentifier])
	FROM [User].[ProfileStats] [profileStats] WITH(NOLOCK)
		INNER JOIN [User].[vwProfile] [profile] WITH(NOLOCK)
			ON [profile].[UserIdentifier] = [profileStats].[UserIdentifier]
				AND
				(
					[profile].[ModifiedOn] > DATEADD(MINUTE, @MinutesAgo, GETUTCDATE())
				);
			
	UPDATE [profileStats]
	SET ModifiedOn = GETUTCDATE()
		, [FriendCount] = [stats].[FriendCount]
		, [MembersNearby] = [stats].[MembersNearby]
		, [FriendsOffersCount] = [stats].[FriendsOffersCount]
	FROM [User].[ProfileStats] [profileStats]
		INNER JOIN #Stats [stats] WITH(NOLOCK)
			ON [stats].[UserIdentifier] = [profileStats].[UserIdentifier]
				AND
				(
					[profileStats].[FriendCount] <> [stats].[FriendCount]
					OR
					[profileStats].[MembersNearby] <> [stats].[MembersNearby]
					OR
					[profileStats].[FriendsOffersCount] <> [stats].[FriendsOffersCount]
				)
				
	CLEANUP:
		DROP TABLE #Stats;

END