CREATE TABLE [dbo].[Setting] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [SettingsKey] NVARCHAR (200) NOT NULL,
    [Value]       NVARCHAR (400) NOT NULL,
    CONSTRAINT [PK_Setting] PRIMARY KEY CLUSTERED ([Id] ASC)
);

