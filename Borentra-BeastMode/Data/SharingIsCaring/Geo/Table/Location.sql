CREATE TABLE [Geo].[Location]
(
	[LocationId] [int] NOT NULL,
	[Country] [char](2) NOT NULL,
	[Region] [char](2) NULL,
	[City] [nvarchar](50) NULL,
	[PostalCode] [nvarchar](10) NULL,
	[MetroCode] [nvarchar](10) NULL,
	[AreaCode] [nvarchar](5) NULL,
	[GeoSpatial] [geography] NOT NULL, 
    CONSTRAINT [PK_Location] PRIMARY KEY ([LocationId]),
)

GO

CREATE SPATIAL INDEX [SPATIAL_Geo_Location_GeoSpatial] ON [Geo].[Location] ([GeoSpatial])
