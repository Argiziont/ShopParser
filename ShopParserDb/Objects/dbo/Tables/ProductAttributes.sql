CREATE TABLE [dbo].[ProductAttributes] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [ProductId]       INT            NULL,
    [AttributeName]   NVARCHAR (MAX) NULL,
    [AttributeValues] NVARCHAR (MAX) NULL,
    [AttributeGroup]  NVARCHAR (MAX) NULL,
    [ExternalId]      INT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ProductAttributes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductAttributes_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ProductAttributes_ProductId]
    ON [dbo].[ProductAttributes]([ProductId] ASC);

