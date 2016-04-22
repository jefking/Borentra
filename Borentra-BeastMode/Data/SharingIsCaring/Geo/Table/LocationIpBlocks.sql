CREATE TABLE [Geo].[LocationIpBlocks]
(
	[LocationId] [int] NOT NULL,
	[StartIp] [bigint] NOT NULL,
	[EndIp] [bigint] NOT NULL, 
    CONSTRAINT [PK_LocationIpBlocks] PRIMARY KEY ([LocationId], [EndIp], [StartIp]), 
    CONSTRAINT [FK_Geo_LocationIpBlocks_Geo_Location] FOREIGN KEY ([LocationId]) REFERENCES [Geo].[Location]([LocationId]),
)

GO

CREATE INDEX [IX_Geo_LocationIpBlocks_StartIp] ON [Geo].[LocationIpBlocks] ([StartIp])
GO
CREATE INDEX [IX_Geo_LocationIpBlocks_EndIp] ON [Geo].[LocationIpBlocks] ([EndIp])
