﻿CREATE TABLE [Device].[Device]
(
	[Identifier] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
    [UserIdentifier] UNIQUEIDENTIFIER NOT NULL, 
    [DeviceIdentifier] UNIQUEIDENTIFIER NOT NULL,
    [FacebookAccessToken]     NVARCHAR (512)   DEFAULT ('') NOT NULL,
    [FacebookTokenExpiration] DATETIME         DEFAULT (GETUTCDATE()) NOT NULL,
    [FacebookIsValidated] BIT NOT NULL DEFAULT 0, 
    [OS] TINYINT NOT NULL DEFAULT 1, 
    [IpAddress] NVARCHAR(50) NULL, 
	[Amplitude] INT NOT NULL,
	[VerticalOffset] INT NOT NULL,
	[AngularFrequency] INT NOT NULL,
	[PhaseShift] INT NOT NULL,
    [KeyExpiresOn]               DATETIME2    DEFAULT (GETUTCDATE()) NOT NULL,
    [LastValidatedOn] DATETIME2 NOT NULL DEFAULT (GETUTCDATE()), 
    [CreatedOn]               DATETIME    DEFAULT (GETUTCDATE()) NOT NULL,
    [ModifiedOn]              DATETIME    DEFAULT (GETUTCDATE()) NOT NULL,
    [DeletedOn]               SMALLDATETIME    DEFAULT (NULL) NULL, 
    CONSTRAINT [FK_Device_User_Profile] FOREIGN KEY ([UserIdentifier]) REFERENCES [User].[Profile]([UserIdentifier]),
)