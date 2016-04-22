CREATE TABLE [Geo].[Country]
(
	[IsoCode] CHAR(2) NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(200) NOT NULL, 
    [Position] [sys].[geography] NOT NULL, 
    [IsInHeatMap] BIT NOT NULL DEFAULT 0
)

GO

CREATE SPATIAL INDEX [SPATIAL_Country_Position] ON [Geo].[Country] ([Position])
