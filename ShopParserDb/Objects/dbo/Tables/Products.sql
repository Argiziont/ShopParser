CREATE TABLE [dbo].[Products] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [CompanyId]      INT            NULL,
    [ExternalId]     NVARCHAR (MAX) NULL,
    [Title]          NVARCHAR (MAX) NULL,
    [Url]            NVARCHAR (MAX) NULL,
    [SyncDate]       DATETIME2 (7)  NOT NULL,
    [ExpirationDate] DATETIME2 (7)  NOT NULL,
    [ProductState]   INT            NOT NULL,
    [Description]    NVARCHAR (MAX) NULL,
    [Price]          NVARCHAR (MAX) NULL,
    [KeyWords]       NVARCHAR (MAX) NULL,
    [JsonData]       NVARCHAR (MAX) NULL,
    [JsonDataSchema] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Products_Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Companies] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Products_CompanyId]
    ON [dbo].[Products]([CompanyId] ASC);

