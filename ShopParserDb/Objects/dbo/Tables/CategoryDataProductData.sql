CREATE TABLE [dbo].[CategoryDataProductData] (
    [CategoriesId] INT NOT NULL,
    [ProductsId]   INT NOT NULL,
    CONSTRAINT [PK_CategoryDataProductData] PRIMARY KEY CLUSTERED ([CategoriesId] ASC, [ProductsId] ASC),
    CONSTRAINT [FK_CategoryDataProductData_Categories_CategoriesId] FOREIGN KEY ([CategoriesId]) REFERENCES [dbo].[Categories] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CategoryDataProductData_Products_ProductsId] FOREIGN KEY ([ProductsId]) REFERENCES [dbo].[Products] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_CategoryDataProductData_ProductsId]
    ON [dbo].[CategoryDataProductData]([ProductsId] ASC);

