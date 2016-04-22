CREATE TABLE [Geo].[City]
(
	[GeoNameId] BIGINT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(200) NOT NULL, 
    [Position] [sys].[geography] NOT NULL
)

GO

CREATE SPATIAL INDEX [SPATIAL_City_Position] ON [Geo].[City] ([Position])
