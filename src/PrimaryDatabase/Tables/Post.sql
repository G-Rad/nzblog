CREATE TABLE [dbo].[Post] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [Title]           NVARCHAR (200) NOT NULL,
    [Thumbnail]       NVARCHAR (300) NULL,
    [Body]            NVARCHAR (MAX) NULL,
    [Url]             NVARCHAR (200) NOT NULL,
    [DateCreated]     DATETIME       NOT NULL,
    [DatePublished]   DATETIME       NOT NULL,
    [IsPublished]     BIT            NULL,
    [MetaDescription] NVARCHAR (500) NULL,
    [MetaImageUrl]    NVARCHAR (500) NULL,
    CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED ([Id] ASC)
);

