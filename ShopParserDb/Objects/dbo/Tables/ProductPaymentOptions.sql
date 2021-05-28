CREATE TABLE [dbo].[ProductPaymentOptions] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [ProductId]      INT            NULL,
    [ExternalId]     INT            NOT NULL,
    [OptionName]     NVARCHAR (MAX) NULL,
    [OptionsComment] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_ProductPaymentOptions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductPaymentOptions_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ProductPaymentOptions_ProductId]
    ON [dbo].[ProductPaymentOptions]([ProductId] ASC);

