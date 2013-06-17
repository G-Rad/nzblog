CREATE TABLE [dbo].[Flick] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [FlickrId] NVARCHAR (300) NOT NULL,
    [Title]    NVARCHAR (300) NOT NULL,
    [FarmId]   NVARCHAR (100) NOT NULL,
    [ServerId] NVARCHAR (100) NOT NULL,
    [Secret]   NVARCHAR (100) NOT NULL,
    [Description] NVARCHAR(1000) NULL, 
    [Tags] NVARCHAR(500) NULL, 
    CONSTRAINT [PK_Flick] PRIMARY KEY CLUSTERED ([Id] ASC)
);

