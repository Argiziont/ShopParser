CREATE TABLE [dbo].[Categories] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (MAX) NULL,
    [Url]               NVARCHAR (MAX) NULL,
    [SupCategoryDataId] INT            NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Categories_Categories_SupCategoryId] FOREIGN KEY ([SupCategoryDataId]) REFERENCES [dbo].[Categories] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Categories_SupCategoryId]
    ON [dbo].[Categories]([SupCategoryDataId] ASC);

