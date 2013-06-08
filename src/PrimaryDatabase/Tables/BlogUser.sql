CREATE TABLE [dbo].[BlogUser] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Username] NVARCHAR (100) NOT NULL,
    [Password] NVARCHAR (300) NOT NULL,
    [IsAdmin]  BIT            NOT NULL,
    CONSTRAINT [PK_BlogUser] PRIMARY KEY CLUSTERED ([Id] ASC)
);

