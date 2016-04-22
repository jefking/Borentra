CREATE VIEW [Test].[vwSignUp]
AS
	SELECT [Email]
      , [Name]
      , [UserIdentifier]
      , [Type]
      , [CreatedOn]
	FROM [Test].[SignUp]
	WHERE [DeletedOn] IS NULL
