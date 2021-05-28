CREATE TABLE [dbo].[ProductDeliveryOptions] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [ProductId]      INT            NULL,
    [ExternalId]     INT            NOT NULL,
    [OptionName]     NVARCHAR (MAX) NULL,
    [OptionsComment] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_ProductDeliveryOptions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductDeliveryOptions_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ProductDeliveryOptions_ProductId]
    ON [dbo].[ProductDeliveryOptions]([ProductId] ASC);

