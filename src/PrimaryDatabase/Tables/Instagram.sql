CREATE TABLE [dbo].[Instagram] (
    [Id]                      INT            IDENTITY (1, 1) NOT NULL,
    [InstagramId]             NVARCHAR (300) NOT NULL,
    [TimeCreated]             DATETIME       NOT NULL,
    [TimeUnixCreated]         BIGINT         NOT NULL,
    [Username]                NVARCHAR (100) NOT NULL,
    [Url]                     NVARCHAR (300) NOT NULL,
    [ImageThumbnail]          NVARCHAR (300) NOT NULL,
    [ImageLowResolution]      NVARCHAR (300) NOT NULL,
    [ImageStandartResolution] NVARCHAR (300) NOT NULL,
    [Text]                    NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_Instagram] PRIMARY KEY CLUSTERED ([Id] ASC)
);

