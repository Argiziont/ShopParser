CREATE TABLE [dbo].[Sources] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Sources] PRIMARY KEY CLUSTERED ([Id] ASC)
);

