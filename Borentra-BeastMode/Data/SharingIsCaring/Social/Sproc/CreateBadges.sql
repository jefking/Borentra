CREATE PROCEDURE [Social].[CreateBadges]
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	EXECUTE [Social].[CreateBadge]
		@Identifier = 1,
		@Title = 'Premium',
		@Description = 'When a members account status is premium.',
		@Points = 1,
		@IconName = 'premium.png'
		
	EXECUTE [Social].[CreateBadge]
		@Identifier = 2,
		@Title = 'Lender',
		@Description = 'Member has lent out an item.',
		@Points = 6,
		@IconName = 'lender.png'

	EXECUTE [Social].[CreateBadge]
		@Identifier = 3,
		@Title = 'Borrower',
		@Description = 'Member has borrowed an item.',
		@Points = 4,
		@IconName = 'borrower.png'

	EXECUTE [Social].[CreateBadge]
		@Identifier = 4,
		@Title = 'Founder',
		@Description = 'Member joined in the first 1000 users.',
		@Points = 1,
		@IconName = 'founder.png'

	EXECUTE [Social].[CreateBadge]
		@Identifier = 5,
		@Title = 'Conversation Starter',
		@Description = 'Member has started a conversation.',
		@Points = 1,
		@IconName = 'conversationstarter.png'

	EXECUTE [Social].[CreateBadge]
		@Identifier = 6,
		@Title = 'Ambassador',
		@Description = 'Member has been exceptional and has helped us grow.',
		@Points = 10,
		@IconName = 'ambassador.png'

	EXECUTE [Social].[CreateBadge]
		@Identifier = 7,
		@Title = 'Friendly',
		@Description = 'Member has friends here.',
		@Points = 4,
		@IconName = 'friendly.png'

	EXECUTE [Social].[CreateBadge]
		@Identifier = 8,
		@Title = 'Trader',
		@Description = 'Member has traded an offer.',
		@Points = 5,
		@IconName = 'trader.png'

	EXECUTE [Social].[CreateBadge]
		@Identifier = 9,
		@Title = 'Giver',
		@Description = 'Member has given an offer.',
		@Points = 6,
		@IconName = 'giver.png'

	EXECUTE [Social].[CreateBadge]
		@Identifier = 10,
		@Title = 'Reciever',
		@Description = 'Member has recieved an offer.',
		@Points = 1,
		@IconName = 'reciever.png'

	EXECUTE [Social].[CreateBadge]
		@Identifier = 11,
		@Title = 'Builder',
		@Description = 'Member has asked a freind to join!',
		@Points = 2,
		@IconName = 'communitybuilder.png'
		
	EXECUTE [Social].[CreateBadge]
		@Identifier = 12,
		@Title = 'Renter',
		@Description = 'Member has rented from someone.',
		@Points = 3,
		@IconName = 'renter.png'

	EXECUTE [Social].[CreateBadge]
		@Identifier = 13,
		@Title = 'Proprietor',
		@Description = 'Member has rented to someone.',
		@Points = 4,
		@IconName = 'ownerrenter.png'

	EXECUTE [Social].[CreateBadge]
		@Identifier = 14,
		@Title = 'Multiple Offers',
		@Description = 'Member has 10 offers or more.',
		@Points = 3,
		@IconName = 'manyoffers.png'

	EXECUTE [Social].[CreateBadge]
		@Identifier = 15,
		@Title = 'Multiple Wants',
		@Description = 'Member has 10 wants or more.',
		@Points = 2,
		@IconName = 'manywants.png'

END