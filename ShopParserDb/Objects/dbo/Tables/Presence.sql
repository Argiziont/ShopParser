CREATE TABLE [dbo].[Presence] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [PresenceSureAvailable] BIT            NOT NULL,
    [OrderAvailable]        BIT            NOT NULL,
    [Available]             BIT            NOT NULL,
    [Title]                 NVARCHAR (MAX) NULL,
    [Ending]                BIT            NOT NULL,
    [Waiting]               BIT            NOT NULL,
    [ProductId]             INT            NOT NULL,
    CONSTRAINT [PK_Presence] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Presence_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id]) ON DELETE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Presence_ProductId]
    ON [dbo].[Presence]([ProductId] ASC);

