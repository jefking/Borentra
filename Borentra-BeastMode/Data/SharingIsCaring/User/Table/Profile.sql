CREATE TABLE [User].[Profile] (
    [UserIdentifier]          UNIQUEIDENTIFIER NOT NULL,
    [DisplayName]             NVARCHAR (256)   NOT NULL,
    [Status]                  NVARCHAR (512)   NULL,
    [CreatedOn]               DATETIME    DEFAULT (GETUTCDATE()) NOT NULL,
    [ModifiedOn]              DATETIME    DEFAULT (GETUTCDATE()) NOT NULL,
    [DeletedOn]               SMALLDATETIME    DEFAULT (NULL) NULL,
    [IdentityProvider]        NVARCHAR (256)   DEFAULT ('Facebook') NOT NULL,
    [FacebookAccessToken]     NVARCHAR (512)   DEFAULT ('') NOT NULL,
    [FacebookTokenExpiration] DATETIME         DEFAULT (GETUTCDATE()) NOT NULL,
    [Key] NVARCHAR(286) NULL, 
    [Location] NVARCHAR(128) NULL, 
    [FacebookId] BIGINT NULL, 
    [Email] NVARCHAR(1024) NULL, 
    [PrivacyLevel] TINYINT NOT NULL DEFAULT 1, 
    [GeoLocation] [sys].[geography] NULL, 
    [SearchRadius] INT NOT NULL DEFAULT 250000, 
    [IpAddress] NVARCHAR(50) NULL, 
    CONSTRAINT [PK_User_Profile] PRIMARY KEY CLUSTERED ([UserIdentifier] ASC),
    CONSTRAINT [AK_Profile_Key] UNIQUE ([Key])
);


GO
